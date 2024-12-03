using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] CarScriptableObject carScriptableObject;

    [SerializeField, Space(10)] CarColor color = CarColor.black;
    [SerializeField] CarSize size = CarSize.four;
    [SerializeField] CarDirection direction = CarDirection.LB;

    private CarData carData = new CarData();
    private Vector3 delta;

    public CarData CarData { get => carData; set => carData = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        carData.Position = transform.position; // Lưu lại vị trí xe mỗi khi xe di chuyển
    }

    // region: Xử lý di chuyển xe bằng touch --------------------------------------
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
    // endgion


    // region: Cập nhật info và hiển thị đúng ------------------------------------
    public void SetCarData(CarData carData)
    {
        this.CarData = carData;

        color = this.CarData.Color;
        size = this.CarData.Size;
        direction = this.CarData.Direction;

        transform.position = this.carData.Position;
    }

    public void LoadView()
    {
        // update sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite sprite = carScriptableObject.GetSprite(this.CarData);
        spriteRenderer.sprite = sprite;

        // update collider
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        collider.points = carScriptableObject.GetCollisionPoints(this.CarData);

        // lật object (vì phải lật ảnh)
        Vector3 currentScale = transform.localScale;
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

    /* Cập nhật khi chỉnh sửa ở editor
     */
    private void OnValidate()
    {
        this.CarData.SetData(color, size, direction);
        LoadView();
    }
    // endregion

}
