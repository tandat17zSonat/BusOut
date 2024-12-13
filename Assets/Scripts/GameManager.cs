using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameState _state = GameState.LOBBY;
    public GameState State { get => _state; set => _state = value; }

    [SerializeField] PlotManager plotManager;
    [SerializeField] QueuePassengerController queueManager;

    [SerializeField] GameObject UITool;

    private GameObject selectedCar;
    public GameObject SelectedCar
    {
        get => selectedCar;
        set
        {
            selectedCar = value;
            var controller = selectedCar.GetComponent<CarController>();
            if (controller.CanPlayCar())
            {
                Debug.Log("Can't play");
            }
            else
            {
                Debug.Log("Can play");
            }
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

    public void Display()
    {
        plotManager.Data = data.ParkingPlotData;
        queueManager.Data = data.QueuePassengerData;

        // hiển thị phần UI
        Singleton<CellManager>.Instance.SetInfo();
        Singleton<ScaleHandler>.Instance.Scale = data.ScaleFactor;
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
        // Cần check đâm xe khách không nữa
        if (selectedCar != null && CanCarMove())
        {
            Debug.Log("GameManager: selectedCar");
            var slotController = Singleton<SlotManager>.Instance.GetEmptySlot();
            slotController.WaitingCar(selectedCar);

            var destinationPoint = slotController.transform.position;
            var carController = selectedCar.GetComponent<CarController>();
            carController.MoveToSlot(destinationPoint);

            selectedCar = null;

        }

        if (CanPassengerGo())
        {
            var pController = Singleton<QueuePassengerController>.Instance.GetFrontPassenger();
            var pData = pController.Data as PassengerData;

            var cars = Singleton<SlotManager>.Instance.GetCarByColor(pData.Color);
            var car = cars[0];
            car.IncreaseNum(1);
            pController.MoveToCar(car);
        }

        //if (CanCarLeave())
        //{
        //    var car = Singleton<SlotManager>.Instance.GetFullCar();
        //    // Xe rời đi thì PlotManager returnobject vào pool
        //    car.GetComponent<CarController>().Leave();
        //}

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

    bool CanCarMove()
    {
        // Singleton<SlotManager>.Instance duyệt qua các slot xem có ai EMPTY không?
        return Singleton<SlotManager>.Instance.CheckEmptySlot();
    }

    bool CanPassengerGo()
    {
        // Lấy màu khách
        // Lấy xe đang ở slot theo màu và còn trống
        // Check xem có xe không
        var passenger = Singleton<QueuePassengerController>.Instance.GetFrontPassenger();
        if (passenger.State != PassengerState.READY)
        {
            return false;
        }
        var pData = passenger.Data as PassengerData;
        
        var carByColor = Singleton<SlotManager>.Instance.GetCarByColor(pData.Color);
        return carByColor.Count > 0;
    }

    void AddCartoSlotQueue()
    {

    }

    bool CheckEndGame()
    {
        return false;
    }

    public void Play()
    {
        _state = GameState.PLAY;
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
