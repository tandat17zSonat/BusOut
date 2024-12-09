using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPopupController : MonoBehaviour
{
    public void SetActivePopup(bool active)
    {
        gameObject.SetActive(active);
    }

    public void DevidePassengerGroup(GameObject inputNum)
    {
        var input = inputNum.GetComponent<TMP_InputField>();
        int num = 1;
        if( int.TryParse(input.text, out num) )
        {
            Singleton<CellManager>.Instance.DividePassengerGroup(num);
        }
    }
}
