using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

public class QueuePassengerController : MonoBehaviour
{
    private QueuePassengerData queuePassengerData = new QueuePassengerData();
    [SerializeField] ObjectPool objectPool;
    [SerializeField] int queueSize = 17;

    private Queue<GameObject> currentQueue;
    private int remainNum;

    // Start is called before the first frame update
    void Start()
    {
        currentQueue = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void Rearrange()
    //{
    //    currentQueue = new Queue<GameObject>();
    //    for (int i = 0; i < queueSize; i++)
    //    {
    //        GameObject passenger = objectPool.GetObject();
    //        currentQueue.Enqueue(passenger);
    //        if (i > queuePassengerData.QueuePassenger.Count)
    //        {
    //            passenger.SetActive(false);
    //        }
    //        else
    //        {
    //            passenger.GetComponent<PassengerBehaviour>().MoveTo(queueSize - i - 1);
    //        }
    //    }
    //}

    public void EnqueuePassenger(int num)
    {
        CarColor color = CarColor.blue;
        queuePassengerData.EnqueuePassenger(color, num);

        for (int i = 0; i < num; i++)
        {
            if (currentQueue.Count < queueSize)
            {
                GameObject passenger = objectPool.GetObject();
                var controller = passenger.GetComponent<PassengerController>();

                var data = controller.PassengerData;
                data.Color = color;
                data.PositionIndex = currentQueue.Count;
                controller.SetData(data);

                currentQueue.Enqueue(passenger);
            }
            else
            {
                break;
            }
        }
        Debug.Log("QueueData -> Size: " + queuePassengerData.GetSize());
    }

    public void DequeuePassenger(int num)
    {
        if (queuePassengerData.GetSize() == 0)
        {
            Debug.Log("queue is empty");
            return;
        }

        for (int i = 0; i < num; i++)
        {
            GameObject passenger = currentQueue.Dequeue();
            objectPool.ReturnObject(passenger);
        }

        for (int i = 0; i < num; i++)
        {
            int nextIdx = num + currentQueue.Count;
            if (nextIdx < queuePassengerData.GetSize())
            {
                GameObject passenger = objectPool.GetObject();
                var controller = passenger.GetComponent<PassengerController>();

                var data = controller.PassengerData;
                data.Color = queuePassengerData.GetColorByIdx(nextIdx);
                data.PositionIndex = nextIdx;
                currentQueue.Enqueue(passenger);
            }
            else
            {
                break;
            }

        }

        foreach (var passenger in currentQueue.ToArray())
        {
            var controller = passenger.GetComponent<PassengerController>();

            var data = controller.PassengerData;
            data.PositionIndex = data.PositionIndex - num;
            controller.SetData(data);
        }

        this.queuePassengerData.DequeuePassenger(num);
        Debug.Log("QueueData -> Size: " + queuePassengerData.GetSize());
    }
}
