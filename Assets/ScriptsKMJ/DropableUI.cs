using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    private RawImage image;
    private Color originColor;
    private void Start()
    {
        image = GetComponent<RawImage>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("드롭");
            eventData.pointerDrag.GetComponentInParent<Slot>().UseCard();
        }
    }
}
