using UnityEngine;

public class SlotController : MonoBehaviour
{
    CarController carController;
    SlotState _state = SlotState.EMPTY;

    /// <summary>
    /// slot vào trạng thái chờ xe tới
    /// </summary>
    public void WaitingCar(GameObject car)
    {
        carController = car.GetComponent<CarController>();
        _state = SlotState.WAITING; // Vô hiệu hóa slot này để không cho nhận xe
        Invoke("ToReady", Config.TIME_WAIT_CAR);

    }

    public bool CheckEmpty()
    {
        return _state == SlotState.EMPTY;
    }

    void ToReady()
    {
        Debug.Log("Slot đã nhận được xe");
        _state = SlotState.READY;
    }

    public CarController GetCar()
    {
        return carController;
    }
}

public enum SlotState
{
    EMPTY, // Xe có thể tới
    WAITING, // Chờ xe, không thể nhận xe khác
    READY // Sẵn sàng đón khách
}
