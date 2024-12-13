using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class CarController : BController
{
    [SerializeField] CarScriptableObject carScriptableObject;

    [SerializeField, Space(10)] CarColor color = CarColor.black;
    [SerializeField] CarSize size = CarSize.four;
    [SerializeField] CarDirection direction = CarDirection.LB;

    private bool isStay = false;
    public bool IsStay { get => isStay; set => isStay = value; }

    CarState _state = CarState.PARKING;
    public CarState State { get => _state; set => _state = value; }


    public override void Init()
    {
        this.data = new CarData();
    }

    # region: Cập nhật info và hiển thị đúng
    public override void SetInfo(BData data)
    {
        base.SetInfo(data);

        var carData = data as CarData;
        color = carData.Color;
        size = carData.Size;
        direction = carData.Direction;

        transform.localPosition = carData.Position;
    }

    public override void Display()
    {
        Debug.Log("check car");
        // update sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite sprite = carScriptableObject.GetSprite((CarData) this.data);
        spriteRenderer.sprite = sprite;

        // update collider
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        collider.points = carScriptableObject.GetCollisionPoints((CarData) this.data);

        // lật object (vì phải lật ảnh)
        Vector3 currentScale = transform.localScale;
        var direction = ((CarData)this.data).Direction;
        if (direction == CarDirection.RB || direction == CarDirection.RT || direction == CarDirection.R)
        {
            currentScale.x = -Mathf.Abs(currentScale.x);
            transform.localScale = currentScale;
        }
        else
        {
            currentScale.x = Mathf.Abs(currentScale.x);
            transform.localScale = currentScale;
        }
    }
    # endregion

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.y);
        ((CarData)data).Position = transform.localPosition; // Lưu lại vị trí xe mỗi khi xe di chuyển
    }

    // Cập nhật khi chỉnh sửa ở editor
    private void OnValidate()
    {
        if (this.data == null) this.data = new CarData();
        ((CarData)this.data).SetData(color, size, direction);
        Display();
    }

    public bool CanPlayCar()
    {
        Vector2 boxSize = new Vector2(1f, 1f); // Kích thước hình hộp
        float maxDistance = 1000f; // Khoảng cách kiểm tra
        float angle = ((CarData) data).GetDirectionAngle();
        Vector2 direction = ((CarData)data).GetDirectionVector();

        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, angle, direction, maxDistance);

        // Nếu va chạm
        bool check = true;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                check = false;
                break;
            }
        }
        return check;
    }

    /// <summary>
    /// Xe di chuyển tới slot
    /// </summary>
    public void MoveToSlot(Vector2 destination)
    {
        Debug.Log("Xe di chuyển tới destination");
        State = CarState.MOVE;
        // Xe di chuyeen trong config.time_wait_car
        transform.position = destination;

        var cData = Data as CarData;
        cData.Direction = CarDirection.parking;
        Data = cData;
        Display();
        State = CarState.READY;
    }

    public void Leave()
    {
        // Xe di chuyen rời đi -------------
        State = CarState.LEAVE;
        return;
    }

    public int GetCurrentNum()
    {
        return 0;
    }
}

public enum CarState
{
    PARKING,
    MOVE,
    READY,
    LEAVE
}
