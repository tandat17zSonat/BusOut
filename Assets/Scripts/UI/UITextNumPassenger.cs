using TMPro;
using UnityEngine;

public class UITextNumPassenger : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        int num = Singleton<QueuePassengerController>.Instance.Data.GetSize();
        GetComponent<TMP_Text>().text = "Num: " + num;
    }
}
