using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomColorHandler : MonoBehaviour
{
    [SerializeField] List<Toggle> toggles;
    [SerializeField] ParkingPlotController plotController;

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

        var plotData = (ParkingPlotData)plotController.Data;
        foreach (var carData in plotData.Cars)
        {
            carData.Color = colors[UnityEngine.Random.Range(0, colors.Count)];
        }
        plotController.Display();
    }
}
