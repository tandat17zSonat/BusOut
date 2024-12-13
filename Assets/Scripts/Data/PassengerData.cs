using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerData :BData
{
    private CarColor color;
    private bool isSeat = false;
    private int positionIndex; // index vị trí đừng

    public CarColor Color { get => color;}
    public bool IsSeat { get => isSeat; set => isSeat = value; }
    public int PositionIndex { get => positionIndex;}

    public PassengerData()
    {
        //this.SetData(CarColor.black, 0, false);
    }

    public void SetData(CarColor color, int positionIndex, bool isSeat = false)
    {
        this.color = color;
        this.positionIndex = positionIndex;
        this.isSeat = isSeat;
    }

    public string GetSpriteName()
    {
        if (this.IsSeat == false)
        {
            return color.ToString() + "_front1";
        }
        else
        {
            return "boy" + "_" + color.ToString() + "_seat";
        }
        
    }
}
