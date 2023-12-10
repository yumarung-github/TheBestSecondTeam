using SJ;
using System.Collections;
using System.Collections.Generic;
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
        instance = this;
    }
    private void Start()
    {
        randomCard = Random.Range(0, 1);
        player.AddCard(cards[randomCard], cards[randomCard].animalType, cards[randomCard].skillType);
    }
}
