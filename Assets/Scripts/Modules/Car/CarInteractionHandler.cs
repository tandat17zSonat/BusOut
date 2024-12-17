using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(PolygonCollider2D))]
public class CarInteractionHandler : MonoBehaviour
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

        if (Singleton<GameManager>.Instance.State == GameState.TOOL)
        {
            Singleton<ToolManager>.Instance.SelectedCar = gameObject;
        }

        Singleton<GameManager>.Instance.SelectedCar = gameObject;
    }

    private void OnMouseDrag()
    {
        if (Singleton<GameManager>.Instance.State == GameState.TOOL)
        {
            if (lineRenderer.enabled)
            {
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.delta;
            }
        }

    }

    #endregion

    private void UpdateOutline()
    {
        if (polygonCollider != null && lineRenderer.enabled == true)
        {
            Vector2[] points = polygonCollider.GetPath(0); // Lấy đường đầu tiên (nếu có nhiều đường, cần lặp)

            // Đặt số lượng điểm cho LineRenderer
            lineRenderer.positionCount = points.Length;
            for (int i = 0; i < points.Length; i++)
            {
                Vector3 worldPoint = transform.TransformPoint(points[i]);
                lineRenderer.SetPosition(i, worldPoint);
            }
        }
    }

    private void Update()
    {
        if (Singleton<GameManager>.Instance.State == GameState.TOOL)
        {
            UpdateOutline();
        }
            
    }
}