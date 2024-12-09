using UnityEngine;
using UnityEngine.UI;

public class ParkingPlotController : BController
{
    [SerializeField] ObjectPool objectPool;

    public override BData Data
    {
        get
        {
            var plotData = new ParkingPlotData();
            foreach (Transform child in transform)
            {
                if (child.gameObject.activeSelf == true)
                {
                    var carData = child.gameObject.GetComponent<BController>().Data as CarData;
                    plotData.Cars.Add(carData);
                }
            }
            return plotData;
        }
        set
        {
            RemoveAll();
            SetInfo(value);
        }
    }
    public override void Init()
    {
        this.data = new ParkingPlotData();
    }

    public override void Display()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf == true)
            {
                objectPool.ReturnObject(child.gameObject);
            }
        }

        var plotData = this.data as ParkingPlotData;
        foreach (var carData in plotData.Cars)
        {
            var obj = objectPool.GetObject();
            var controller = obj.GetComponent<BController>();
            controller.SetInfo(carData);
        }
    }

    public void Add(CarData carData)
    {
        var obj = objectPool.GetObject();
        var controller = obj.GetComponent<BController>();
        controller.SetInfo(carData);

        var plotData = this.data as ParkingPlotData;
        plotData.Cars.Add(carData);
    }

    public void Remove(GameObject obj)
    {
        objectPool.ReturnObject(obj);
    }

    public void RemoveAll()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf == true)
            {
                objectPool.ReturnObject(child.gameObject);
            }
        }
        this.data = new ParkingPlotData();
    }

    public void SetScale(float scale)
    {

    }

    public void EnableTrigger(Toggle toggle)
    {
        bool enabled = toggle.isOn;
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Collider2D>().isTrigger = enabled;
        }
    }
}
