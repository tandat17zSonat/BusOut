using System.IO;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Build.Content;

public class ToolManager : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField, Space(10)] TMP_InputField inputLevel;

    public void SaveToJson()
    {
        // Get level ------------------------------------
        int level;
        try
        {
            level = int.Parse(inputLevel.text);
        }
        catch
        {
            Debug.Log("ERROR: Nhập lại level");
            return;
        }

        // Save gameData to json 
        string filePath = $"Assets/Config/Level/{level}.json";
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore // Bỏ qua vòng lặp
        };
        var gameData = gameController.Data as GameData;
        string json = JsonConvert.SerializeObject(gameData, Formatting.Indented, settings);
        File.WriteAllText(filePath, json);
        Debug.Log($"Parking plot saved to {filePath}");
    }

    public void LoadFromJson()
    {
        // Get level ------------------------------------------------
        int level;
        try
        {
            level = int.Parse(inputLevel.text);
        }
        catch
        {
            Debug.Log("ERROR: Nhập lại level");
            return;
        }

        //--------------------------------------------------------------------
        string filePath = $"Assets/Config/Level/{level}.json";
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Save file not found!");
            return;
        }

        // read
        string json = File.ReadAllText(filePath);
        var gameData = JsonConvert.DeserializeObject<GameData>(json);

        gameController.SetInfo(gameData);
    }

    public void RemoveCar()
    {
        //if(carScriptableObject.SelectedCar == null)
        //{
        //    Debug.LogWarning("RemoveCar -> selectedcar == null");
        //    return;
        //}

        //GameObject.Destroy(carScriptableObject.SelectedCar.gameObject);
        //carScriptableObject.SelectedCar = null;
    }

}

