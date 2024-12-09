using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableCell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private Vector2 originalPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Lưu lại vị trí và cha gốc
        originalParent = transform.parent;
        originalPosition = transform.localPosition;

        // Đưa cell ra ngoài Layout Group để kéo tự do
        transform.SetParent(originalParent.parent);
        Debug.Log("selected cell: " + name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Cập nhật vị trí của cell theo chuột
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Xác định vị trí mới trong lưới
        int newIndex = GetNewGridIndex();

        // Đưa cell trở lại Grid Layout Group
        transform.SetParent(originalParent);
        transform.SetSiblingIndex(newIndex);

        // Cập nhật lại bố cục
        LayoutRebuilder.ForceRebuildLayoutImmediate(originalParent.GetComponent<RectTransform>());

        //Singleton<ToolManager>.Instance.UpdatePassenger();
    }

    private int GetNewGridIndex()
    {
        // Lấy danh sách các cell khác trong Grid Layout Group
        float closestDistance = float.MaxValue;
        int closestIndex = 0;

        var rectTransform = GetComponent<RectTransform>();
        for (int i = 0; i < originalParent.childCount; i++)
        {
            RectTransform sibling = originalParent.GetChild(i) as RectTransform;
            float distance = Vector2.Distance(rectTransform.position, sibling.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }
}
