using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSpawner : BSpawner
{
    [SerializeField, Space(10)] ToggleGroup toggleGroupColor;
    [SerializeField] ToggleGroup toggleGroupSize;
    [SerializeField] ToggleGroup toggleGroupDirection;

    public override void Create()
    {
        try
        {
            string strColor = toggleGroupColor.GetComponent<ToggleGroupController>().SelectedToggleName;
            CarColor color = (CarColor)Enum.Parse(typeof(CarColor), strColor);

            string strSize = toggleGroupSize.GetComponent<ToggleGroupController>().SelectedToggleName;
            CarSize size = (CarSize)Enum.Parse(typeof(CarSize), strSize);

            string strDirection = toggleGroupDirection.GetComponent<ToggleGroupController>().SelectedToggleName;
            CarDirection direction = (CarDirection)Enum.Parse(typeof(CarDirection), strDirection);

            Debug.Log("Create Car -> " + color + "- " + size + "- " + direction);
            var carData = new CarData(color, size, direction);

            ((ParkingPlotController)controller).Add(carData);
        }
        catch
        {
            Debug.Log("ERROR: create car");
            return;
        }
    }

    public override void Remove()
    {
        
    }

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
                ((ParkingPlotController)controller).Add(carData);
            }
        }

    }

    Vector2 GetRandomPosition()
    {
        return Vector2.zero;
    }

    bool IsPositionValid(Vector2 position)
    {
        // Tạo một object tạm thời
        GameObject tempObject = Instantiate(prefab, position, Quaternion.identity);
        PolygonCollider2D collider = tempObject.GetComponent<PolygonCollider2D>();

        // Kiểm tra va chạm
        Collider2D[] results = new Collider2D[10];
        int count = Physics2D.OverlapCollider(collider, new ContactFilter2D().NoFilter(), results);

        Destroy(tempObject); // Hủy object tạm thời

        return count == 0; // Nếu không có va chạm, vị trí hợp lệ
    }

    T GetRandomEnumValue<T>()
    {
        T[] values = (T[])System.Enum.GetValues(typeof(T)); // Lấy tất cả các giá trị của enum
        return values[UnityEngine.Random.Range(0, values.Length)];      // Chọn một giá trị ngẫu nhiên
    }
}
