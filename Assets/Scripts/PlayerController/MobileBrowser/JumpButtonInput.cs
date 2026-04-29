using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonInput : MonoBehaviour, IPointerDownHandler
{
    private bool jumpPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        jumpPressed = true;
    }

    public bool ConsumeJump()
    {
        if (!jumpPressed)
            return false;

        jumpPressed = false;
        return true;
    }
}