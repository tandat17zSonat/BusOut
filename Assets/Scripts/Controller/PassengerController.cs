using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerController : BController
{
    [SerializeField] PassengerScriptableObject passengerScriptableObject;

    [SerializeField, Space(10)] CarColor color = CarColor.black;
    [SerializeField] int positionIndex = 0;
    [SerializeField] bool isSeat = false;

    public override void Init()
    {
        this.data = new PassengerData();
    }

    //------------------------------------------------------------
    #region: Cập nhật info và hiển thị đúng
    public override void SetInfo(BData data)
    {
        base.SetInfo(data);

        var passengerData = (PassengerData)data;
        color = passengerData.Color;
        positionIndex = passengerData.PositionIndex;
        isSeat = passengerData.IsSeat;
    }

    public override void Display()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite sprite = passengerScriptableObject.GetSprite((PassengerData) this.data);
        spriteRenderer.sprite = sprite;

        transform.localPosition = GetPosition();
    }
    #endregion

    // Cập nhật khi chỉnh sửa ở editor
    private void OnValidate()
    {
        if( this.data == null) this.data = new PassengerData();
        ((PassengerData) this.data).SetData(color, positionIndex, isSeat);
        Display();
    }
    

    public Vector2 GetPosition()
    {
        var pIdx = ((PassengerData)this.data).PositionIndex;
        int cellX = pIdx - 12 > 0 ? 12 : pIdx;
        int cellY = pIdx - 12 > 0 ? pIdx - 12 : 0;
        return new Vector2(cellX * 0.6f, cellY * 0.6f);
    }
}
