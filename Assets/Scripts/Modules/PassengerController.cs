using UnityEngine;

public class PassengerController : MonoBehaviour
{
    [SerializeField] PassengerDataController pDataController;


    PassengerState _state = PassengerState.READY;
    public PassengerState State { get => _state; set => _state = value; }

    public PassengerData Data { get => pDataController.Data as PassengerData; }

    #region: Hành khách di chuyển lên xe
    public void MoveToCar(CarController car)
    {
        State = PassengerState.MOVING;
        Invoke("AfterMoveToCar", Config.TIME_PASSENGER_TO_CAR);

        // effect
        transform.position = car.transform.position;
        gameObject.SetActive(false);
    }

    private void AfterMoveToCar()
    {
        State = PassengerState.READY;
    }
    #endregion


    public bool IsReady()
    {
        return _state == PassengerState.READY;
    }
}

public enum PassengerState
{
    READY, // sẵn sàng lên xe
    MOVING, // Đang lên xe
    FINISH // đã lên xe
}

