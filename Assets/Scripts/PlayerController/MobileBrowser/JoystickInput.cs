using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;

    public float handleRange = 80f;

    public Vector2 Direction { get; private set; }
    public bool IsUsingJoystick { get; private set; }

    private Vector2 startPosition;

    private void Start()
    {
        startPosition = background.anchoredPosition;
        ResetJoystick();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsUsingJoystick = true;
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        Vector2 clamped = Vector2.ClampMagnitude(localPoint, handleRange);

        handle.anchoredPosition = clamped;
        Direction = clamped / handleRange;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetJoystick();
    }

    private void ResetJoystick()
    {
        IsUsingJoystick = false;
        Direction = Vector2.zero;

        if (handle != null)
            handle.anchoredPosition = Vector2.zero;
    }
}