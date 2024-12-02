using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefabCar;
    [SerializeField] GameObject parentObject;

    [SerializeField, Space(10)] ToggleGroup toggleGroupColor;
    [SerializeField] ToggleGroup toggleGroupSize;
    [SerializeField] ToggleGroup toggleGroupDirection;

    

    private CarData carData;

    private void Start()
    {

    }

    public void create()
    {
        string strColor = toggleGroupColor.GetComponent<ToggleGroupController>().SelectedToggleName;
        CarColor color = (CarColor)Enum.Parse(typeof(CarColor), strColor);

        string strSize = toggleGroupSize.GetComponent<ToggleGroupController>().SelectedToggleName;
        CarSize size = (CarSize)Enum.Parse(typeof(CarSize), strSize);

        string strDirection = toggleGroupDirection.GetComponent<ToggleGroupController>().SelectedToggleName;
        CarDirection direction = (CarDirection)Enum.Parse(typeof(CarDirection), strDirection);

        Debug.Log("Create " + color + "- " + size + "- " + direction);

        carData = new CarData(color, size, direction);

        GameObject newObject = Instantiate(prefabCar);
        newObject.transform.SetParent(parentObject.transform);
        newObject.GetComponent<CarController>().SetData(carData);
    }
}
