using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PassengerData
{
    private int width = 12;
    private int height = 7;

    private CarColor color;
    private bool isSeat = false;
    private int positionIndex;

    public CarColor Color { get => color; set => color = value; }
    public bool IsSeat { get => isSeat; set => isSeat = value; }
    public int PositionIndex { get => positionIndex; set => positionIndex = value; }

    public PassengerData()
    {
        this.SetData(CarColor.black, 0, false);
    }

    public void SetData(CarColor color, int positionIndex, bool isSeat)
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

    public Vector2 GetPosition()
    {
        int cellX = this.positionIndex - width > 0 ? width : this.positionIndex;
        int cellY = this.positionIndex - width > 0 ? this.positionIndex - 12 : 0;

        return new Vector2(cellX * 0.6f, cellY * 0.6f);
    }
}
