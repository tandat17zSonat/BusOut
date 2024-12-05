using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ToolManager : Singleton<ToolManager>
{
    [SerializeField] GameController gameController;
    [SerializeField, Space(10)] TMP_InputField inputLevel;

    [SerializeField] ToggleGroup toggleGroupColor;
    private GameObject selectedCar;

    public GameObject SelectedCar { get => selectedCar; set => selectedCar = value; }

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
        if ( selectedCar != null )
        {
            GameObject.Destroy(selectedCar.gameObject);
            selectedCar = null;

        }
    }

    public void DisplayCar()
    {
        if (selectedCar == null) return;

        var controller = selectedCar.GetComponent<CarController>();

        string strColor = toggleGroupColor.GetComponent<ToggleGroupController>().SelectedToggleName;
        CarColor color = (CarColor)Enum.Parse(typeof(CarColor), strColor);

        var newData = ((CarData)controller.Data);
        Debug.Log(newData.Color + "_" + newData.Size.ToString() + "_" + newData.Direction.ToString());

        newData.Color = color;
        newData.Size = CarSize.four;
        newData.Direction = CarDirection.L;

        controller.SetInfo(newData);
    }
}

