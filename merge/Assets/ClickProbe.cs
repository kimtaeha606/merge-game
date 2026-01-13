using UnityEngine;
using UnityEngine.EventSystems;

public class ClickProbe : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public void OnPointerDown(PointerEventData e) => Debug.Log("UI DOWN");
    public void OnPointerUp(PointerEventData e) => Debug.Log("UI UP");
    public void OnPointerClick(PointerEventData e) => Debug.Log("UI CLICK");
}
