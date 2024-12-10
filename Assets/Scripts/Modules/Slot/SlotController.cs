using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    GameObject car;
    SlotState _state = SlotState.EMPTY;

    /// <summary>
    /// slot vào trạng thái chờ xe tới
    /// </summary>
    public void WaitingCar(GameObject car)
    {
        //Invoke("DoSomething", 2f);
        // chờ vài giây để xe tới
        _state = SlotState.WAITING; // Vô hiệu hóa slot này để không cho nhận xe
    }
}

public enum SlotState
{
    EMPTY, // Xe có thể tới
    WAITING, // Chờ xe, không thể nhận xe khác
    READY // Sẵn sàng đón khách
}
