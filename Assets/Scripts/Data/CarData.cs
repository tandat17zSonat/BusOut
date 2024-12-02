using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.Mathematics;
using UnityEngine;

public enum CarColor
{
    black,
    blue,
    brown,
    green,
    grey,
    oceanblue,
    orange,
    pink,
    purple,
    red,
    yellow
}

public enum CarSize
{
    four = 4,
    six = 6,
    ten = 10
}

public enum CarDirection
{
    LB = 1,
    L = 2,
    LT = 3,
    T = 5,
    RT = -3,
    R = -2,
    RB = -1,
    B = 4
}

public class CarData
{
    private CarColor color;
    private CarSize size;
    private CarDirection direction;
    private Vector2 position;

    public CarColor Color { get => color; set => color = value; }
    public CarSize Size { get => size; set => size = value; }
    public CarDirection Direction { get => direction; set => direction = value; }
    public Vector2 Position { get => position; set => position = value; }

    public CarData()
    {

    }

    public CarData(CarColor color, CarSize size, CarDirection direction)
    {
        this.SetData(color, size, direction);
    }

    public void SetData(CarColor color, CarSize size, CarDirection direction)
    {
        this.color = color;
        this.size = size;
        this.direction = direction;

        if(this.Position == null) this.Position = Vector2.zero;
    }

    public string GetSpriteName()
    {
        return this.color.ToString() + "_car_" + (int)this.size + "_" + this.GetDirectionId();
    }

    public void SetDirection(CarDirection direction)
    {
        this.direction = direction;
    }

    public int GetDirectionId()
    {
        return math.abs((int)this.direction);
    }

    public int GetSizeId()
    {
        if (this.size == CarSize.four) return 0;
        if (this.size == CarSize.six) return 1;
        if (this.size == CarSize.ten) return 2;
        return -1;
    }
}
