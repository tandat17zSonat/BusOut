using UnityEditorInternal;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    CarController carController;

    SlotState _state = SlotState.EMPTY;
    public SlotState State { get => _state; set => _state = value; }

    public bool CheckEmpty()
    {
        return _state == SlotState.EMPTY;
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

    #region: slot vào trạng thái chờ xe tới
    public void WaitingCar(CarController car)
    {
        carController = car;
        _state = SlotState.BUSY; // Vô hiệu hóa slot này để không cho nhận xe

    }
    #endregion
}

public enum SlotState
{
    EMPTY, // Xe có thể tới
    BUSY, // Chờ xe, không thể nhận xe khác
    READY // Sẵn sàng đón khách
}
