using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Image image;
    public Card card;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Uimanager.Instance.cardWindow.SetActive(true);
        Uimanager.Instance.cardName.text = "카드 이름 : " + card.cardName;
        Uimanager.Instance.cardInfo.text = "카드 정보 \n" + card.cardInfo;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Uimanager.Instance.cardWindow.SetActive(false);
    }
    public void SetItem(Card cardTemp)
    {
        card = cardTemp;
        image.sprite = card.sprite;
        if (card == null)
            image.sprite = null;
        else
        {
            image.sprite = card.sprite;
        }
    }
    public void UseCard()
    {
        //Debug.Log("들어옴");
        if (card != null)
        {
            card.Active();
        }
    }
}