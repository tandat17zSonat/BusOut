using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerSpawner : BSpawner
{
    [SerializeField, Space(10)] ToggleGroup toggleGroupColor;
    [SerializeField] int numIncrease = 1;

    public override void Create()
    {
        try
        {
            string strColor = toggleGroupColor.GetComponent<ToggleGroupController>().SelectedToggleName;
            CarColor color = (CarColor)Enum.Parse(typeof(CarColor), strColor);
            ((QueuePassengerController)controller).Add(color, numIncrease);
        }
        catch
        {
            Debug.Log("ERROR: create Passenger");
            return;
        }
    }

    public void Remove()
    {
        ((QueuePassengerController)controller).Remove(this.numIncrease);
    }
}
