using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public void GenerateCar(int numCar4, int numCar6, int numCar10)
    {
        Debug.Log("GenCar: " + numCar4 + " " + numCar6 + " " + numCar10);
        var mapCount = new Dictionary<int, int>();
        mapCount[4] = numCar4;
        mapCount[6] = numCar6;
        mapCount[10] = numCar10;
        int count = 0;

        List<CarDirection> ignoreDirection = new List<CarDirection>() { CarDirection.parking};
        List<CarSize> ignoreSize = new List<CarSize>();
        while (true)
        {
            count++;
            var rColor = Util.GetRandomEnumValue<CarColor>();
            var rDirection = Util.GetRandomEnumValue<CarDirection>(ignoreDirection.ToArray());
            var rSize = Util.GetRandomEnumValue<CarSize>(ignoreSize.ToArray());

            if(mapCount[(int) rSize] > 0)
            {
                mapCount[(int)rSize] -= 1;
                var position = GetRandomPosition();

                var carData = new CarData(rColor, rSize, rDirection);
                carData.Position = position;

                Singleton<PlotManager>.Instance.Add(carData);
            }

            // Không random lại các giá trị đã tìm đủ
            if(mapCount[(int) rSize] == 0)
            {
                ignoreSize.Add(rSize);
            }

            // Random xong 
            if( ignoreSize.Count == Util.GetEnumLength<CarSize>())
            {
                break;
            }
        }

    }

    Vector2 GetRandomPosition()
    {
        return Vector2.zero;
    }
}
