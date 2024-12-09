using UnityEngine;

public class CarInteractionHandler : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private PolygonCollider2D polygonCollider;

    private Vector3 delta;

    private void Awake()
    {
        // Lấy các component cần thiết
        lineRenderer = GetComponent<LineRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();

        lineRenderer.enabled = false; // Ẩn đường viền lúc đầu
    }

    //----------------------------------------------------------------
    #region: Xử lý di chuyển xe bằng touch 
    private void OnMouseDown()
    {
        Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.delta = clickPoint - this.transform.position;

        Singleton<ToolManager>.Instance.SelectedCar = gameObject;
    }

    private void OnMouseDrag()
    {
        if (lineRenderer.enabled)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.delta;
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

            // Gán các điểm cho LineRenderer
            for (int i = 0; i < points.Length; i++)
            {
                Vector3 worldPoint = transform.TransformPoint(points[i]);
                lineRenderer.SetPosition(i, worldPoint);
            }
        }
    }

    private void Update()
    {
        UpdateOutline();
    }
}