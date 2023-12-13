using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Card card;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Uimanager.Instance.cardWindow.SetActive(true);
        if(card != null )
        {
            Uimanager.Instance.cardName.text = "TEST 카드 이름 : " + card.cardName;
            Uimanager.Instance.cardInfo.text = "카드 정보 \n" + card.cardInfo;
        }        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Uimanager.Instance.cardWindow.SetActive(false);
    }
    public void SetItem(Card cardTemp)
    {
        card = cardTemp;
        image.sprite = card.sprite;
        Color32 tempColor = image.color;
        Debug.Log(tempColor.a);
        image.color = new Color32(tempColor.r, tempColor.g, tempColor.b, 255);
        if (card == null)
            image.sprite = null;
        else
        {
            image.sprite = card.sprite;
        }
    }
    public void UseCard()
    {
        if (card != null)
        {
            card.Active();
            if (card.isUse == true)
            {
                EmptySlot();
            }
        }
    }
    public void EmptySlot()
    {
        image.sprite = null;
        card = null;
        RoundManager.Instance.cat.inven.SetSort();
    }
}