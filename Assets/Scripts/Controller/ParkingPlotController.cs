using UnityEngine;
using UnityEngine.UI;

public class ParkingPlotController : BController
{
    [SerializeField] ObjectPool objectPool;

    public override void Init()
    {
        this.data = new ParkingPlotData();
    }

    public override void Display()
    {
        objectPool.ResetActive(false);

        var plotData = this.data as ParkingPlotData; 
        foreach( var carData in plotData.Cars)
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
    }

    public void Remove()
    {
        Debug.Log("Remove Car ???");
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
