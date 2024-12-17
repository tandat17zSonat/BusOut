using UnityEngine;

public class ToolButtonHandler : ButtonHandler
{
    [SerializeField] SharedDataSO sharedDataSO;

    public override void HandleClick(int level)
    {
        sharedDataSO.level = level;
    }
}
