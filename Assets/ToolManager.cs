using System.IO;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    [SerializeField] CarScriptableObject carScriptableObject;

    [SerializeField, Space(10)]  GameObject parkingPlot;
    [SerializeField] GameObject queuePassengers;
    [SerializeField] CarSpawner carSpawner;


    void Start()
    {
        
    }

    public void SaveToJson(int level)
    {
        if (parkingPlot == null)
        {
            Debug.LogError("Parking plot is not assigned!");
            return;
        }

        // Tạo đối tượng lưu dữ liệu
        ParkingPlotData plotData = new ParkingPlotData();
        plotData.Level = level; 
        // Thu thập thông tin các xe con
        foreach (Transform carTransform in parkingPlot.transform)
        {
            CarData carData = carTransform.GetComponent<CarController>().CarData;
            plotData.Cars.Add(carData);
        }

        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore // Bỏ qua vòng lặp
        };

        string filePath = "Assets/Config/Level/" + level + ".json";
        // Serialize dữ liệu thành JSON và lưu vào file
        string json = JsonConvert.SerializeObject(plotData, Formatting.Indented, settings);
        File.WriteAllText(filePath, json);

        Debug.Log($"Parking plot saved to {filePath}");
    }

    public void LoadFromJson(int level)
    {
        string filePath = "Assets/Config/Level/" + level + ".json";
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("Save file not found!");
            return;
        }

        // Đọc nội dung file JSON
        string json = File.ReadAllText(filePath);
        ParkingPlotData plotData = JsonConvert.DeserializeObject<ParkingPlotData>(json);

        Debug.Log("ParkingPlot -> Load data: Level" + plotData.Level);
        foreach (Transform child in parkingPlot.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (CarData carData in plotData.Cars)
        {
            carSpawner.Create(carData);
        }
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
