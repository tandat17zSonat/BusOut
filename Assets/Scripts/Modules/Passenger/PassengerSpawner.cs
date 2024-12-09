using System;
using UnityEngine;
using UnityEngine.UI;

public class PassengerSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    [SerializeField, Space(10)] ToggleGroupController toggleGroupColor;
    [SerializeField] int numIncrease = 1;

    //public void Create()
    //{
    //    try
    //    {
    //        var color = toggleGroupColor.GetSelectedToggle<CarColor>();
    //        ((QueuePassengerController)controller).Add(color, numIncrease);
    //    }
    //    catch
    //    {
    //        Debug.Log("ERROR: create Passenger");
    //        return;
    //    }
    //}

    //public void Remove()
    //{
    //    ((QueuePassengerController)controller).Remove(this.numIncrease);
    //}
}
