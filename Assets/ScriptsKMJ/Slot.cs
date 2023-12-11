using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public Image image;
    public Card card;

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
        Debug.Log("들어옴");
        if (card != null)
        {
            card.Active();
        }
    }
}