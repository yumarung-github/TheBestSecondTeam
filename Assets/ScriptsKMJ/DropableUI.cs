using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    private Image image;
    private Color originColor;
    private void Start()
    {
        image = GetComponent<Image>();
        originColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            image.color = new Color(1, 1, 0, 0.1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = originColor;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponentInParent<Slot>().UseCard();
        }
    }
}
