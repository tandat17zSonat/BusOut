using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingPlotController : MonoBehaviour
{
    private ParkingPlotData plotData = new ParkingPlotData();
    public ParkingPlotData ParkingPlotData { get => plotData; set => plotData = value; }
}
