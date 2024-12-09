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

        // Cấu hình LineRenderer
        lineRenderer.loop = true; // Kết nối điểm cuối với điểm đầu
        lineRenderer.startWidth = 0.25f; // Độ dày đường
        lineRenderer.endWidth = 0.02f;
        lineRenderer.useWorldSpace = true; // Sử dụng tọa độ thế giới
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Vật liệu mặc định
        lineRenderer.startColor = Color.green; // Màu đường (có thể thay đổi)
        lineRenderer.endColor = Color.green;

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
        UpdateOutline(); // Liên tục cập nhật đường viền theo vị trí của GameObject
    }
}