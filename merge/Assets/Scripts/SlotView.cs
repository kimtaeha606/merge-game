using UnityEngine;
using UnityEngine.EventSystems;
public sealed class SlotView : MonoBehaviour, IDropHandler
{
    [Header("Slot")]
    [SerializeField] private int index;


    [Header("Refs")]
    [SerializeField] private BoardUI boardUI;
    public int Index => index;

    public void SetIndex(int value) => index = value;

    public void OnDrop(PointerEventData eventData)
    {
        var go = eventData.pointerDrag;
        if (go == null)
        {
            Debug.Log($"[SlotView] Drop on {index} but pointerDrag is null");
            return;
        }

        var animalView = go.GetComponent<AnimalView>();
        if (animalView == null)
        {
            Debug.Log($"[SlotView] Drop on {index} but AnimalView not found on dragged");
            return;
        }

        Debug.Log($"[SlotView] Drop success: from {animalView.SlotIndex} -> to {index}");

        int from = animalView.SlotIndex;
        int to = index;

        boardUI.HandleDrop(from, to);
    }
}