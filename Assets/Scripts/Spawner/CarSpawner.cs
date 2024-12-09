using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public void GenCar(int numCar4, int numCar6, int numCar10)
    {
        Debug.Log("GenCar: " + numCar4 + " " + numCar6 + " " + numCar10);
        var mapCount = new Dictionary<int, int>();
        mapCount[4] =  numCar4;
        mapCount[6] = numCar6;
        mapCount[10] = numCar10;
        int count = 0;
        while (count < 1000)
        {
            count++;
            var rColor = GetRandomEnumValue<CarColor>();
            var rDirection = GetRandomEnumValue<CarDirection>();
            var rSize = GetRandomEnumValue<CarSize>();

            if(mapCount[(int) rSize] > 0)
            {
                mapCount[(int)rSize] -= 1;
                var position = GetRandomPosition();

                var carData = new CarData(rColor, rSize, rDirection);
                carData.Position = position;
                Singleton<PlotManager>.Instance.Add(carData);
            }
        }

    }

    Vector2 GetRandomPosition()
    {
        return Vector2.zero;
    }

    T GetRandomEnumValue<T>()
    {
        T[] values = (T[])System.Enum.GetValues(typeof(T)); // Lấy tất cả các giá trị của enum
        return values[UnityEngine.Random.Range(0, values.Length)];      // Chọn một giá trị ngẫu nhiên
    }
}
