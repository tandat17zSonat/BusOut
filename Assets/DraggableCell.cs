using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableCell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Transform originalParent;
    private Vector2 originalPosition;

    private int oldIndex = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        Singleton<CellManager>.Instance.SelectedCell = this.gameObject;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Lưu lại vị trí và cha gốc
        originalParent = transform.parent;
        originalPosition = transform.localPosition;
        oldIndex = transform.GetSiblingIndex();

        // Đưa cell ra ngoài Layout Group để kéo tự do
        transform.SetParent(originalParent.parent);
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

        Singleton<CellManager>.Instance.UpdateCellInfo(oldIndex, newIndex);
    }

    private int GetNewGridIndex()
    {
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
