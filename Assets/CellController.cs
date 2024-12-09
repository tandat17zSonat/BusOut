using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    [SerializeField] ColorScriptableObject colorSO;
    [SerializeField] TMP_Text textUI;

    [SerializeField] CarColor color;
    [SerializeField] int num;

    public void SetInfo(CarColor color, int num)
    {
        this.color = color;
        this.num = num;

        Display();
    }

    private void Display()
    {
        GetComponent<Image>().sprite = colorSO.GetSprite(color);
        textUI.text = num.ToString();
    }

    private void OnValidate()
    {
        SetInfo(this.color, this.num);
    }

    public void SetSelected(bool isSelected)
    {
        if( isSelected)
        {
            textUI.color = Color.red;
        }
        else
        {
            textUI.color = Color.white;
        }
    }
}
