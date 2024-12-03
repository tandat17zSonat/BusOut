using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

public class CarController : BController
{
    [SerializeField] CarScriptableObject carScriptableObject;

    [SerializeField, Space(10)] CarColor color = CarColor.black;
    [SerializeField] CarSize size = CarSize.four;
    [SerializeField] CarDirection direction = CarDirection.LB;

    private Vector3 delta;

    public override void Init()
    {
        this.data = new CarData();
    }

    //--------------------------------------------------------------------
    # region: Cập nhật info và hiển thị đúng
    public override void SetInfo(BData data)
    {
        base.SetInfo(data);

        var carData = data as CarData;
        color = carData.Color;
        size = carData.Size;
        direction = carData.Direction;

        transform.position = carData.Position;
    }

    public override void Display()
    {
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
        ((CarData)data).Position = transform.position; // Lưu lại vị trí xe mỗi khi xe di chuyển
    }

    //----------------------------------------------------------------
    #region: Xử lý di chuyển xe bằng touch 
    private void OnMouseDown()
    {
        Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.delta = clickPoint - this.transform.position;

        carScriptableObject.SelectedCar = this;
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.delta;
    }
    #endregion

    // Cập nhật khi chỉnh sửa ở editor
    private void OnValidate()
    {
        if (this.data == null) this.data = new CarData();
        ((CarData)this.data).SetData(color, size, direction);
        Display();
    }
    

}
