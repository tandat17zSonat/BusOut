using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameState _state;

    [SerializeField] PlotManager plotManager;
    [SerializeField] QueuePassengerController queueManager;

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
        if (selectedCar != null && CanCarMove())
        {
            var slot = Singleton<SlotManager>.Instance.GetEmptySlot();
            slot.GetComponent<SlotController>().WaitingCar(selectedCar);

            var destinationPoint = slot.transform.position;
            var carController = selectedCar.GetComponent<CarController>();
            carController.MoveToSlot(destinationPoint);

        }

        selectedCar = null;
        if (CanPassengerGo())
        {
            var passenger = Singleton<QueuePassengerController>.Instance.GetFrontPassenger();
            var passengerController = passenger.GetComponent<PassengerController>();

            var data = passengerController.Data as PassengerData;
            var cars = Singleton<SlotManager>.Instance.GetCarByColor(data.Color);
            passengerController.MoveToCar(cars[0]);
        }

        if (CanCarGo())
        {
            var car = Singleton<SlotManager>.Instance.GetFullCar();
            car.GetComponent<CarController>().Leave();
        }

        if(CheckEndGame())
        {
            _state = GameState.RESULT;
        }

    }

    void OnResultState()
    {

    }

    bool CanCarGo()
    {
        // Xem xe ở trạng thái đầy chưa
        return false;
    }

    bool CanCarMove()
    {
        // Singleton<SlotManager>.Instance duyệt qua các slot xem có ai EMPTY không?
        return false;
    }

    bool CanPassengerGo()
    {
        // màu các xe đang chờ
        // QueuePassengerManager check xem khách đầu có lên đc màu nào khong
        // Khách đang ở state == ready
        return false;
    }

    void AddCartoSlotQueue()
    {

    }

    bool CheckEndGame()
    {
        return false;
    }
}

public enum GameState
{
    LOBBY,
    PREPARE,
    PLAY,
    RESULT
}
