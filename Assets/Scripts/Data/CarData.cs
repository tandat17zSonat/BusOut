using Unity.Mathematics;
using UnityEngine;

public class CarData: BData
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

    public Vector2 GetDirectionVector()
    {
        Vector2 direction = Vector2.zero;
        switch(this.direction)
        {
            case CarDirection.LB:
                {
                    direction = Vector2.left + Vector2.down;
                    break;
                }

            case CarDirection.L:
                {
                    direction = Vector2.left;
                    break;
                }

            case CarDirection.LT:
                {
                    direction = Vector2.left + Vector2.up;
                    break;
                }

            case CarDirection.T:
                {
                    direction = Vector2.up;
                    break;
                }

            case CarDirection.RT:
                {
                    direction = Vector2.right + Vector2.up;
                    break;
                }

            case CarDirection.R:
                {
                    direction = Vector2.right;
                    break;
                }

            case CarDirection.RB:
                {
                    direction = Vector2.right + Vector2.down;
                    break;
                }

            case CarDirection.B:
                {
                    direction = Vector2.down;
                    break;
                }
        }
        return direction.normalized;
    }

    public float GetDirectionAngle()
    {
        switch (this.direction)
        {
            case CarDirection.LB:
            case CarDirection.LT:
            case CarDirection.RT:
            case CarDirection.RB:
                return 45f;
        }
        return 0f;
    }
} 

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
    B = 4,
    parking = 6
}
