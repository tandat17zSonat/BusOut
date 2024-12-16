using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
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

    public static Vector3 GetIntersectionPoint(Vector3 point, Vector3 direction, Vector3 planeNormal, Vector3 planePoint)
    {
        Plane plane = new Plane(planeNormal.normalized, planePoint);

        Ray ray = new Ray(point, direction);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 intersectionPoint = ray.GetPoint(distance);
            return intersectionPoint;
        }
        else
        {
            Debug.Log("Không có giao điểm.");
        }
        return Vector3.zero;
    }

    public static List<Vector3> GetListPoint(Vector3 point, Vector3 direction, Vector3 target)
    {
        var width = 10;
        List<Vector3> list = new List<Vector3>();
        var p0 = new Vector3(-width, -15, 0);
        var p1 = new Vector3(width, -15, 0);
        var p2 = new Vector3(width, 5, 0);
        var p3 = new Vector3(-width, 5, 0);

        var i0 = GetIntersectionPoint(point, direction, p2 - p1, p0);
        var i1 = GetIntersectionPoint(point, direction, p3 - p2, p1);
        var i2 = GetIntersectionPoint(point, direction, p0 - p3, p2);
        var i3 = GetIntersectionPoint(point, direction, p1 - p0, p3);

        if (i0 != Vector3.zero && i0.x >= -width && i0.x <= width)
        {
            Debug.Log("GetListPoint: " + direction.x + " " + direction.y + " " + direction.z);
            Debug.Log("GetListPoint: intersection0 " + i0.x + " " + i0.y + " " + i0.z);

            if (direction.x > 0)
            {
                list.Add(i0);
                list.Add(p1);
                list.Add(p2);
            }
            else
            {
                list.Add(i0);
                list.Add(p0);
                list.Add(p3);
            }
        }
        else if (i1 != Vector3.zero && i1.y >= -15 && i1.y <= 5)
        {
            Debug.Log("GetListPoint: intersection1 " + i1.x + " " + i1.y + " " + i1.z);
            list.Add(i1);
            list.Add(p2);
        }
        else if (i3 != Vector3.zero && i3.y >= -15 && i3.y <= 5)
        {
            Debug.Log("GetListPoint: intersection3 " + i3.x + " " + i3.y + " " + i3.z);
            list.Add(i3);
            list.Add(p3);
        }
        else if (i2 != Vector3.zero && i2.x >= -width && i2.x <= width)
        {
            list.Add(i2);
            Debug.Log("GetListPoint: intersection2 " + i2.x + " " + i2.y + " " + i2.z);
        }


        //--------------------------------------------
        var iTarget = GetIntersectionPoint(target, Vector3.down, p0 - p3, p2);
        list.Add(iTarget);
        list.Add(target);
        return list;
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
