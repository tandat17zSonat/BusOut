using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int poolSize = 20;
    [SerializeField] Queue<GameObject> pool;
    [SerializeField] GameObject parent;

    private void Awake()
    {
        pool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject go = Instantiate(prefab);
            go.transform.SetParent(parent.transform, false);
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
            obj.transform.SetParent(parent.transform, false);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    public void Reset()
    {
        foreach (Transform child in parent.transform)
        {
            if (child.gameObject.activeSelf == true)
            {
                ReturnObject(child.gameObject);
            }
        }
    }
}
