using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] SharedDataSO sharedDataSO;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    public void EnablePlay()
    {
        Replay();
    }

    public void EnableTool()
    {
        Singleton<GameplayManager>.Instance.State = GameState.TOOL;
        Singleton<PlotManager>.Instance.SetTrigger(false);
    }

    public void Replay()
    {
        Singleton<GameplayManager>.Instance.Reset();
        Singleton<PlotManager>.Instance.SetTrigger(true);

        int level = sharedDataSO.level;
        Load(level);

        Singleton<GameplayManager>.Instance.StartPlay();

    }

    public void Save(int level)
    {
        // Save gameData to json 
        string filePath = $"Assets/Config/Level/{level}.json";
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore // Bỏ qua vòng lặp
        };

        var data = Singleton<GameplayManager>.Instance.Data;
        string json = JsonConvert.SerializeObject(data, Formatting.Indented, settings);
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

        Singleton<GameplayManager>.Instance.Data = gameData;
    }
}
