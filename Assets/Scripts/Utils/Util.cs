using System;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static T GetRandomEnumValue<T>(T[] ignores = null)
    {
        T[] values = (T[])System.Enum.GetValues(typeof(T)); // Lấy tất cả các giá trị của enum
        Dictionary<T, bool> mapIgnore = new Dictionary<T, bool>();
        int ignoreLength = 0;
        if (ignores != null)
        {
            foreach (T t in ignores)
            {
                mapIgnore.Add(t, true);
            }
            ignoreLength = ignores.Length;
        }


        int random = UnityEngine.Random.Range(0, values.Length - ignoreLength);

        int i = 0;
        foreach (T value in values)
        {
            if (mapIgnore.ContainsKey(value) == false)
            {
                if (i == random) return value;
                i += 1;
            }
        }
        return values[0];
    }

    public static int GetEnumLength<T>()
    {
        T[] values = (T[])System.Enum.GetValues(typeof(T));
        return values.Length;
    }

    public static  CarDirection GetCarDirectionByVector(Vector2 normal)
    {
        CarDirection res = CarDirection.parking;
        float max = -1.0f;
        foreach (CarDirection direction in Enum.GetValues(typeof(CarDirection)))
        {
            var v = GetDirectionVector(direction);
            var dotValue = Vector2.Dot(normal, v);
            if ( max < dotValue)
            {
                max = dotValue;
                res = direction;
            }
        }
        return res;
    }

    public static Vector2 GetDirectionVector(CarDirection carDirection)
    {
        Vector2 direction = Vector2.zero;
        switch (carDirection)
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
}
