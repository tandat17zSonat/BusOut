using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Drawing;

public class ToolManager : Singleton<ToolManager>
{

    private GameObject selectedCar;
    public GameObject SelectedCar
    {
        get
        {
            return selectedCar;
        }
        set
        {
            if (selectedCar != null)
            {
                selectedCar.GetComponent<LineRenderer>().enabled = false;
            }

            if (selectedCar == value || value == null)
            {
                selectedCar = null;
                return;
            }
            else
            {
                selectedCar = value;
                selectedCar.GetComponent<LineRenderer>().enabled = true;
            }

        }
    }

    public void UpdateCar(CarColor color, CarSize size, CarDirection direction)
    {
        if (selectedCar == null) return;

        var controller = selectedCar.GetComponent<CarController>();
        var newData = ((CarData)controller.Data);
        newData.Color = color;
        newData.Size = size;
        newData.Direction = direction;
        controller.SetInfo(newData);
        Debug.Log("Update car: " + color + " " + size + " " + direction);
    }

    public void Remove()
    {
        if (selectedCar == null) return;
        Singleton<PlotManager>.Instance.Remove(selectedCar);
    }

    public void RemoveAll()
    {
        if (selectedCar == null) return;
        Singleton<PlotManager>.Instance.RemoveAll();
    }

    public void RandomColorCar(List<CarColor> colors)
    {
        var listCars = Singleton<PlotManager>.Instance.GetCarObjects();
        foreach (var carObj in listCars)
        {
            var controller = carObj.GetComponent<BController>();
            var carData = controller.Data as CarData;
            carData.Color = colors[UnityEngine.Random.Range(0, colors.Count)];
            controller.SetInfo(carData);
        }
    }

    public void OnPhysic(Toggle toggle)
    {
        bool isTrigger = toggle.isOn;
        Singleton<PlotManager>.Instance.OnPhysic(isTrigger);
    }

    public void AddPassenger(CarColor color, int num)
    {
        Singleton<QueuePassengerController>.Instance.Add(color, num);
    }

    public void RemovePassenger(int num)
    {
        Singleton<QueuePassengerController>.Instance.Remove(num);
    }

    public void UpdatePassenger(int oldIndexInQueue, int newIndexInQueue)
    {
        Singleton<QueuePassengerController>.Instance.UpdatePassenger(oldIndexInQueue, newIndexInQueue);
    }
}

