using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButtonText : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform textRect;

    void Start()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            textRect.offsetMax = new Vector2(textRect.offsetMax.x, -34);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            textRect.offsetMax = new Vector2(textRect.offsetMax.x, 20);
        }
    }
}
