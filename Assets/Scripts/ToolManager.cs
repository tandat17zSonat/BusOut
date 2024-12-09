﻿using System.IO;
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

    public void GeneratePassenger(int num)
    {
        CarColor[] values = (CarColor[])System.Enum.GetValues(typeof(CarColor)); // Lấy tất cả các giá trị của enum
        
        for (int i = 0; i < num; i++)
        {
            var color = values[UnityEngine.Random.Range(0, values.Length)];
            Singleton<QueuePassengerController>.Instance.Add(color, 1);
        }
    }

    public void MergeQueue()
    {
        QueuePassengerData  queueData = Singleton<QueuePassengerController>.Instance.Data;
        queueData.MergeQueue();
        Singleton<CellManager>.Instance.SetInfo();
    }

    public void RemovePassengerGroup(int indexInQueue)
    {
        QueuePassengerData queueData = Singleton<QueuePassengerController>.Instance.Data;
        queueData.QueuePassenger.Remove(indexInQueue);

        Singleton<QueuePassengerController>.Instance.Data = queueData;
        Singleton<CellManager>.Instance.SetInfo();
    }

    public void DevidePassengerGroup(int index, int divisor)
    {
        QueuePassengerData queueData = Singleton<QueuePassengerController>.Instance.Data;

        LinkedListNode<GroupPassenger> node = queueData.QueuePassenger.GetNodeAt(index);

        if (node != null)
        {
            CarColor color = node.Value.color;
            int num = node.Value.num;

            int q = num / divisor,
                r = num % divisor;

            if( r != 0)
            {
                var item = new GroupPassenger(color, r);
                queueData.QueuePassenger.ToList().AddAfter(node, item);
            }
            

            for ( int i = 0; i<q; i++ )
            {
                var item = new GroupPassenger(color, divisor);
                queueData.QueuePassenger.ToList().AddAfter(node, item);
            }
            queueData.QueuePassenger.Remove(index);

            Singleton<QueuePassengerController>.Instance.Data = queueData;
            Singleton<CellManager>.Instance.SetInfo();
        }
    }
}

