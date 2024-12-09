using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRandomColorController: MonoBehaviour
{
    [SerializeField] List<Toggle> toggles;

    public void RandomColor()
    {
        List<CarColor> colors = new List<CarColor>();
        foreach (var toggle in toggles)
        {
            if(toggle.isOn)
            {
                CarColor color = (CarColor)Enum.Parse(typeof(CarColor), toggle.name);
                colors.Add(color);
            }
        }

        Singleton<ToolManager>.Instance.RandomColorCar(colors);
    }
}
