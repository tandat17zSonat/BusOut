using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatCar : MonoBehaviour
{
    [SerializeField] ParkingPlotController parkingPlotController;
    [SerializeField] List<Toggle> toggles;
    public void UpdateInfo()
    {
        var plotData = (ParkingPlotData) parkingPlotController.Data;
        foreach (var toggle in toggles)
        {
            string strColor = toggle.name;
            var uiText = toggle.GetComponent<TextMeshProUGUI>();

            var color = (CarColor)Enum.Parse(typeof(CarColor), strColor);
            uiText.text = plotData.GetNumberByColor(color).ToString();
        }
    }
}
