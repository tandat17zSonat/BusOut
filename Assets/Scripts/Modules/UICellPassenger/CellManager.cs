using System;
using TMPro;
using UnityEngine;

public class CellManager : Singleton<CellManager>
{
    [SerializeField] GameObject cells;
    [SerializeField] TMP_Text textUI;


    private int curPage = 1;
    private int maxPage = 1;
    private int NUM_CELL = 0;

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
        SetInfo();
    }

    public void SetInfo()
    {
        var queueData = Singleton<QueuePassengerController>.Instance.Data;
        queue = queueData.QueuePassenger;
        curPage = 1;
        NUM_CELL = cells.transform.childCount;
        Debug.Log(NUM_CELL);
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

            var child = cells.transform.GetChild(i % NUM_CELL);
            if (child != null)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<CellController>().SetInfo(data.color, data.num);
            }
            i++;

            if (i >= NUM_CELL * curPage) break;
        }

        if( i == 0) cells.transform.GetChild(0).gameObject.SetActive(false);

        i = i % NUM_CELL;
        while (i < cells.transform.childCount && i != 0)
        {
            cells.transform.GetChild(i % NUM_CELL).gameObject.SetActive(false);
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

    public void RemovePassengerGroup()
    {
        int idx = (curPage - 1) * NUM_CELL + selectedCell.transform.GetSiblingIndex();
        Singleton<ToolManager>.Instance.RemovePassengerGroup(idx);
    }

    public void DividePassengerGroup(int num)
    {
        int idx = (curPage - 1) * NUM_CELL + selectedCell.transform.GetSiblingIndex();

        Singleton<ToolManager>.Instance.DevidePassengerGroup(idx, num);

    }
}