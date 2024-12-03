using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatPassenger : MonoBehaviour
{
    [SerializeField] QueuePassengerController queuePassengerController;
    [SerializeField] ToggleGroup toggleGroup;

    public void UpdateInfo()
    {
        var queueData = queuePassengerController.QueuePassengerData;
        foreach(var toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            string strColor = toggle.name;
            var uiText = toggle.GetComponent<TextMeshProUGUI>();

            var color = (CarColor) Enum.Parse(typeof(CarColor), strColor);
            uiText.text = queueData.GetNumberByColor(color).ToString();
        }
    }
}
