using UnityEngine;

public class CarCollisionController : MonoBehaviour
{
    public Vector2 boxSize = new Vector2(2f, 1f); // Kích thước hình hộp
    public float maxDistance = 1000f; // Khoảng cách kiểm tra
    public Vector2 direction = Vector2.right; // Hướng kiểm tra
    public LayerMask layerMask; // Lớp kiểm tra

    private LineRenderer lineRenderer;
    private PolygonCollider2D polygonCollider;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        // Bắt đầu BoxCast
        Vector2 pos = new Vector2(transform.position.x + 2, transform.position.y);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(pos, boxSize, 0f, direction, maxDistance);

        //// Vẽ vùng kiểm tra
        //DrawBoxCast(transform.position, direction, boxSize, maxDistance);

        // Nếu va chạm
        bool check = false;
        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                check = true;
                break;
            }
        }

        if (check)
        {
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }
        else
        {
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
        }
        UpdateOutline();
    }

    private void UpdateOutline()
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
