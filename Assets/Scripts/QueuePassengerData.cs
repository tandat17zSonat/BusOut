using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QueuePassengerData
{
    public struct Pair
    {
        public CarColor color;
        public int num;

        public Pair(CarColor color, int num)
        {
            this.color = color;
            this.num = num;
        }
    }

    private CustomQueue<Pair> queue = new CustomQueue<Pair>();
    public CustomQueue<Pair> QueuePassenger { get => queue; set => queue = value; }

    public void LoadData()
    {

    }

    public void EnqueuePassenger(CarColor color, int num)
    {
        if (queue.Count > 0)
        {
            var back = queue.Back();
            if (back.color == color)
            {
                back.num += num;
                queue.SetBack(back);
                Debug.Log("enqueue -> size: " + GetSize());
                return;
            }
        }
        queue.Enqueue(new Pair(color, num));
        Debug.Log("enqueue -> size: " + GetSize());
    }

    public void DequeuePassenger(int num)
    {
        while (true)
        {
            var front = QueuePassenger.Peek();
            if (front.num > num)
            {
                front.num -= num;
                queue.SetFront(front);
                break;
            }
            QueuePassenger.Dequeue();
        }
    }

    public int GetSize()
    {
        int cnt = 0;
        foreach (var pair in queue.ToList())
        {
            cnt += pair.num;
        }
        return cnt;
    }

    public CarColor GetColorByIdx(int idx)
    {
        int cnt = 0;
        foreach (var info in queue.ToList())
        {
            cnt += info.num;
            if (cnt > idx) return info.color;
        }
        Debug.LogError("GetColorByIdx");
        return CarColor.black;
    }
}


//------------------------------------------------------------------------------
public class CustomQueue<T>
{
    private LinkedList<T> list = new LinkedList<T>();

    public void Enqueue(T item)
    {
        list.AddLast(item);
    }

    public T Dequeue()
    {
        if (list.Count == 0) throw new InvalidOperationException("Queue is empty.");
        T value = list.First.Value;
        list.RemoveFirst();
        return value;
    }

    public T Peek()
    {
        if (list.Count == 0) throw new InvalidOperationException("Queue is empty.");
        return list.First.Value;
    }

    public void SetFront(T item)
    {
        list.First.Value = item;
    }

    public T Back()
    {
        if (list.Count == 0) throw new InvalidOperationException("Queue is empty.");
        return list.Last.Value;
    }

    public void SetBack(T item)
    {
        list.Last.Value = item;
    }

    public void Update(Func<T, bool> predicate, Action<T> updateAction)
    {
        var node = list.First;
        while (node != null)
        {
            if (predicate(node.Value))
            {
                updateAction(node.Value);
                break;
            }
            node = node.Next;
        }
    }

    public LinkedList<T> ToList()
    {
        return list;
    }

    public bool IsEmpty => list.Count == 0;

    public int Count => list.Count;
}
