using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Diagnostics;

public class CarController : MonoBehaviour
{
    [SerializeField] CarDataController carDataController;


    CarState _state = CarState.PARKING;

    Vector2 oldPosition;
    Vector2 target;
    SlotController targetSlot;

    int currentPassengerNum = 0;

    public CarState State { get => _state; set => _state = value; }

    public CarData Data { get => carDataController.Data as CarData; }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case CarState.START_MOVE:
                {
                    var v = (Vector3)Data.GetDirectionVector() * Config.VEC_CAR_MOVE;
                    transform.position += v * Time.deltaTime;
                    break;
                }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_state == CarState.START_MOVE)
        {
            // Va chạm với object chỉ đường -> Xe có thể di tới điểm slot đón khách
            if (collision.gameObject.tag == "DirectionObject")
            {
                _state = CarState.MOVE_TO_SLOT;

                var obj = collision.gameObject;
                List<Vector2> points = Singleton<PlotManager>.Instance.GetListPointToTarget(Data, obj, target);

                var sequence = DOTween.Sequence();
                var prePoint = (Vector2) transform.position;
                foreach (var point in points)
                {
                    var time = Vector2.Distance(point, prePoint) / Config.VEC_CAR_MOVE;
                    var delta_direction = point - prePoint;
                    var currentDirection = Util.GetCarDirectionByVector(delta_direction);
                    sequence.Append(
                        transform.DOMove(point, time).OnStart(() =>
                        {
                            // Chỉnh lại hướng đi cho xe
                            carDataController.DisplayChangeDirection(currentDirection);
                        })
                        );
                    prePoint = point;
                }
                sequence.OnComplete(() =>
                {
                    carDataController.DisplayChangeDirection(CarDirection.parking);
                    _state = CarState.READY;
                });
            }

            if (collision.gameObject.tag == "Car")
            {
                _state = CarState.MOVE_BACK;

                // Xe bị đâm rung lắc
                var otherCar = collision.gameObject;
                var otherController = otherCar.GetComponent<CarController>();
                if (otherController.State == CarState.PARKING)
                {
                    otherController.Crash();
                }

                // Báo targetSlot không chờ xe này nữa
                Singleton<GameManager>.Instance.QueueSlot.Enqueue(targetSlot);
                targetSlot = null;

                // Xe di chuyển về vị trí ban đầu
                transform.DOMove(oldPosition, Config.TIME_CAR_MOVE_BACK)
                    .SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        _state = CarState.PARKING;
                    });

            }
        }
    }

    public void SetMove(SlotController slot)
    {
        var cData = Data as CarData;

        oldPosition = transform.position;
        targetSlot = slot;
        target = slot.transform.position;

        _state = CarState.START_MOVE;

    }

    public void Crash()
    {
        // rung lắc
        if (_state != CarState.CRASHING)
        {
            _state = CarState.CRASHING;
            transform.DOPunchPosition(new Vector3(0.5f, 0, 0), Config.TIME_CAR_SHAKE).OnComplete(() =>
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
                    carDataController.SetInfo(cData);
                })
            );

        totalTime += t1;
        totalTime += t2;
        Invoke("AfterLeave", totalTime);
        return;
    }

    private void AfterLeave()
    {
        Singleton<PlotManager>.Instance.Remove(gameObject);
    }
    #endregion

    public bool IsReady()
    {
        return _state == CarState.READY;
    }

    public bool CheckColor(CarColor color)
    {
        return Data.Color == color;
    }

    public void AddPassenger()
    {
        currentPassengerNum += 1;
    }

    public bool IsFull()
    {
        return currentPassengerNum == (int)Data.Size;
    }

    public void ShowPassengerSeat()
    {
        int size = (int)Data.Size;
        int count = currentPassengerNum - 1;

        var child = transform.Find(size.ToString() + count.ToString());
        child.gameObject.SetActive(true);
    }
}
public enum CarState
{
    PARKING,
    START_MOVE,
    MOVE_TO_SLOT,
    MOVE_BACK,
    CRASHING,
    READY,
    LEAVE
}