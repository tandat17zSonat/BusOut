using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomColorHandler: MonoBehaviour
{
    [SerializeField] List<Toggle> toggles;

    public void RandomColor()
    {
        CarColor[] colors = (CarColor[])Enum.GetValues(typeof(CarColor));
        List<CarColor> listColor = new List<CarColor>();
        foreach(var color in colors)
        {
            listColor.Add(color);
        }
        Singleton<ToolManager>.Instance.RandomColorCar(listColor);
    }
}
