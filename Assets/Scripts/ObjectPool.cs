using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int poolSize = 20;
    [SerializeField] Queue<GameObject> pool;

    private void Awake()
    {
        pool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(prefab);
            go.SetActive(false);
            pool.Enqueue(go);
        }
    }

    public GameObject GetObject()
    {
        if( pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefab);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
