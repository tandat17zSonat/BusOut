using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSharedDataSO", menuName = "SharedDataSO")]
public class SharedDataSO : ScriptableObject
{
    public int level;

    public void Reset()
    {
        level = 0;
    }
}
