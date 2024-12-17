﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlotManager : Singleton<PlotManager>
{
    [SerializeField] ObjectPool objectPool;
    [SerializeField] DirectionController directionController;

    public ParkingPlotData Data
    {
        get
        {
            var plotData = new ParkingPlotData();
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf == true)
                {
                    var carData = child.gameObject.GetComponent<BController>().Data as CarData;
                    plotData.Cars.Add(carData);
                }
            }
            return plotData;
        }
        set
        {
            RemoveAll();
            foreach( var carData in value.Cars)
            {
                Add(carData);
            }
        }
    }

    public List<GameObject> GetCarObjects()
    {
        var listCars = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf == true)
            {
                listCars.Add(child.gameObject);
            }
        }
        return listCars;
    }

    public void Add(CarData carData)
    {
        var obj = objectPool.GetObject();
        var controller = obj.GetComponent<BController>();
        controller.SetInfo(carData);
    }

    public void Remove(GameObject obj)
    {
        objectPool.ReturnObject(obj);
    }

    public void RemoveAll()
    {
        objectPool.Reset();
    }


    #region: Xe rời khỏi slot
    private CarDataController carLeaved;
    public void Leave(CarDataController car)
    {
        this.carLeaved = car;
        this.carLeaved.Leave();
        Invoke("AfterLeave", Config.TIME_CAR_LEAVE);
    }
    private void AfterLeave()
    {
        Remove(carLeaved.gameObject);
    }
    #endregion

    public void SetTrigger(bool isTrigger)
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Collider2D>().isTrigger = isTrigger;
        }
    }

    public void Reset()
    {
        RemoveAll();
    }

    public Vector2 GetDirectionVector(Vector2 oldDirection, GameObject collision, Vector2 collisionPosition, Vector2 targetPosition)
    {
        return directionController.GetDirectionVector(oldDirection, collision, collisionPosition, targetPosition);
    }
}
