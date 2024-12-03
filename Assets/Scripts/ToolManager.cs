using System.IO;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using TMPro;

public class ToolManager : MonoBehaviour
{
    [SerializeField] CarScriptableObject carScriptableObject;

    [SerializeField, Space(10)]  GameObject parkingPlot;
    [SerializeField] GameObject queuePassengers;

    [SerializeField, Space(10)] CarSpawner carSpawner;
    [SerializeField] PassengerSpawner passengerSpawner;

    [SerializeField] TMP_InputField inputLevel;

    public void SaveToJson()
    {
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
        
        
        if (parkingPlot == null)
        {
            Debug.LogError("Parking plot is not assigned!");
            return;
        }   
        
        //--------------------------------------------------------------------------
        // data passenger
        var controller = queuePassengers.GetComponent<QueuePassengerController>();
        var queueData = controller.QueuePassengerData;

        // data car
        ParkingPlotData plotData = new ParkingPlotData();
        plotData.Level = level; 
        foreach (Transform carTransform in parkingPlot.transform)
        {
            CarData carData = carTransform.GetComponent<CarController>().CarData;
            plotData.Cars.Add(carData);
        }

        // Tạo game data --------------------------
        GameData gameData = new GameData();
        gameData.ParkingPlotData = plotData;
        gameData.QueuePassengerData = queueData;

        // Save to json 
        string filePath = "Assets/Config/Level/" + level + ".json";
        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore // Bỏ qua vòng lặp
        };
        string json = JsonConvert.SerializeObject(gameData, Formatting.Indented, settings);
        File.WriteAllText(filePath, json);
        Debug.Log($"Parking plot saved to {filePath}");
    }

    public void LoadFromJson()
    {
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
        string filePath = "Assets/Config/Level/" + level + ".json";
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Save file not found!");
            return;
        }

        // read
        string json = File.ReadAllText(filePath);
        var gameData = JsonConvert.DeserializeObject<GameData>(json);

        // load parkingPlotData---------------
        var plotData = gameData.ParkingPlotData;
        Debug.Log("ParkingPlot -> Load data: Level" + plotData.Level);

        // display
        foreach (Transform child in parkingPlot.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (CarData carData in plotData.Cars)
        {
            carSpawner.Create(carData);
        }

        // load queuePassengerData -----------
        var queueData = gameData.QueuePassengerData;
        // display
        // - dequeue về rỗng
        var queueController = queuePassengers.GetComponent<QueuePassengerController>();
        queueController.QueuePassengerData = queueData; 
    }

    public void RemoveCar()
    {
        if(carScriptableObject.SelectedCar == null)
        {
            Debug.LogWarning("RemoveCar -> selectedcar == null");
            return;
        }

        GameObject.Destroy(carScriptableObject.SelectedCar.gameObject);
        carScriptableObject.SelectedCar = null;
    }
}

