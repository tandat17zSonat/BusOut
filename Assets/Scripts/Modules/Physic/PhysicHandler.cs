using UnityEngine;
using UnityEngine.UI;

public class PhysicHandler : MonoBehaviour
{
    public void OnPhysic(Toggle toggle)
    {
        bool isTrigger = toggle.isOn;
        Singleton<PlotManager>.Instance.SetTrigger(isTrigger);
    }
}
