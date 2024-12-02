using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Drawing;

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

    public void CreateFromToggle()
    {
        try
        {
            string strColor = toggleGroupColor.GetComponent<ToggleGroupController>().SelectedToggleName;
            CarColor color = (CarColor)Enum.Parse(typeof(CarColor), strColor);

            string strSize = toggleGroupSize.GetComponent<ToggleGroupController>().SelectedToggleName;
            CarSize size = (CarSize)Enum.Parse(typeof(CarSize), strSize);

            string strDirection = toggleGroupDirection.GetComponent<ToggleGroupController>().SelectedToggleName;
            CarDirection direction = (CarDirection)Enum.Parse(typeof(CarDirection), strDirection);
            
            Debug.Log("Create " + color + "- " + size + "- " + direction);
            this.carData = new CarData(color, size, direction);

            this.Create(this.carData);
        }
        catch
        {
            Debug.Log("ERROR create car");
            return;
        }
    }

    public void Create(CarData carData)
    {
        GameObject newObject = Instantiate(prefabCar);
        newObject.transform.SetParent(parentObject.transform);

        // Hiển thị Object -------------------------
        CarController controller = newObject.GetComponent<CarController>();
        controller.SetCarData(carData);
        controller.LoadView();
    }
}
