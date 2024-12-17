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

    public static  CarDirection GetCarDirectionByVector(Vector3 normal)
    {
        if (normal.x < 0 && normal.y < 0) return CarDirection.LB;
        if (normal.x < 0 && normal.y == 0) return CarDirection.L;
        if (normal.x < 0 && normal.y > 0) return CarDirection.LT;
        if (normal.x == 0 && normal.y > 0) return CarDirection.T;
        if (normal.x > 0 && normal.y > 0) return CarDirection.RT;
        if (normal.x > 0 && normal.y == 0) return CarDirection.R;
        if (normal.x > 0 && normal.y < 0) return CarDirection.RB;
        if (normal.x == 0 && normal.y < 0) return CarDirection.B;
        return CarDirection.parking;
    }
}
