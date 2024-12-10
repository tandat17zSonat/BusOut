using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : Singleton<SlotManager>
{
    /// <summary>
    /// Lấy ra emptyslot.
    /// </summary>
    public GameObject GetEmptySlot()
    {
        return null;
    }

    public GameObject GetFullCar()
    {
        return null; 
    }

    public GameObject[] GetCarByColor(CarColor color)
    {
        return null;
    }
}
