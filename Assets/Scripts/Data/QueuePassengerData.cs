using UnityEngine;

public class QueuePassengerData: BData
{
    private CustomQueue<GroupPassenger> queue = new CustomQueue<GroupPassenger>();
    public CustomQueue<GroupPassenger> QueuePassenger { get => queue; set => queue = value; }

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

    public void MergeQueue()
    {
        var tempQueue = queue;

        queue = new CustomQueue<GroupPassenger>();
        CarColor curColor = CarColor.black;
        int curNum = 0;
        while( tempQueue.Count > 0)
        {
            var temp = tempQueue.Dequeue();
            if(curNum == 0 || temp.color != curColor)
            {
                if(curNum != 0)
                {
                    var item = new GroupPassenger();
                    item.num = curNum;
                    item.color = curColor;
                    queue.Enqueue(item);
                }
                
                curNum = temp.num;
                curColor = temp.color;
            }
            else
            {
                curNum += temp.num;
                curColor = temp.color;
            }            
        }

        var lastItem = new GroupPassenger();
        lastItem.num = curNum;
        lastItem.color = curColor;
        queue.Enqueue(lastItem);
    }

}

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