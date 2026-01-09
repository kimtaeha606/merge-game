using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.EventSystems;

public sealed class Animalview : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private int slotIndex;
    public int SlotIndex => slotIndex;

    [Header("Refs")]
    [SerializeField] private Canvas canvas;          // ÃÖ»ó´Ü Canvas
    [SerializeField] private CanvasGroup canvasGroup;

    private Transform originalParent;
    private Vector3 originalPosition;

    public void BindSlotIndex(int index) => slotIndex = index;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalPosition = transform.localPosition;

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false;

        if (canvas != null)
            transform.SetParent(canvas.transform, worldPositionStays: true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;

        if  (originalParent != null)
            transform.SetParent(originalParent,worldPositionStays:true);

        transform.position = originalPosition;
    }
}
