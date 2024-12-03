using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BSpawner : MonoBehaviour
{
    [SerializeField, Space(10)] protected GameObject prefab;
    [SerializeField] protected GameObject parent;
    [SerializeField] protected BController controller;

    public abstract void Create();
}
