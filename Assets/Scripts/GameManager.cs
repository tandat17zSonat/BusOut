using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] PlotManager plotManager;
    [SerializeField] QueuePassengerController queueManager;
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
}
