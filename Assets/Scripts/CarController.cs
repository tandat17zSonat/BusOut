using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] CarScriptableObject carScriptableObject;

    [SerializeField] CarColor color = CarColor.black;
    [SerializeField] CarSize size = CarSize.four;
    [SerializeField] CarDirection direction = CarDirection.LB;

    private CarData carData = new CarData();

    private Vector3 delta;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.delta = clickPoint - this.transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.delta;
    }

    public void SetCarData(CarData carData)
    {
        this.carData = carData;
    }

    public void LoadView()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite sprite = carScriptableObject.GetSprite(this.carData);
        spriteRenderer.sprite = sprite;

        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        collider.points = carScriptableObject.GetCollisionPoints(this.carData);

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

    private void OnValidate()
    {
        this.carData.SetData(color, size, direction);
        LoadView();
    }


    public void SetData(CarData carData)
    {
        this.carData = carData;

        color = this.carData.Color;
        size = this.carData.Size;
        direction = this.carData.Direction;

        this.LoadView();
    }
}