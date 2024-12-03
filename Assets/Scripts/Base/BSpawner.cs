using UnityEngine;

public abstract class BSpawner : MonoBehaviour
{
    [SerializeField, Space(10)] protected GameObject prefab;
    [SerializeField] protected BController controller;

    public abstract void Create();
    public abstract void Remove();
}
