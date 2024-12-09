using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class UIToolController : MonoBehaviour
{
    [SerializeField] TMP_InputField inputLevel;

    public void SaveToJson()
    {
        int level;
        int.TryParse(inputLevel.text, out level);

        // Save gameData to json 
        string filePath = $"Assets/Config/Level/{level}.json";
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore // Bỏ qua vòng lặp
        };
        var gameData = Singleton<GameManager>.Instance.Data;
        string json = JsonConvert.SerializeObject(gameData, Formatting.Indented, settings);
        File.WriteAllText(filePath, json);
        Debug.Log($"Parking plot saved to {filePath}");
    }

    public void LoadFromJson()
    {
        int level;
        int.TryParse(inputLevel.text, out level);

        string filePath = $"Assets/Config/Level/{level}.json";
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Save file not found!");
            return;
        }

        // read
        string json = File.ReadAllText(filePath);
        var gameData = JsonConvert.DeserializeObject<GameData>(json);

        Singleton<GameManager>.Instance.Data = gameData;
    }
}
