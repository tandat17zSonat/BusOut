using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class CarDataController : BController
{
    [SerializeField] CarScriptableObject carScriptableObject;

    [SerializeField, Space(10)] CarColor color = CarColor.black;
    [SerializeField] CarSize size = CarSize.four;
    [SerializeField] CarDirection direction = CarDirection.LB;


    private bool isStay = false;
    public bool IsStay { get => isStay; set => isStay = value; }

    CarState _state = CarState.PARKING;
    public CarState State { get => _state; set => _state = value; }
    public List<PassengerController> Passengers { get => passengers; set => passengers = value; }

    int currentNum = 0;

    List<PassengerController> passengers = new List<PassengerController>();

    Vector2 oldPosition;
    Vector2 target;
    Vector2 veclocity = Vector2.zero;

    // ----------------------------------------------------------------------------------------------
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

    public void DisplayChangeDirection(CarDirection direction)
    {
        var cData = Data as CarData;
        cData.Direction = direction;
        SetInfo(cData);
    }

    // Cập nhật khi chỉnh sửa ở editor
    private void OnValidate()
    {
        if (this.data == null) this.data = new CarData();
        ((CarData)this.data).SetData(color, size, direction);
        Display();
    }
    # endregion

    // Update is called once per frame
    void Update()
    {
        var cData = Data as CarData;
        switch (_state)
        {
            case CarState.MOVE:
            case CarState.MOVE_TO_SLOT:
                {
                    var v = (Vector3) veclocity * Config.VEC_CAR_MOVE;
                    transform.position += v  * Time.deltaTime;
                    break;
                }
        }

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.y);
        ((CarData)data).Position = transform.localPosition; // Lưu lại vị trí xe mỗi khi xe di chuyển
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_state == CarState.MOVE || _state == CarState.MOVE_TO_SLOT)
        {
            if (collision.gameObject.tag == "DirectionObject")
            {
                _state = CarState.MOVE_TO_SLOT;
                var cData = Data as CarData;
                var obj = collision.gameObject;
                veclocity = Singleton<PlotManager>.Instance.GetDirectionVector(cData.GetDirectionVector(), obj, transform.position, target);
                veclocity.Normalize();

                var delta = 0.5f;
                if( obj.name == "T" && Math.Abs(transform.position.x - target.x) < delta)
                {
                    _state = CarState.READY;
                    transform.DOMove(target, 0.25f);

                    DisplayChangeDirection(CarDirection.parking);
                }
                else
                {
                    var direction = Util.GetCarDirectionByVector(veclocity);
                    DisplayChangeDirection(direction);
                }
            }
            
            if (collision.gameObject.tag == "Car" && _state != CarState.MOVE_TO_SLOT)
            {
                var otherCar = collision.gameObject;
                var otherController = otherCar.GetComponent<CarDataController>();
                if (otherController.State == CarState.PARKING)
                {
                    Singleton<GameManager>.Instance.CarEvent = CarEvent.CRASH;
                    otherController.Crash();
                }

                transform.DOMove(oldPosition, 0.5f).SetEase(Ease.OutBack);
                _state = CarState.PARKING;
            }
        }
    }

    public void SetMove(Vector2 targetPosition)
    {
        var cData = Data as CarData;

        oldPosition = transform.position;
        veclocity = cData.GetDirectionVector();
        target = targetPosition;

        _state = CarState.MOVE;
    }

    public void Crash()
    {
        // rung lắc
        if (_state != CarState.CRASHING)
        {
            _state = CarState.CRASHING;
            transform.DOPunchPosition(new Vector3(0.5f, 0, 0), 1).OnComplete(() =>
            {
                _state = CarState.PARKING;
            });
        }
    }


    #region: Xe rời đi
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
    PARKING,
    MOVE,
    MOVE_TO_SLOT,
    CRASHING,
    READY,
    LEAVE
}
