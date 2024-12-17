using DG.Tweening;
using Spine;
using UnityEngine;
using UnityEngine.UI;

public class PhysicHandler : MonoBehaviour
{
    public void OnPhysic(Toggle toggle)
    {
        bool isTrigger = toggle.isOn;
        Singleton<PlotManager>.Instance.SetTrigger(isTrigger);

        if(isTrigger == false)
        {
            var plotTransform = Singleton<PlotManager>.Instance.transform;

            foreach (Transform child in plotTransform)
            {
                if (child.gameObject.activeSelf == true)
                {
                    var seq = DOTween.Sequence();
                    seq.Append(child.transform.DOScale(Vector3.one * 1.2f, 0.2f));
                    seq.Append(child.transform.DOScale(Vector3.one, 0.2f));
                }
            }
        }
    }
}
