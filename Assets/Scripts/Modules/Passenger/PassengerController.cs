using Unity.VisualScripting;
using UnityEngine;

public class PassengerController : BController
{
    [SerializeField] PassengerScriptableObject passengerScriptableObject;

    [SerializeField, Space(10)] CarColor color = CarColor.black;
    [SerializeField] int positionIndex = 0;
    [SerializeField] bool isSeat = false;

    PassengerState _state = PassengerState.READY;
    public PassengerState State { get => _state; set => _state = value; }






    public override void Init()
    {
        this.data = new PassengerData();
    }

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
        Sprite sprite = passengerScriptableObject.GetSprite((PassengerData)this.data);
        spriteRenderer.sprite = sprite;

        transform.localPosition = GetPosition();
    }
    #endregion

    // Cập nhật khi chỉnh sửa ở editor
    private void OnValidate()
    {
        if (this.data == null) this.data = new PassengerData();
        ((PassengerData)this.data).SetData(color, positionIndex, isSeat);
        Display();
    }


    public Vector3 GetPosition()
    {
        var pIdx = ((PassengerData)this.data).PositionIndex;

        int cellX = pIdx - Config.WIDTH_QUEUE_PASSENGER > 0 ? Config.WIDTH_QUEUE_PASSENGER : pIdx;
        int cellY = pIdx - Config.WIDTH_QUEUE_PASSENGER > 0 ? pIdx - Config.WIDTH_QUEUE_PASSENGER : 0;

        float x = cellX * Config.DISTANCE_PASSENGER,
            y = cellY * Config.DISTANCE_PASSENGER;
        return new Vector3(x, y, y);
    }

    #region: Hành khách di chuyển lên xe
    public void MoveToCar(CarController car)
    {
        State = PassengerState.MOVING;
        Invoke("AfterMoveToCar", Config.TIME_PASSENGER_TO_CAR);

        // effect
        transform.position = car.transform.position;
    }

    private void AfterMoveToCar()
    {
        
        State = PassengerState.READY;
    }
    #endregion


    public bool IsReady()
    {
        return _state == PassengerState.READY;
    }
}

public enum PassengerState
{
    READY, // sẵn sàng lên xe
    MOVING, // Đang lên xe
    FINISH // đã lên xe
}
