using TMPro;
using UnityEngine;

public class ScaleHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject go;

    public void ScaleTo()
    {
        float scale = 1.0f;
        if(float.TryParse(inputField.text, out scale))
        {
            go.transform.localScale = Vector3.one * scale;
        }
        else
        {
            return;
        }
    }

    public void ScaleBy(float delta)
    {
        Vector3 scale = gameObject.transform.localScale;
        go.transform.localScale = scale + Vector3.one * delta;
    }
}
