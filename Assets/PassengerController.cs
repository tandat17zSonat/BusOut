using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerController : MonoBehaviour
{
    [SerializeField] PassengerScriptableObject passengerScriptableObject;

    [SerializeField, Space(10)] CarColor color = CarColor.black;
    [SerializeField] PassengerDirection direction = PassengerDirection.left1;
    [SerializeField] bool isSeat = false;

    private PassengerData passengerData = new PassengerData();
    public PassengerData PassengerData { get => passengerData; set => passengerData = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetData(PassengerData passengerData)
    {
        this.PassengerData = passengerData;

        color = this.PassengerData.Color;
        direction = this.PassengerData.Direction;
        isSeat = this.PassengerData.IsSeat;
    }

    public void LoadView()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite sprite = passengerScriptableObject.GetSprite(this.PassengerData);
        spriteRenderer.sprite = sprite;
    }

    private void OnValidate()
    {
        this.PassengerData.SetData(color, direction, isSeat);
        LoadView();
    }
}
