using UnityEngine;
using UnityEngine.EventSystems;

// 인터페이스 상속이 반드시 필요합니다.
public sealed class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("State")]
    [SerializeField] private int slotIndex;
    public int SlotIndex => slotIndex;

    [Header("Refs")]
    [SerializeField] private Canvas canvas; 
    [SerializeField] private CanvasGroup canvasGroup;

    private RectTransform rectTransform;
    private Transform originalParent;
    private Vector3 originalPosition;

    private void Awake()
    {
        // UI 요소는 RectTransform을 사용하므로 캐싱해두면 좋습니다.
        rectTransform = GetComponent<RectTransform>();
    }

    public void BindSlotIndex(int index) => slotIndex = index;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = transform.position; // Local 대신 World Position 저장

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false; // 드래그 중 마우스가 자기 자신을 통과하게 함

        if (canvas != null)
        {
            // 드래그 중 최상위 캔버스로 옮겨 다른 UI에 가려지지 않게 합니다.
            transform.SetParent(canvas.transform, true);
            transform.SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Screen Space - Overlay인 경우 간단하게 위치 이동
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;

        // 원래 부모로 복귀
        if (originalParent != null)
            transform.SetParent(originalParent);

        // 원래 위치로 복귀 (슬롯에 안착하지 못한 경우 대비)
        transform.position = originalPosition;
    }
}