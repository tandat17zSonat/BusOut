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
                    var oldScale = child.transform.localScale;
                    seq.Append(child.transform.DOScale(oldScale * 1.3f, 0.2f));
                    seq.Append(child.transform.DOScale(oldScale, 0.2f));
                }
            }
        }
    }
}
