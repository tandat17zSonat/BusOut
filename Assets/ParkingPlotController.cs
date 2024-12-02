using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingPlotController : MonoBehaviour
{
    [SerializeField] GameObject toolManager;

    public void LoadData(ParkingPlotData parkingPlotData)
    {
        Debug.Log("ParkingPlot -> Load data: Level" + parkingPlotData.Level);
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        CarSpawner spawner = this.transform.GetComponent<CarSpawner>();
        foreach (CarData carData in parkingPlotData.Cars)
        {
            spawner.Create(carData);
        }
    }
}
