// using SJ;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : SingleTon<CardManager>
{
    public PlayerInventory playerInventory;
    public List<Card> cards = new List<Card>();//카드의 저장소
    public List<Card> cardDeck = new List<Card>();//카드들 덱
    [SerializeField]
    private Transform deckParent;

    private new void Awake()
    {
        base.Awake();
        cards = Resources.LoadAll<Card>("CardPrefabs").ToList();
        //Debug.Log(cards[0].damage);
    }
    private void Start()
    {
        //randomCard = Random.Range(0, 1);
        foreach(Card cardTemp in cards)
        {
            Card card = Instantiate(cardTemp, deckParent);
            RoundManager.Instance.cat.AddCard(card, card.costType);
            RoundManager.Instance.bird.AddCard(card, card.costType);
            RoundManager.Instance.wood.AddCard(card, card.costType);
        }
        
    }
}