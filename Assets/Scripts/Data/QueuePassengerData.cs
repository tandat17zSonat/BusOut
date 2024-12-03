using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QueuePassengerData
{
    public struct GroupPassenger
    {
        public CarColor color;
        public int num;

        public GroupPassenger(CarColor color, int num)
        {
            this.color = color;
            this.num = num;
        }
    }

    private CustomQueue<GroupPassenger> queue = new CustomQueue<GroupPassenger>();
    public CustomQueue<GroupPassenger> QueuePassenger { get => queue; set => queue = value; }

    public void LoadData()
    {

    }

    // enqueue
    public void EnqueuePassenger(CarColor color, int num)
    {
        if (queue.Count > 0)
        {
            var back = queue.Back();
            if (back.color == color)
            {
                back.num += num;
                queue.SetBack(back);
                return;
            }
        }
        queue.Enqueue(new GroupPassenger(color, num));
    }

    // dequeue
    public void DequeuePassenger(int num)
    {
        while (queue.Count > 0)
        {
            var front = QueuePassenger.Peek();
            if (front.num >= num)
            {
                front.num -= num;
                queue.SetFront(front);
                break;
            }
            QueuePassenger.Dequeue();
        }
    }

    // Lấy số lượng khách đang chờ
    public int GetSize()
    {
        int cnt = 0;
        foreach (var group in queue.ToList())
        {
            cnt += group.num;
        }
        return cnt;
    }

    // Lấy màu của khách theo index
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

    public int GetNumberByColor(CarColor color)
    {
        int cnt = 0;
        foreach (var info in queue.ToList())
        {
            if (info.color == color) cnt += info.num;
        }
        return cnt;
    }

}