using System.Collections.Generic;
using UnityEngine;

public class SlotManager : Singleton<SlotManager>
{
    [SerializeField] List<SlotController> slots;
    public List<CarController> GetCars()
    {
        List<CarController> cars = new List<CarController>();
        foreach (var controller in slots)
        {
            if( controller.CheckEmpty() == false)
            {
                cars.Add(controller.GetCar());
            }
            
        }
        return cars; 
    }
    public CarController GetFullCar()
    {
        var cars = GetCars();
        foreach (var cController in cars)
        {
            var cData = cController.Data as CarData;
            if( cController.GetCurrentNum() == (int) cData.Size)
            {
                return cController;
            }
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
