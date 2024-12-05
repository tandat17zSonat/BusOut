using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupController : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public Type enumType;

    private string selectedToggleName;
    public string SelectedToggleName { get; private set; }
    void Start()
    {
        // Gắn sự kiện onValueChanged cho từng toggle
        foreach (var toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.AddListener((isOn) => OnToggleChanged(toggle, isOn));
        }
    }

    private void OnToggleChanged(Toggle toggle, bool isOn)
    {
        if (isOn) // Nếu toggle được bật
        {
            SelectedToggleName = toggle.name;

            //---------------------------
            var selectedCar = Singleton<ToolManager>.Instance.SelectedCar;
            if (selectedCar == null) return;

            var controller = selectedCar.GetComponent<CarController>();
            var newData = ((CarData)controller.Data);

            try
            {
                string strColor = toggle.name;
                CarColor color = (CarColor)Enum.Parse(typeof(CarColor), strColor);
                newData.Color = color;
            }
            catch
            {
            }

            try
            {
                string strSize = toggle.name;
                CarSize size = (CarSize)Enum.Parse(typeof(CarSize), strSize);
                newData.Size = size;
            }
            catch
            {

            }

            try
            {
                string strDirection = toggle.name;
                CarDirection direction = (CarDirection)Enum.Parse(typeof(CarDirection), strDirection);
                newData.Direction = direction;
            }
            catch
            {

            }
            controller.SetInfo(newData);
        }
    }
}
