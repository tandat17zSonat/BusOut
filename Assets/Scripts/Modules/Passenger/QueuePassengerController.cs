using System.Collections.Generic;
using UnityEngine;

public class QueuePassengerController : Singleton<QueuePassengerController>
{
    [SerializeField] ObjectPool objectPool;
    [SerializeField] int queueSize = 17;

    private QueuePassengerData data = new QueuePassengerData();
    private Queue<GameObject> currentQueue = new Queue<GameObject>();

    public QueuePassengerData Data 
    { 
        get => data; 
        set {
            data = value;
            Display();
        }
    }

    public void Display()
    {
        while( currentQueue.Count > 0 )
        {
            var obj = currentQueue.Dequeue();
            objectPool.ReturnObject(obj);
        }

        int size = data.GetSize();
        for ( int  i = 0; i < queueSize; i++ )
        {
            if( i < size)
            {
                var obj = objectPool.GetObject();
                var controller = obj.GetComponent<BController>();

                var color = data.GetColorByIdx(i);
                var iData = new PassengerData();
                iData.SetData(color, i);
                controller.SetInfo(iData);

                currentQueue.Enqueue(obj);
            }
            else
            {
                break;
            }
        }
    }

    public void Add(CarColor color, int num)
    {
        // update data
        data.EnqueuePassenger(color, num);
        Display();
    }

    public void Remove(int num)
    {
        data.DequeuePassenger(num);
        Display();
    }

    public void RemoveAll()
    {
        int size = data.GetSize();
        Remove(size);
    }

    #region: Hành khách lên xe
    public void MoveToCar(PassengerController passenger, CarDataController car)
    {
        passenger.MoveToCar(car);
        Invoke("AfterMoveToCar", Config.TIME_PASSENGER_TO_CAR);

        // tạo 1 hành khách trên xe
        var temp = GetTempPassenger();
        var pData = passenger.Data as PassengerData;
        pData.IsSeat = true;
        temp.SetInfo(pData);
        temp.MoveToCar(car);
        car.Passengers.Add(temp);
    }
    private void AfterMoveToCar()
    {
        Remove(1);
    }
    #endregion


    public void UpdatePassenger(int oldIndex, int newIndex)
    {
        data.QueuePassenger.MoveElement(oldIndex, newIndex);
        Display();
    }

    public PassengerController GetFrontPassenger()
    {
        if( currentQueue.Count == 0)
        {
            return null;
        }
        return currentQueue.Peek().GetComponent<PassengerController>();
    }

    public PassengerController GetTempPassenger()
    {
        return objectPool.GetObject().GetComponent<PassengerController>();
    }

    public void ReturnPassenger(GameObject obj)
    {
        objectPool.ReturnObject(obj);
    }

    public void Reset()
    {
        RemoveAll();
    }
}
