using System.Collections.Generic;
using UnityEngine;

public class SlotManager : Singleton<SlotManager>
{
    [SerializeField] List<SlotController> slots;
    public List<CarController> GetCars()
    {
        // Lấy các xe đã tới bên (slot.state = ready)
        List<CarController> cars = new List<CarController>();
        foreach (var controller in slots)
        {
            if( controller.CheckEmpty() == false && controller.State == SlotState.READY)
            {
                cars.Add(controller.GetCar());
            }
            
        }
        return cars; 
    }

    public SlotController GetSlotHasFullCar()
    {
        foreach (var slot in slots)
        {
            if( slot.CheckEmpty() == false)
            {
                var cController = slot.GetCar();
                var cData = cController.Data as CarData;
                if (cController.GetCurrentNum() == (int)cData.Size)
                {
                    return slot;
                }
            }
            
        }
        return null;
    }

    public CarController GetFullCar()
    {
        var slot = GetSlotHasFullCar();
        if (slot != null)
        {
            return slot.GetCar();
        }
        return null;
    }

    public List<CarController> GetCarByColor(CarColor color)
    {
        List<CarController> carByColor = new List<CarController>();
        var cars = GetCars();
        foreach (var car in cars)
        {
            var cData = car.Data as CarData;
            if (color == cData.Color && car.GetCurrentNum() < (int) cData.Size)
            {
                carByColor.Add(car);
            }
        }
        return carByColor;
    }

    public bool CheckEmptySlot()
    {
        foreach (var slot in slots)
        {
            if (slot.CheckEmpty() == true)
            {
                return true;
            }
        }
        return false;
    }

    public SlotController GetEmptySlot()
    {
        foreach (var slot in slots)
        {
            if (slot.CheckEmpty() == true)
            {
                return slot;
            }
        }
        Debug.LogWarning("SlotManager: don't getEmptySlot");
        return null;
    }
}
