using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CarAnimation : MonoBehaviour
{
    [SerializeField] private float cyclelength = 2f;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(new Vector3(10, 0, 0), cyclelength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
