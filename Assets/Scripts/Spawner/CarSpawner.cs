using System;
using UnityEngine;
using UnityEngine.UI;

public class CarSpawner : BSpawner
{
    [SerializeField, Space(10)] ToggleGroup toggleGroupColor;
    [SerializeField] ToggleGroup toggleGroupSize;
    [SerializeField] ToggleGroup toggleGroupDirection;

    public override void Create()
    {
        try
        {
            string strColor = toggleGroupColor.GetComponent<ToggleGroupController>().SelectedToggleName;
            CarColor color = (CarColor)Enum.Parse(typeof(CarColor), strColor);

            string strSize = toggleGroupSize.GetComponent<ToggleGroupController>().SelectedToggleName;
            CarSize size = (CarSize)Enum.Parse(typeof(CarSize), strSize);

            string strDirection = toggleGroupDirection.GetComponent<ToggleGroupController>().SelectedToggleName;
            CarDirection direction = (CarDirection)Enum.Parse(typeof(CarDirection), strDirection);

            Debug.Log("Create Car -> " + color + "- " + size + "- " + direction);
            var carData = new CarData(color, size, direction);

            ((ParkingPlotController)controller).Add(carData);
        }
        catch
        {
            Debug.Log("ERROR: create car");
            return;
        }
    }

    public override void Remove()
    {
        ((ParkingPlotController)controller).Remove();
    }
}
