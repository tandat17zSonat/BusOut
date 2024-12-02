using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PassengerDirection
{
    front1,
    front2,
    front3,

    left1,
    left2,
    left3
}

public class PassengerData
{
    private CarColor color;
    private PassengerDirection direction;
    private bool isSeat = false;

    public CarColor Color { get => color; set => color = value; }
    public PassengerDirection Direction { get => direction; set => direction = value; }
    public bool IsSeat { get => isSeat; set => isSeat = value; }

    public PassengerData()
    {
        this.SetData(CarColor.black, PassengerDirection.left1, false);
    }

    public void SetData(CarColor color, PassengerDirection direction, bool isSeat)
    {
        this.color = color;
        this.direction = direction;
        this.isSeat = isSeat;
    }

    public string GetSpriteName()
    {
        if (this.IsSeat == false)
        {
            return color.ToString() + "_" + direction.ToString();
        }
        else
        {
            return "boy" + "_" + color.ToString() + "_seat";
        }
        
    }
}
