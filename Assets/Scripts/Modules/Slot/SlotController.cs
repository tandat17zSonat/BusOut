using UnityEditorInternal;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    CarController carController;
    SlotState _state = SlotState.EMPTY;
    public SlotState State { get => _state; set => _state = value; }

    /// <summary>
    /// slot vào trạng thái chờ xe tới
    /// </summary>
    public void WaitingCar(GameObject car)
    {
        carController = car.GetComponent<CarController>();
        _state = SlotState.WAITING; // Vô hiệu hóa slot này để không cho nhận xe
        Invoke("AfterWaitingCar", Config.TIME_CAR_MOVE);

    }

    public bool CheckEmpty()
    {
        return _state == SlotState.EMPTY;
    }

    void AfterWaitingCar()
    {
        Debug.Log("Slot đã nhận được xe");
        _state = SlotState.READY;
    }

    public CarController GetCar()
    {
        return carController;
    }

    public void Free()
    {
        carController = null;
        _state = SlotState.EMPTY;
    }

}

public enum SlotState
{
    EMPTY, // Xe có thể tới
    WAITING, // Chờ xe, không thể nhận xe khác
    READY // Sẵn sàng đón khách
}
