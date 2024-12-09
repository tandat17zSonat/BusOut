using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] PlotManager plotManager;

    private GameData data;
    public GameData Data
    {
        get
        {
            var gameData = new GameData();
            gameData.ParkingPlotData = plotManager.PlotData;
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
        plotManager.PlotData = data.ParkingPlotData;
    }
}
