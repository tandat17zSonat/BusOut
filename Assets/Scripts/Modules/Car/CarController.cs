using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Diagnostics;

[RequireComponent(typeof(PolygonCollider2D))]
public class CarController : BController
{
    [SerializeField] CarScriptableObject carScriptableObject;

    [SerializeField, Space(10)] CarColor color = CarColor.black;
    [SerializeField] CarSize size = CarSize.four;
    [SerializeField] CarDirection direction = CarDirection.LB;

    [SerializeField, Space(10)] BoudaryController boudary;

    private bool isStay = false;
    public bool IsStay { get => isStay; set => isStay = value; }

    CarState _state = CarState.PARKING;
    public CarState State { get => _state; set => _state = value; }
    public List<PassengerController> Passengers { get => passengers; set => passengers = value; }

    int currentNum = 0;

    List<PassengerController> passengers = new List<PassengerController>();




    public override void Init()
    {
        this.data = new CarData();
    }

    # region: Cập nhật info và hiển thị đúng
    Vector2 defaultPostion;
    public override void SetInfo(BData data)
    {
        base.SetInfo(data);

        var carData = data as CarData;
        color = carData.Color;
        size = carData.Size;
        direction = carData.Direction;

        transform.localPosition = carData.Position;
        defaultPostion = transform.position;
    }

    public override void Display()
    {
        // update sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite sprite = carScriptableObject.GetSprite((CarData)this.data);
        spriteRenderer.sprite = sprite;

        // update collider
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        collider.points = carScriptableObject.GetCollisionPoints((CarData)this.data);

        // lật object (vì phải lật ảnh)
        Vector3 currentScale = transform.localScale;
        var direction = ((CarData)this.data).Direction;
        if (direction == CarDirection.RB || direction == CarDirection.RT || direction == CarDirection.R)
        {
            currentScale.x = -Mathf.Abs(currentScale.x);
            transform.localScale = currentScale;
        }
        else
        {
            currentScale.x = Mathf.Abs(currentScale.x);
            transform.localScale = currentScale;
        }
    }
    # endregion

    public void DisplayChangeDirection(CarDirection direction)
    {
        var cData = Data as CarData;
        cData.Direction = direction;
        SetInfo(cData);
    }

    // Update is called once per frame
    void Update()
    {
        var cData = Data as CarData;
        var v = 10;
        switch (_state)
        {
            case CarState.WILL_CRASH:
                {
                    transform.position += (Vector3)cData.GetDirectionVector() * v * Time.deltaTime;
                    break;
                }
        }
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.y);
        ((CarData)data).Position = transform.localPosition; // Lưu lại vị trí xe mỗi khi xe di chuyển
    }

    // Cập nhật khi chỉnh sửa ở editor
    private void OnValidate()
    {
        if (this.data == null) this.data = new CarData();
        ((CarData)this.data).SetData(color, size, direction);
        Display();
    }

    public (GameObject, Vector3) CanMove()
    {
        Vector2 boxSize = new Vector2(0.5f, 0.5f); // Kích thước hình hộp
        float maxDistance = 1000f; // Khoảng cách kiểm tra
        float angle = ((CarData)data).GetDirectionAngle();
        Vector2 direction = ((CarData)data).GetDirectionVector();

        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, angle, direction, maxDistance);

        // Nếu va chạm
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                try
                {
                    var carController = hit.collider.GetComponent<CarController>();
                    if (carController.State == CarState.PARKING)
                    {
                        return (hit.collider.gameObject, hit.point);
                    }
                }
                catch
                {

                }


            }
        }
        return (null, Vector3.zero);
    }


    #region: Xe di chuyển tới slot
    public void MoveToSlot(Vector2 destination)
    {
        _state = CarState.MOVE;

        // effect
        var cData = Data as CarData;
        var direction = (Vector3)cData.GetDirectionVector();
        var listPoint = Util.GetListPoint(transform.position, direction, destination);

        float totalTime = 0;
        var sequence = DOTween.Sequence();
        var prePoint = transform.position;
        foreach (var point in listPoint)
        {
            Debug.Log("Point: " + point.x + " " + point.y + " " + point.z);
            var distance = Vector3.Distance(prePoint, point);
            Vector3 delta = point - prePoint;
            delta.Normalize();
            float t = distance / Config.VEC_CAR_MOVE;
            sequence.Append(
                transform.DOMove(point, t)
                .OnStart(() =>
                {
                    cData.Direction = Util.GetCarDirectionByVector(delta);
                    SetInfo(cData);
                })
                );
            prePoint = point;

            totalTime += t;
        }

        Invoke("AfterMoveToSlot", totalTime);
    }

    private void AfterMoveToSlot()
    {
        _state = CarState.READY;
        var cData = Data as CarData;
        cData.Direction = CarDirection.parking;
        Display();
    }
    #endregion

    #region: Xe rời đi
    [SerializeField] int backTime = 1;
    public void Leave()
    {
        // Xe di chuyen rời đi -------------
        State = CarState.LEAVE;

        float totalTime = 0;
        var sequence = DOTween.Sequence();

        var point1 = transform.position + Vector3.down * 2;
        float t1 = 2 / Config.VEC_CAR_MOVE;
        sequence.Append(transform.DOMove(point1, t1));

        var point2 = point1 + Vector3.right * 30;
        float t2 = 30 / Config.VEC_CAR_MOVE; 
        sequence.Append(
            transform.DOMove(point2, t2)
                .OnStart(() =>
                {
                    CarData cData = Data as CarData;
                    cData.Direction = CarDirection.R;
                    SetInfo(cData);
                })
            );

        totalTime += t1;
        totalTime += t2;
        Invoke("AfterLeave", totalTime);
        return;
    }

    private void AfterLeave()
    {
        foreach (var p in Passengers)
        {
            Singleton<QueuePassengerController>.Instance.ReturnPassenger(p.gameObject);
        }
        Singleton<PlotManager>.Instance.Remove(gameObject);
    }
    #endregion

    #region: crash
    public void Crash(Vector3 point)
    {
        //var beforePosition = transform.position;
        //var time = Vector3.Distance(point, transform.position) / 5f;

        //var sequence = DOTween.Sequence();
        //sequence.Append(transform.DOMove(point, time));
        //sequence.Append(transform.DOMove(beforePosition, time));
        _state = CarState.WILL_CRASH;
    }

    public void Crash2()
    {
        if (_state != CarState.CRASHING)
        {
            _state = CarState.CRASHING;
            transform.DOPunchPosition(new Vector3(0.5f, 0, 0), 1).OnComplete(() =>
            {
                _state = CarState.PARKING;
            });
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("OnCollisionEnter2D");
        if (_state == CarState.WILL_CRASH)
        {
            _state = CarState.PARKING;
            transform.DOMove(defaultPostion, 0.5f).SetEase(Ease.OutFlash);

            other.gameObject.GetComponent<CarController>().Crash2();
        }

    }
    #endregion

    public void IncreaseNum(int num)
    {
        currentNum += num;
    }
    public int GetCurrentNum()
    {
        return currentNum;
    }

    public bool IsReady()
    {
        return _state == CarState.READY;
    }

    public bool CheckColor(CarColor color)
    {
        var carData = Data as CarData;
        return carData.Color == color;
    }

    public bool IsFull()
    {
        var carData = Data as CarData;
        return currentNum == (int)carData.Size;
    }
}

public enum CarState
{
    WILL_CRASH,
    CRASHING,
    PARKING,
    MOVE,
    READY,
    LEAVE
}
