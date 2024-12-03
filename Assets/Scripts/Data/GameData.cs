using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    ParkingPlotData parkingPlotData;
    QueuePassengerData queuePassengerData;

    public ParkingPlotData ParkingPlotData { get => parkingPlotData; set => parkingPlotData = value; }
    public QueuePassengerData QueuePassengerData { get => queuePassengerData; set => queuePassengerData = value; }

}
