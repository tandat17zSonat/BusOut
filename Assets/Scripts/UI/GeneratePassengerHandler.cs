using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneratePassengerHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField inputNum;
    [SerializeField] ToggleGroupController toggleGroupColor;

    public void Add()
    {
        CarColor color = toggleGroupColor.GetSelectedToggle<CarColor>();
        int num = 0;
        if (int.TryParse(inputNum.text, out num) == false)
        {
            num = 1;
        }
        Singleton<ToolManager>.Instance.AddPassenger(color, num);

    }

    public void Remove()
    {
        int num = 0;
        if (int.TryParse(inputNum.text, out num) == false)
        {
            num = 1;
        }
        Singleton<ToolManager>.Instance.RemovePassenger(num);
    }

    public void AddRandomColor()
    {
        int num = 0;
        if (int.TryParse(inputNum.text, out num) == false)
        {
            var plotData = Singleton<PlotManager>.Instance.Data;
            foreach (var car in plotData.Cars)
            {
                num += (int)car.Size;
            }

        }
        Singleton<ToolManager>.Instance.GeneratePassenger(num);
    }

    public void Generate()
    {
        Singleton<QueuePassengerController>.Instance.RemoveAll();
        Dictionary<CarColor, int> color2num = new Dictionary<CarColor, int>();

        var plotData = Singleton<PlotManager>.Instance.Data;
        foreach (var car in plotData.Cars)
        {
            Singleton<QueuePassengerController>.Instance.Add(car.Color, (int)car.Size);
        }
    }
}
