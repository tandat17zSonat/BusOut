using System;
using System.Collections.Generic;
using System.Diagnostics;

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

    public void MoveElement(int fromIndex,  int toIndex)
    {
        if (fromIndex < 0 || fromIndex >= list.Count || toIndex < 0 || toIndex >= list.Count)
        {
            return;
        }

        // Lấy node tại vị trí fromIndex
        LinkedListNode<T> node = GetNodeAt(fromIndex);

        if (node == null) return;

        // Xóa node khỏi vị trí hiện tại
        list.Remove(node);

        // Tìm vị trí mới (toIndex)
        LinkedListNode<T> targetNode = GetNodeAt(toIndex);

        if (targetNode != null)
        {
            list.AddBefore(targetNode, node);
        }
        else
        {
            list.AddLast(node);
        }
    }

    public LinkedListNode<T> GetNodeAt(int index)
    {
        int currentIndex = 0;
        LinkedListNode<T> currentNode = list.First;

        while (currentNode != null)
        {
            if (currentIndex == index)
            {
                return currentNode;
            }

            currentIndex++;
            currentNode = currentNode.Next;
        }

        return null;
    }

    public bool IsEmpty => list.Count == 0;

    public int Count => list.Count;
}

