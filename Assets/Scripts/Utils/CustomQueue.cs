using System;
using System.Collections.Generic;

/*------------------------------------------------------------------------------
 * Viết lại cấu trúc dữ liệu queue:
 *      + peek()
 *      + Chỉnh sửa phần tử trong queue
 **/
public class CustomQueue<T>
{
    private LinkedList<T> list = new LinkedList<T>();
    public LinkedList<T> List { get => list; set => list = value; }

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

    public LinkedList<T> ToList()
    {
        return list;
    }

    public bool IsEmpty => list.Count == 0;

    public int Count => list.Count;
}

