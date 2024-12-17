using Spine.Unity;
using UnityEngine;

public class ControlSkeleton : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation; // Kéo thả SkeletonAnimation vào đây
    public string animationName = "animation"; // Animation bạn muốn chạy
    public bool loop = false;

    public void PlaySkeletonAnimation()
    {
        if (skeletonAnimation != null)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, animationName, loop);
        }
        else
        {
            Debug.LogWarning("No SkeletonAnimation");
        }
    }
}
