using System.Collections.Generic;

public class ParkingPlotData
{
    private List<CarData> cars = new List<CarData>();

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
