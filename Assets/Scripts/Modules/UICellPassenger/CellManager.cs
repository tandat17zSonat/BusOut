using System;
using TMPro;
using UnityEngine;

public class CellManager : Singleton<CellManager>
{
    [SerializeField] GameObject cells;
    [SerializeField] TMP_Text textUI;


    private int curPage = 1;
    private int maxPage = 1;
    private int numCell = 0;

    private CustomQueue<GroupPassenger> queue;
    private GameObject selectedCell;

    public GameObject SelectedCell 
    { 
        get => selectedCell;
        set
        {
            if( selectedCell) selectedCell.GetComponent<CellController>().SetSelected(false);
            selectedCell = value;
            selectedCell.GetComponent<CellController>().SetSelected(true);
        }
    }

    private void Start()
    {
        int i = 0;
        numCell = cells.transform.childCount;
        while (i < numCell)
        {
            cells.transform.GetChild(i % numCell).gameObject.SetActive(false);
            i++;
        }
    }

    public void SetInfo(bool backFirstPage = true)
    {
        var queueData = Singleton<QueuePassengerController>.Instance.Data;
        queue = queueData.QueuePassenger;
        if( backFirstPage) curPage = 1;
        numCell = cells.transform.childCount;
        Display();
    }

    void Display()
    {
        int i = 0;
        maxPage = (int)Math.Ceiling((double) queue.Count/numCell);

        foreach(var data in queue.ToList())
        {
            if (i < numCell * (curPage - 1))
            {
                i++;
                continue;
            }

            var child = cells.transform.GetChild(i % numCell);
            if (child != null)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<CellController>().SetInfo(data.color, data.num);
            }
            i++;

            if (i >= numCell * curPage) break;
        }

        if( i == 0) cells.transform.GetChild(0).gameObject.SetActive(false);

        i = i % numCell;
        while (i < numCell && i != 0)
        {
            cells.transform.GetChild(i % numCell).gameObject.SetActive(false);
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
        int oldIndexInQueue = (curPage - 1) * numCell + oldIndex,
            newIndexInQueue = (curPage - 1) * numCell + newIndex;
        Singleton<ToolManager>.Instance.UpdatePassenger(oldIndexInQueue, newIndexInQueue);
    }

    public void RemovePassengerGroup()
    {
        int idx = (curPage - 1) * numCell + selectedCell.transform.GetSiblingIndex();
        Singleton<ToolManager>.Instance.RemovePassengerGroup(idx);
    }

    public void DividePassengerGroup(int num)
    {
        int idx = (curPage - 1) * numCell + selectedCell.transform.GetSiblingIndex();

        Singleton<ToolManager>.Instance.DevidePassengerGroup(idx, num);

    }
}
