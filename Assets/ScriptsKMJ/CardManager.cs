// using SJ;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public Card card;
    public Player player;
    public PlayerInventory playerInventory;
    public List<Card> cards = new List<Card>();
    private int randomCard;

    private void Awake()
    {
        cards = Resources.LoadAll<Card>("CardPrefabs").ToList();
        instance = this;
        //Debug.Log(cards[0].damage);
    }
    private void Start()
    {
        randomCard = Random.Range(0, 1);
        Card card = Instantiate(cards[randomCard]);
        player.AddCard(card, card.animalType, card.skillType);
    }
}