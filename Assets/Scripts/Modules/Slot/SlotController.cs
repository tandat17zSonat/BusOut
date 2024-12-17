using UnityEditorInternal;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    CarDataController carController;

    public bool CheckEmpty()
    {
        return carController == null;
    }

    

    public CarDataController GetCar()
    {
        return carController;
    }

    public void Free()
    {
        carController = null;
    }

    public void WaitingCar(CarDataController car)
    {
        carController = car;

    }
}
