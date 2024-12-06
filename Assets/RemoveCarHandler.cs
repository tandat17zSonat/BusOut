using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCarHandler : MonoBehaviour
{
    public void RemoveCar()
    {
        var selectedCar = Singleton<ToolManager>.Instance.SelectedCar;
        if (selectedCar != null)
        {
            GameObject.Destroy(selectedCar.gameObject);
            Singleton<ToolManager>.Instance.SelectedCar = null;
        }
    }
}
