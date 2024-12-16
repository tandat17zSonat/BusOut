using UnityEngine;

[CreateAssetMenu(fileName = "NewConfigSO", menuName = "ConfigScriptableObject")]
public class ConfigSO : ScriptableObject
{
    public static int WIDTH_QUEUE_PASSENGER;
    public float DISTANCE_PASSENGER = 0.6f;

    public float TIME_CAR_MOVE = 1f;
    public float TIME_PASSENGER_TO_CAR = 0.5f;
    public float TIME_CAR_LEAVE = 3.0f;

    public int GetWidthQueuePassenger()
    {
        return WIDTH_QUEUE_PASSENGER;
    }
}
