using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatCar : MonoBehaviour
{
    [SerializeField] ParkingPlotController parkingPlotController;
    [SerializeField] ToggleGroup toggleGroup;
    public void UpdateInfo()
    {
        var plotData = parkingPlotController.ParkingPlotData;
        foreach (var toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            string strColor = toggle.name;
            var uiText = toggle.GetComponent<TextMeshProUGUI>();

            var color = (CarColor)Enum.Parse(typeof(CarColor), strColor);
            uiText.text = plotData.GetNumberByColor(color).ToString();
        }
    }
}
