using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BController : MonoBehaviour
{
    protected BData data;

    public virtual BData Data {  get => data; set => data = value; }

    private void Awake()
    {
        Init();
    }
    public virtual void SetInfo(BData data)
    {
        this.data = data;
        this.Display();
    }

    public abstract void Init();
    public abstract void Display();
}
