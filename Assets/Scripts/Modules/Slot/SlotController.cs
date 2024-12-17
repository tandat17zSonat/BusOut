using UnityEditorInternal;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    CarController carController;

    public bool CheckEmpty()
    {
        return carController == null;
    }

    

    public CarController GetCar()
    {
        return carController;
    }

    public void Free()
    {
        carController = null;
    }

    public void SetCar(CarController car)
    {
        carController = car;

    }
}
