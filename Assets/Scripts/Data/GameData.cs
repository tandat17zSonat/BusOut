public class GameData
{
    int level;
    ParkingPlotData parkingPlotData;
    QueuePassengerData queuePassengerData;
    float scaleFactor = 1;

    public int Level { get => level; set => level = value; }
    public ParkingPlotData ParkingPlotData { get => parkingPlotData; set => parkingPlotData = value; }
    public QueuePassengerData QueuePassengerData { get => queuePassengerData; set => queuePassengerData = value; }
    public float ScaleFactor { get => scaleFactor; set => scaleFactor = value; }
}
