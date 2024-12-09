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

        var queueData = (QueuePassengerData)this.data;
        int size = queueData.GetSize();
        for ( int  i = 0; i < queueSize; i++ )
        {
            if( i < size)
            {
                var obj = objectPool.GetObject();
                var controller = obj.GetComponent<BController>();

                var color = queueData.GetColorByIdx(i);
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
        this.data.EnqueuePassenger(color, num);
        this.Display();
        Debug.Log("Queue Size: " + this.data.GetSize());
    }

    public void Remove(int num)
    {
        this.data.DequeuePassenger(num);
        this.Display();
        Debug.Log("Queue Size: " + this.data.GetSize());
    }

}
