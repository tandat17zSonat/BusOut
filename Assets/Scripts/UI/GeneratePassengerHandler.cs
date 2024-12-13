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
        if( int.TryParse(inputNum.text, out num )  == false)
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

    public void Generate()
    {
        int num = 0;
        if (int.TryParse(inputNum.text, out num) == false)
        {
            var plotData = Singleton<PlotManager>.Instance.Data;
            foreach( var car in plotData.Cars)
            {
                num += (int)car.Size;
            }

        }
        Singleton<ToolManager>.Instance.GeneratePassenger(num);

    }
}
