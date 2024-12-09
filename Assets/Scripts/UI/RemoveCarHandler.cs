using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCarHandler : MonoBehaviour
{
    public void RemoveCar()
    {
        Singleton<ToolManager>.Instance.Remove();
    }
}
