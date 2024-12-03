using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject parentObject;
    [SerializeField] QueuePassengerController queueController;

    [SerializeField, Space(10)] ToggleGroup toggleGroupColor;
    [SerializeField] int numIncrease = 1;
    
    public void CreateFromToggle()
    {
        try
        {
            string strColor = toggleGroupColor.GetComponent<ToggleGroupController>().SelectedToggleName;
            CarColor color = (CarColor)Enum.Parse(typeof(CarColor), strColor);

            Debug.Log("Create Passenger-> " + color);
            queueController.EnqueuePassenger(color, this.numIncrease);
        }
        catch
        {
            Debug.Log("ERROR: create car");
            return;
        }
    }

    public void RemovePassenger()
    {
        queueController.DequeuePassenger(this.numIncrease);
    }
}
