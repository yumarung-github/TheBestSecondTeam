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
        foreach (Card cardTemp in cards)//같은거 건들여서 같은곳 참조해서 3번씩 써짐
        {
            Card card1 = Instantiate(cardTemp, deckParent);
            Card card2 = Instantiate(cardTemp, deckParent);
            Card card3 = Instantiate(cardTemp, deckParent);
            RoundManager.Instance.cat.AddCard(card1, card1.costType);
            RoundManager.Instance.bird.AddCard(card2, card2.costType);
            RoundManager.Instance.wood.AddCard(card3, card3.costType);
        }
        
    }
}