using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class CarController : MonoBehaviour
{
    [SerializeField] CarDataController carDataController;


    CarState _state = CarState.PARKING;

    Vector2 veclocity = Vector2.zero;
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
            case CarState.MOVE:
            case CarState.MOVE_TO_SLOT:
                {
                    var v = (Vector3)veclocity * Config.VEC_CAR_MOVE;
                    transform.position += v * Time.deltaTime;
                    break;
                }
        }
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

                var delta = 0.75f;

                // Di gan toi slot
                if (obj.name == "T" &&  ((veclocity.x > 0 && obj.transform.position.x > target.x)
                        || (veclocity.x < 0 && obj.transform.position.x < target.x)))
                {
                        _state = CarState.READY;
                        transform.DOMove(target, 0.25f);

                        carDataController.DisplayChangeDirection(CarDirection.parking);
                    
                }
                else
                {
                    var direction = Util.GetCarDirectionByVector(veclocity);
                    carDataController.DisplayChangeDirection(direction);
                }
            }

            if (collision.gameObject.tag == "Car" && _state != CarState.MOVE_TO_SLOT)
            {
                var otherCar = collision.gameObject;
                var otherController = otherCar.GetComponent<CarController>();
                if (otherController.State == CarState.PARKING)
                {
                    otherController.Crash();
                }

                Singleton<GameManager>.Instance.QueueSlot.Enqueue(targetSlot);
                targetSlot = null;

                transform.DOMove(oldPosition, 0.5f).SetEase(Ease.OutBack);
                _state = CarState.PARKING;
            }
        }
    }

    public void SetMove(SlotController slot)
    {
        var cData = Data as CarData;

        oldPosition = transform.position;
        veclocity = cData.GetDirectionVector();
        targetSlot = slot;
        target = slot.transform.position;

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

    public bool CanMoveToSlot()
    {

        return true;
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
    MOVE,
    MOVE_TO_SLOT,
    CRASHING,
    READY,
    LEAVE
}