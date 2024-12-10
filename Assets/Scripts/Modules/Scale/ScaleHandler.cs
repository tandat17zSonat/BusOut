using TMPro;
using UnityEngine;

public class ScaleHandler : Singleton<ScaleHandler>
{
    public TMP_InputField inputField;
    public GameObject go;

    private float scale = 1f;

    public float Scale 
    { 
        get => scale;
        set
        {
            scale = value;
            var newScale = Vector3.one * scale;
            inputField.text = newScale.x.ToString();
            go.transform.localScale = newScale;
        }
    }

    private void Start()
    {
        Scale = Singleton<GameManager>.Instance.Data.ScaleFactor;
    }

    public void ScaleTo()
    {
        Scale = 1.0f;
        if(float.TryParse(inputField.text, out scale))
        {
            Singleton<GameManager>.Instance.Data.ScaleFactor = scale;
            go.transform.localScale = Vector3.one * scale;
        }
        else
        {
            return;
        }
    }

    public void ScaleBy(float delta)
    {
        float oldScale = Singleton<GameManager>.Instance.Data.ScaleFactor ;

        Scale = oldScale + delta;
        Singleton<GameManager>.Instance.Data.ScaleFactor = Scale;
    }
}
