using UnityEngine;
using UnityEngine.InputSystem;

public class PressEdgeDebug : MonoBehaviour
{
    bool prev;

    void Update()
    {
        var p = Pointer.current;
        if (p == null) return;

        bool now = p.press.isPressed;
        if (!prev && now) Debug.Log("EDGE DOWN");
        if (prev && !now) Debug.Log("EDGE UP");
        prev = now;
    }
}
