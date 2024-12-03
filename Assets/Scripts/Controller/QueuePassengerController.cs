using System.Collections.Generic;
using UnityEngine;

public class QueuePassengerController : BController
{
    [SerializeField] ObjectPool objectPool;
    [SerializeField] int queueSize = 17;

    private Queue<GameObject> currentQueue;

    public override void Init()
    {
        this.data = new QueuePassengerData();
        this.currentQueue = new Queue<GameObject>();
    }

    public override void Display()
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
            
        }
    }

    public void Add(CarColor color, int num)
    {
        Debug.Log("Queue -> Add");
        var queuePassengerData = (QueuePassengerData)this.data;

        // update data
        queuePassengerData.EnqueuePassenger(color, num);
        SetInfo(queuePassengerData);

        Debug.Log("Queue Size: " + queuePassengerData.GetSize());
        Debug.Log("QueuePassengerController -> Add: " + color.ToString() + num);
    }

    public void Remove(int num)
    {
        Debug.Log("Queue -> Remove");
        var queuePassengerData = (QueuePassengerData)this.data;

        // update data
        queuePassengerData.DequeuePassenger(num);
        SetInfo(queuePassengerData);

        Debug.Log("Queue Size: " + queuePassengerData.GetSize());
    }

}
