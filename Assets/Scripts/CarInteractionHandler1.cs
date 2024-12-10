using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(PolygonCollider2D))]
public class CarInteractionHandler1 : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private PolygonCollider2D polygonCollider;

    private Vector3 delta;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();

        lineRenderer.enabled = false;
    }

    #region: Xử lý di chuyển xe bằng touch 
    private void OnMouseDown()
    {
        Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.delta = clickPoint - this.transform.position;

        Singleton<GameManager>.Instance.SelectedCar = gameObject;
    }
    #endregion
}