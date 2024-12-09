using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : Singleton<CellManager>
{
    [SerializeField] TMP_Text textUI;

    private CustomQueue<GroupPassenger> queue;
    int curPage = 1;
    int maxPage = 1;
    int NUM_CELL = 0;

    private void Start()
    {
        SetInfo();
    }

    public void SetInfo()
    {
        var queueData = Singleton<QueuePassengerController>.Instance.Data;
        queue = queueData.QueuePassenger;
        NUM_CELL = transform.childCount;
        Display();
    }

    void Display()
    {
        int i = 0;
        maxPage = (int)Math.Ceiling((double) queue.Count/NUM_CELL);

        foreach(var data in queue.ToList())
        {
            if (i < NUM_CELL * (curPage - 1))
            {
                i++;
                continue;
            }

            var child = transform.GetChild(i % NUM_CELL);
            if (child != null)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<CellController>().SetInfo(data.color, data.num);
            }
            i++;

            if (i >= NUM_CELL * curPage) break;
        }

        i = i % NUM_CELL;
        while (i < transform.childCount && i != 0)
        {
            transform.GetChild(i % NUM_CELL).gameObject.SetActive(false);
            i++;
        }

        textUI.text = "Page " + curPage + "/" + maxPage;
    }
    
    public void nextPage()
    {
        curPage++;
        if (curPage <= maxPage) Display();
        else curPage = maxPage;
    }

    public void prevPage()
    {
        curPage--;
        if (curPage > 0) Display();
        else curPage = 1;
    }

    public void UpdateCellInfo(int oldIndex, int newIndex)
    {
        int oldIndexInQueue = (curPage - 1) * NUM_CELL + oldIndex,
            newIndexInQueue = (curPage - 1) * NUM_CELL + newIndex;
        Singleton<ToolManager>.Instance.UpdatePassenger(oldIndexInQueue, newIndexInQueue);
    }
}
