using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slot : MonoBehaviour
{
    public Image image;
    public Card card;

    public void SetItem(Card card)
    {
        this.card = card;
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
        }
    }
}