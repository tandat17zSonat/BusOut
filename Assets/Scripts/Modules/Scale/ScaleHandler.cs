using TMPro;
using UnityEngine;

public class ScaleHandler : Singleton<ScaleHandler>
{
    public TMP_InputField inputField;

    private float scale = 1f;

    public float Scale 
    { 
        get => scale;
        set
        {
            scale = value;
            var newScale = Vector3.one * scale;
            inputField.text = scale.ToString();
            Singleton<PlotManager>.Instance.gameObject.transform.localScale = newScale;
        }
    }

    private void Start()
    {
        Scale = Singleton<GameplayManager>.Instance.Data.ScaleFactor;
    }

    public void ScaleTo()
    {
        double sc;
        if (double.TryParse(inputField.text, out sc))
        {
            Scale = (float)sc;
            Singleton<GameplayManager>.Instance.Data.ScaleFactor = scale;
        }
        else
        {
            return;
        }
    }

    public void ScaleBy(float delta)
    {
        float oldScale = Singleton<GameplayManager>.Instance.Data.ScaleFactor ;

        Scale = oldScale + delta;
        Singleton<GameplayManager>.Instance.Data.ScaleFactor = Scale;
    }
}
