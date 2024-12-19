using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [SerializeField] SharedDataSO sharedDataSO;

    // Start is called before the first frame update
    void Start()
    {
        Replay();
    }

    public void EnableTool()
    {
        Singleton<GameManager>.Instance.Reset();
        Singleton<GameManager>.Instance.State = GameState.TOOL;
        Singleton<PlotManager>.Instance.SetTrigger(false);
    }

    public void Replay()
    {
        Singleton<GameManager>.Instance.Reset();
        Singleton<PlotManager>.Instance.SetTrigger(true);

        int level = sharedDataSO.level;
        Singleton<GameManager>.Instance.Load(level);

        Singleton<GameManager>.Instance.StartPlay();
        
    }
}
