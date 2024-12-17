using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [SerializeField] SharedDataSO sharedDataSO;

    // Start is called before the first frame update
    void Start()
    {
        Singleton<GameManager>.Instance.Reset();
        Singleton<PlotManager>.Instance.SetTrigger(true);

        int level = sharedDataSO.level;
        Singleton<GameManager>.Instance.State = GameState.LOBBY;
        Singleton<GameManager>.Instance.Load(level);
    }

    public void EnableTool()
    {
        Singleton<GameManager>.Instance.Reset();
        Singleton<GameManager>.Instance.State = GameState.TOOL;
        Singleton<PlotManager>.Instance.SetTrigger(false);
    }
}
