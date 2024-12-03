using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData: BData
{
    int level;
    ParkingPlotData parkingPlotData;
    QueuePassengerData queuePassengerData;

    public int Level { get => level; set => level = value; }
    public ParkingPlotData ParkingPlotData { get => parkingPlotData; set => parkingPlotData = value; }
    public QueuePassengerData QueuePassengerData { get => queuePassengerData; set => queuePassengerData = value; }

}
