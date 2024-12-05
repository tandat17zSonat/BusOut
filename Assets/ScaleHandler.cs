using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScaleHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject gameObject;
    public void ScaleTo()
    {
        float scale = 1.0f;
        if(float.TryParse(inputField.text, out scale))
        {
            gameObject.transform.localScale = Vector3.one * scale;
        }
        else
        {
            return;
        }
    }

    public void ScaleBy(float delta)
    {
        Vector3 scale = gameObject.transform.localScale;
        gameObject.transform.localScale = scale + Vector3.one * delta;
    }
}
