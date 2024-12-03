using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingPlotData
{
    private int level;
    private List<CarData> cars = new List<CarData>();

    public int Level { get => level; set => level = value; }
    public List<CarData> Cars { get => cars; set => cars = value; }

    public int GetNumberByColor(CarColor color)
    {
        int cnt = 0;
        foreach (CarData car in cars)
        {
            if(car.Color == color)
            {
                cnt++;
            }
        }
        return cnt;
    }
}
