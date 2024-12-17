using System.Collections.Generic;
using System.Net.WebSockets;
using System.Security.Cryptography;
using UnityEngine;

public class SlotManager : Singleton<SlotManager>
{
    [SerializeField] List<SlotController> slots;


    public List<CarDataController> GetCars()
    {
        List<CarDataController> cars = new List<CarDataController>();
        foreach (var slot in slots)
        {
            if( slot.CheckEmpty() == false)
            {
                var car = slot.GetCar();
                cars.Add(car);
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
                var car = slot.GetCar();
                if (car.IsFull())
                {
                    return slot;
                }
            }
            
        }
        return null;
    }

    public CarDataController GetFullCar()
    {
        var slot = GetSlotHasFullCar();
        if (slot != null)
        {
            return slot.GetCar();
        }
        return null;
    }

    public List<CarDataController> GetReadyCarByColor(CarColor color)
    {
        List<CarDataController> carByColor = new List<CarDataController>();
        var cars = GetCars();
        foreach (var car in cars)
        {
            if(car.IsReady() && car.CheckColor(color))
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

    public SlotController GetFirstEmptySlot()
    {
        foreach (var slot in slots)
        {
            if (slot.CheckEmpty() == true)
            {
                return slot;
            }
        }
        Debug.LogWarning("SlotManager: don't have EmptySlot");
        return null;
    }
}
