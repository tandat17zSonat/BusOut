using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticalCarHandler : MonoBehaviour
{
    [SerializeField] List<Toggle> toggles;

    public void UpdateInfo()
    {
        var plotData = Singleton<PlotManager>.Instance.Data;
        foreach (var toggle in toggles)
        {
            string strColor = toggle.name;
            var uiText = toggle.GetComponent<TextMeshProUGUI>();

            var color = (CarColor)Enum.Parse(typeof(CarColor), strColor);
            uiText.text = plotData.GetNumberByColor(color).ToString();
        }
    }
}
