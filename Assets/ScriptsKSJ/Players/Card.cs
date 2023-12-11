using CustomInterface;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class CardStrategy
{
    public Card card;
    public CardStrategy(Card card)
    {
        this.card = card;
    }
    public abstract void UseCard();
}
public class BattleCard : CardStrategy
{
    private int damage;
    private int defense;
    private ANIMAL_COST_TYPE costType;
    public BattleCard(Card card) : base(card)
    {
        this.damage = card.damage;
        this.defense = card.defense;
        this.costType = card.costType;
        Debug.Log(costType);
    }
    public override void UseCard()
    {
        //Debug.Log(costType);
        if (RoundManager.Instance.nowPlayer.CardDecks.ContainsKey(costType))
        {
            Debug.Log("내용채우기");
        }
        else
        {
            Debug.Log("없음");
        }
    }
}
public class ProduceCard : CardStrategy
{
    private int cost;
    private ANIMAL_COST_TYPE costType;
    public ProduceCard(Card card) : base(card)
    {
        this.cost = card.cost;
        this.costType = card.costType;
        Debug.Log(costType);
    }

    public override void UseCard()
    {
        if (cost > RoundManager.Instance.nowPlayer.HaveAnimalMoney[costType])
        {
            Debug.Log("사용못함");
        }
        else
        {
            Debug.Log("사용 쌉가능");
            card.DestroyObj();
        }
    }
}

public class Card : MonoBehaviour
{
    public CardStrategy cardStrategy;
    public Sprite sprite;
    public string cardName;
    public string cardInfo;
    public int damage;
    public int defense;
    public int cost;
    public ANIMAL_COST_TYPE costType;

    public CARD_SKILL_TYPE skillType;
    //public Skill skill;

    private void Start()
    {
        switch (skillType)
        {
            case CARD_SKILL_TYPE.BATTLE:
                cardStrategy = new BattleCard(this);
                break;
            case CARD_SKILL_TYPE.PRODUCE:
                cardStrategy = new ProduceCard(this);
                break;
        }
    }

    public void Active()
    {
            cardStrategy.UseCard();
            //Debug.Log("카드 액티브");
    }
    public void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}