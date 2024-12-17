using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameState _state = GameState.TOOL;
    public GameState State { get => _state; set => _state = value; }

    [SerializeField, Space(10)] PlotManager plotManager;
    [SerializeField] QueuePassengerController queueManager;

    private GameObject selectedCar;
    public GameObject SelectedCar
    {
        get => selectedCar;
        set
        {
            selectedCar = value;
        }
    }

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

        //// hiển thị phần UI
        if (_state == GameState.TOOL) {
            Singleton<CellManager>.Instance.SetInfo();
            Singleton<ScaleHandler>.Instance.Scale = data.ScaleFactor;
        }
    }

    public void Save(int level)
    {
        // Save gameData to json 
        string filePath = $"Assets/Config/Level/{level}.json";
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore // Bỏ qua vòng lặp
        };

        string json = JsonConvert.SerializeObject(Data, Formatting.Indented, settings);
        File.WriteAllText(filePath, json);
        Debug.Log($"Parking plot saved to {filePath}");
    }

    public void Load(int level)
    {
        string filePath = $"Assets/Config/Level/{level}.json";
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Save file not found!");
            return;
        }

        // read
        string json = File.ReadAllText(filePath);
        var gameData = JsonConvert.DeserializeObject<GameData>(json);

        Data = gameData;
    }

    public void Reset()
    {
        selectedCar = null;

        plotManager.Reset();
        queueManager.Reset();
    }




    private void Update()
    {
        switch (_state)
        {
            case GameState.LOBBY:
                {
                    break;
                }
            case GameState.PREPARE:
                {
                    OnPrepareState();
                    break;
                }
            case GameState.PLAY:
                {
                    OnPlayState();
                    break;
                }
            case GameState.RESULT:
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
            var tupleRes = car.TryMove();
            if (tupleRes.Item1 != null) // Xe có đi được không?
            {
                car.Crash(tupleRes.Item2);

                //var collisionCar = tupleRes.Item1;
                //collisionCar.GetComponent<CarController>().Crash2();
                Debug.Log("-> Action: selectedCar - INVALID: can't move");
            }
            else
            {
                if (Singleton<SlotManager>.Instance.CheckEmptySlot()) // Còn slot cho xe đỗ không?
                {
                    Debug.Log("-> Action: selectedCar");

                    var slotController = Singleton<SlotManager>.Instance.GetFirstEmptySlot();
                    slotController.WaitingCar(car);

                    var destinationPoint = slotController.transform.position;
                    car.MoveToSlot(destinationPoint);
                }
                else
                {
                    Debug.Log("-> Action: selectedCar - INVALID: don't have slot");
                }
            }
            selectedCar = null;
        }

        // Kiểm tra hành khách đầu tiên lên xe được không? 
        var passenger = Singleton<QueuePassengerController>.Instance.GetFrontPassenger();
        if (passenger != null && CanPassengerGo(passenger))
        {
            var pData = passenger.Data as PassengerData;

            var cars = Singleton<SlotManager>.Instance.GetReadyCarByColor(pData.Color);
            var car = cars[0];

            car.IncreaseNum(1);

            // LƯU VÀO XE ĐỂ SAU THỰC HIỆN POOLING ---------------------------
            Singleton<QueuePassengerController>.Instance.MoveToCar(passenger, car);
        }

        // Kiểm tra xe full khách có thể rời khỏi chưa?
        if (CanCarLeave())
        {
            var slot = Singleton<SlotManager>.Instance.GetSlotHasFullCar(); ;
            var car = Singleton<SlotManager>.Instance.GetFullCar();
            // Xe rời đi thì PlotManager returnobject vào pool
            slot.Free();

            Singleton<PlotManager>.Instance.Leave(car);
        }

        if (CheckEndGame())
        {
            _state = GameState.RESULT;
        }

    }

    void OnResultState()
    {

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
            var pData = passenger.Data as PassengerData;

            var carByColor = Singleton<SlotManager>.Instance.GetReadyCarByColor(pData.Color);
            return carByColor.Count > 0;
        }
        return false;
    }

    bool CheckEndGame()
    {
        return false;
    }

    public void Play()
    {
        _state = GameState.PLAY;
        Singleton<PlotManager>.Instance.SetTrigger(true);

        int level = 1;
        Load(level);
    }
}

public enum GameState
{
    LOBBY,
    PREPARE,
    PLAY,
    RESULT,
    TOOL
}
