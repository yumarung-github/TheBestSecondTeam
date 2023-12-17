using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropableUI : MonoBehaviour, IDropHandler
{

    PointerEventData ped = new PointerEventData(null);
    private GraphicRaycaster gr = null;
    private bool isMove = false;
    List<RaycastResult> results;

    private void Start()
    {
        gr = GetComponent<GraphicRaycaster>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        results = new List<RaycastResult>();
        ped.position = Input.mousePosition;
        gr.Raycast(ped, results);
        if (results.Count <= 0 || results[1].gameObject == null)
            return;
        if (results[1].gameObject.name == "Frame")
            isMove = true;
        else
            isMove = false;
        if (eventData.pointerDrag != null)
        {
            if (isMove)
            {
                results[1].gameObject.transform.GetChild(0).GetComponent<BirdCardAction>().
                AddCard(eventData.pointerDrag.GetComponentInParent<Slot>().card);
                eventData.pointerDrag.GetComponentInParent<Slot>().EmptySlot();
            }
            else
                eventData.pointerDrag.GetComponentInParent<Slot>().UseCard();
        }
    }
}