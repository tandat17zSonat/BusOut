using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : BController
{
    [SerializeField, Space(10)] GameObject parkingPlot;
    [SerializeField] GameObject queuePassengers;

    public override BData Data
    {
        get
        {
            var plotData = (ParkingPlotData) parkingPlot.GetComponent<BController>().Data;
            ((GameData)data).ParkingPlotData = plotData;
            Debug.Log(plotData.Cars.Count);

            var queueData = (QueuePassengerData) queuePassengers.GetComponent<BController>().Data;
            ((GameData)data).QueuePassengerData = queueData;
            return (GameData)data;
        }
        set
        {
            data = value;
        }
    }

    public override void Init()
    {
        data = new GameData();
    }

    public override void Display()
    {
        Debug.Log("GameController -> Display");
        var plotData = ((GameData)data).ParkingPlotData;
        var plotController = parkingPlot.GetComponent<BController>();
        plotController.SetInfo(plotData);

        var queueData = ((GameData)data).QueuePassengerData;
        var queueController = queuePassengers.GetComponent<BController>();
        queueController.SetInfo(queueData);

    }

    public void Remove()
    {
        var selectedCar = Singleton<ToolManager>.Instance.SelectedCar;
        if (selectedCar != null)
        {
            var plotController = parkingPlot.GetComponent<BController>();
            ((ParkingPlotController)plotController).Remove(selectedCar);

            Singleton<ToolManager>.Instance.SelectedCar = null;
        }
    }
    public void RemoveAll()
    {
        var plotController = parkingPlot.GetComponent<BController>();
        ((ParkingPlotController) plotController).RemoveAll();
    }
}
