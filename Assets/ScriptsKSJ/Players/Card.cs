using CustomInterface;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class CardStrategy
{
    public Card card;
    public bool isUse;
    public CardStrategy(Card card)
    {
        this.card = card;
        card.isUse = isUse;
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
    }
    public override void UseCard()
    {
        //Debug.Log(costType);
        if (RoundManager.Instance.nowPlayer.cardDecks.ContainsKey(costType))
        {
            Debug.Log("내용채우기");
            card.isUse = true;
            RoundManager.Instance.nowPlayer.cardDecks[costType].Remove(
                RoundManager.Instance.nowPlayer.cardDecks[costType].Find(card => card.cardName == this.card.cardName));
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
            RoundManager.Instance.nowPlayer.cardDecks[costType].Remove(
                RoundManager.Instance.nowPlayer.cardDecks[costType].Find(card => card.cardName == this.card.cardName));
            Debug.Log("사용 쌉가능");
            card.isUse = true;
        }
    }
}

public class Card : MonoBehaviour
{
    public event Action onActive;
    public CardStrategy cardStrategy;
    public Sprite sprite;
    public bool isUse;
    public string cardName;
    public string cardInfo;
    public int damage;
    public int defense;
    public int cost;
    public ANIMAL_COST_TYPE costType;

    public CARD_SKILL_TYPE skillType;


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

    public void Active()//김성진 수정함 카드 사용하면 사라지고 패에서 소트되는거 해야함
    {

        if (RoundManager.Instance.nowPlayer is Cat)
        {
            cardStrategy.UseCard();
        }
        if (RoundManager.Instance.nowPlayer is Bird)
        {
            cardStrategy.UseCard();
        }
        else if (RoundManager.Instance.nowPlayer is Wood)
        {
            switch (Uimanager.Instance.woodUi.cardUseType)
            {
                case WoodUi.CardUseType.NONE:
                    cardStrategy.UseCard();
                    break;
                case WoodUi.CardUseType.CRAFT:
                    RoundManager.Instance.nowPlayer.craftedCards.Add(this);
                    Uimanager.Instance.woodUi.craftCardText.text =
                        RoundManager.Instance.nowPlayer.craftedCards.Count.ToString();

                    RoundManager.Instance.nowPlayer.cardDecks[costType].Remove(this);
                    Uimanager.Instance.woodUi.cardUseType = WoodUi.CardUseType.NONE;
                    break;
                case WoodUi.CardUseType.SUPPORT:
                    RoundManager.Instance.wood.supportVal[costType]++;//지지자추가      
                    RoundManager.Instance.wood.SetSupportUI(costType);
                    RoundManager.Instance.nowPlayer.cardDecks[costType].Remove(this);
                    Debug.Log(RoundManager.Instance.wood.supportVal[costType]);
                    Uimanager.Instance.woodUi.cardUseType = WoodUi.CardUseType.NONE;
                    break;
                case WoodUi.CardUseType.OFFICER:
                    RoundManager.Instance.wood.OfficerNum++;//장교추가

                    Uimanager.Instance.woodUi.cardUseType = WoodUi.CardUseType.NONE;
                    break;
            }
        }

        onActive?.Invoke();
        //Debug.Log("카드 액티브");
    }
}