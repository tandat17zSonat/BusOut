using System.Collections.Generic;
using DG.Tweening;
using Newtonsoft.Json;
using Spine.Unity;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    private GameState _state = GameState.LOBBY;
    public GameState State { get => _state; set => _state = value; }

    [SerializeField, Space(10)] PlotManager plotManager;
    [SerializeField] QueuePassengerController queueManager;

    [SerializeField] SkeletonAnimation animStart;
    [SerializeField] PanelController panelResult;

    private GameData data;
    public GameData Data
    {
        get
        {
            var gameData = new GameData();
            gameData.ParkingPlotData = plotManager.Data;
            gameData.QueuePassengerData = queueManager.Data;
            gameData.ScaleFactor = Singleton<ScaleHandler>.Instance.Scale;
            return gameData;
        }
        set
        {
            data = value;
            Display();
        }
    }

    //--------------------------------------------------------------------
    public void Display()
    {
        plotManager.Data = data.ParkingPlotData;
        queueManager.Data = data.QueuePassengerData;

        plotManager.transform.localScale = Vector3.one * data.ScaleFactor;
        //// hiển thị phần UI
        if (_state == GameState.TOOL)
        {
            Singleton<CellManager>.Instance.SetInfo();
            Singleton<ScaleHandler>.Instance.Scale = data.ScaleFactor;
        }
    }

    public void StartPlay()
    {
        _state = GameState.PREPARE;
        animStart.state.SetAnimation(0, "animation", false);
        DOVirtual.DelayedCall(Config.TIME_PREPARE, () =>
        {
            _state = GameState.PLAY;
        });
    }


    // ------------------------------------------------------------------------------
    private GameObject selectedCar;
    public GameObject SelectedCar { get => selectedCar; set => selectedCar = value; }


    private Queue<SlotController> queueSlot = new Queue<SlotController>();
    public Queue<SlotController> QueueSlot { get => queueSlot; set => queueSlot = value; }


    public void Reset()
    {
        selectedCar = null;

        plotManager.Reset();
        queueManager.Reset();
        Singleton<SlotManager>.Instance.Reset();
        queueSlot = new Queue<SlotController>();
    }

    private void Update()
    {
        switch (_state)
        {
            case GameState.LOBBY:
                {
                    selectedCar = null;
                    break;
                }
            case GameState.PREPARE:
                {
                    OnPrepareState();
                    selectedCar = null;
                    break;
                }
            case GameState.PLAY:
                {
                    OnPlayState();
                    break;
                }
            case GameState.WIN:
            case GameState.LOSS:
                {
                    OnResultState();
                    break;
                }
        }
    }

    void OnPrepareState()
    {

    }

    void OnPlayState()
    {
        // ACTION
        if (selectedCar != null)
        {
            var car = selectedCar.GetComponent<CarController>();
            if (car.State == CarState.PARKING && Singleton<SlotManager>.Instance.CheckEmptySlot()) // Còn slot cho xe đỗ không?
            {
                Debug.Log("-> Action: selectedCar");

                var target = Singleton<SlotManager>.Instance.GetFirstEmptySlot();
                car.SetMove(target);

                target.SetCar(car); // waiting for ....
            }
            else
            {
                Debug.LogWarning("Don't have empty slot!!!");
            }
            selectedCar = null;
        }

        // Slot dang doi xe nhung xe crash -----------------
        while (queueSlot.Count > 0)
        {
            var slot = queueSlot.Dequeue();
            slot.Free();
        }


        // Kiểm tra hành khách đầu tiên lên xe được không? 
        var passenger = Singleton<QueuePassengerController>.Instance.GetFrontPassenger();
        if (passenger != null && CanPassengerGo(passenger))
        {
            var pData = passenger.Data as PassengerData;

            var cars = Singleton<SlotManager>.Instance.GetReadyCarByColor(pData.Color);
            var car = cars[0];

            // LƯU VÀO XE ĐỂ SAU THỰC HIỆN POOLING ---------------------------
            Singleton<QueuePassengerController>.Instance.MoveToCar(passenger, car);
            car.AddPassenger();
            car.ShowPassengerSeat();
        }

        // Kiểm tra xe full khách có thể rời khỏi chưa?
        if (CanCarLeave())
        {
            var slot = Singleton<SlotManager>.Instance.GetSlotHasFullCar(); ;
            var car = slot.GetCar();
            // Xe rời đi thì PlotManager returnobject vào pool
            slot.Free();

            Singleton<PlotManager>.Instance.Leave(car);
        }

        if (CheckEndGame())
        {
            Debug.Log("End game " + _state);
        }

    }

    void OnResultState()
    {
        panelResult.SetResult(_state == GameState.WIN);
    }

    bool CanCarLeave()
    {
        // Xem xe ở trạng thái đầy chưa
        return Singleton<SlotManager>.Instance.GetFullCar() != null;
    }

    bool CanPassengerGo(PassengerController passenger)
    {
        if (passenger.IsReady())
        {
            var pData = passenger.Data;

            var carByColor = Singleton<SlotManager>.Instance.GetReadyCarByColor(pData.Color);
            return carByColor.Count > 0;
        }
        return false;
    }

    bool CheckEndGame()
    {
        if (Singleton<QueuePassengerController>.Instance.Data.GetSize() == 0)
        {
            _state = GameState.WIN;
            return true;
        }
        if (Singleton<SlotManager>.Instance.CheckBusyAllSlot() == true &&
            Singleton<QueuePassengerController>.Instance.CheckStatusAllPassenger() == false)
        {
            _state = GameState.LOSS;
            return true;
        }
        return false;
    }
}

public enum GameState
{
    LOBBY,
    PREPARE,
    PLAY,
    WIN,
    LOSS,
    TOOL
}

public enum CarEvent
{
    CRASH,
    TO_SLOT
}
