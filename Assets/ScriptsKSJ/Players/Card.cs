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
            if (RoundManager.Instance.nowPlayer.cardDecks[costType].Exists(card => card.cardName == this.card.cardName))
            {
                Debug.Log("내용채우기");
                card.isUse = true;
                RoundManager.Instance.nowPlayer.cardDecks[costType].Remove(card);
            }
            else
            {
                Debug.Log(" 에러");
            }
        }
        else
        {
            Debug.Log("없음");
        }
    }
}
public class DefenseCard : CardStrategy
{
    private int cost;
    private ANIMAL_COST_TYPE costType;
    public DefenseCard(Card card) : base(card)
    {
        this.cost = card.cost;
        this.costType = card.costType;
    }

    public override void UseCard()
    {
        if (cost > RoundManager.Instance.nowPlayer.HaveAnimalMoney[costType])
        {
            Debug.Log("사용못함");
        }
        else
        {
            RoundManager.Instance.nowPlayer.cardDecks[costType].Remove(card);
            Debug.Log("사용 쌉가능");
            card.isUse = true;
        }
    }
}
public class GetScoreCard : CardStrategy
{
    private int cost;
    private ANIMAL_COST_TYPE costType;
    public GetScoreCard(Card card) : base(card)
    {
        this.cost = card.cost;
        this.costType = card.costType;
    }

    public override void UseCard()
    {
        if (cost > RoundManager.Instance.nowPlayer.HaveAnimalMoney[costType])
        {
            Debug.Log("사용 못함");
        }
        else
        {
            RoundManager.Instance.nowPlayer.cardDecks[costType].Remove(card);
            Debug.Log("사용 가능");
            card.isUse = true;
        }
    }
}

public class Card : MonoBehaviour
{
    public Action onActive;
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
            case CARD_SKILL_TYPE.DEFENSE:
                cardStrategy = new DefenseCard(this);
                break;
            case CARD_SKILL_TYPE.GETSCORE:
                cardStrategy = new GetScoreCard(this);
                break;
        }
    }

    public void Active()//김성진 수정함 카드 사용하면 사라지고 패에서 소트되는거 해야함
    {
        if(Uimanager.Instance.woodUi.cardUseType == WoodUi.CardUseType.BATTLE)
        {
            cardStrategy.UseCard();
            Uimanager.Instance.woodUi.cardUseType = WoodUi.CardUseType.NONE;
            Uimanager.Instance.playerUI.battleCardsWindow.SetActive(false);
        }
        else
        {
            if (RoundManager.Instance.nowPlayer is Cat)
            {
                RoundManager.Instance.nowPlayer.cardDecks[costType].Remove(this);
                isUse = true;
            }
            //if (RoundManager.Instance.nowPlayer is Bird && Uimanager.Instance.dropableUI.isMove == true)
            //    Uimanager.Instance.birdUI.birdSlot[Uimanager.Instance.birdUI.BirdInventory.curSlot].
            if (RoundManager.Instance.nowPlayer is Bird)
            {

                switch (Uimanager.Instance.woodUi.cardUseType)
                {
                    case WoodUi.CardUseType.NONE:
                        RoundManager.Instance.nowPlayer.cardDecks[costType].Remove(this);
                        isUse = true;
                        break;
                    case WoodUi.CardUseType.CRAFT:
                        RoundManager.Instance.nowPlayer.craftedCards.Add(this);
                        RoundManager.Instance.nowPlayer.cardDecks[costType].Remove(this);
                        Debug.Log("덱확인" + RoundManager.Instance.nowPlayer.cardDecks[costType].Count);
                        isUse = true;

                        Uimanager.Instance.woodUi.cardUseType = WoodUi.CardUseType.NONE;
                        break;
                }
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
                            RoundManager.Instance.nowPlayer.craftedCards.Count.ToString();//없애고 뽑는카드 숫자 넣기

                        RoundManager.Instance.nowPlayer.cardDecks[costType].Remove(this);
                        Debug.Log("덱확인" + RoundManager.Instance.nowPlayer.cardDecks[costType].Count);
                        isUse = true;                        
                        Uimanager.Instance.woodUi.cardUseType = WoodUi.CardUseType.NONE;
                        break;
                    case WoodUi.CardUseType.SUPPORT:
                        RoundManager.Instance.wood.supportVal[costType]++;//지지자추가      
                        RoundManager.Instance.wood.SetSupportUI(costType);

                        RoundManager.Instance.nowPlayer.cardDecks[costType].Remove(this);
                        isUse = true;

                        Debug.Log(RoundManager.Instance.wood.supportVal[costType]);
                        Uimanager.Instance.woodUi.cardUseType = WoodUi.CardUseType.NONE;
                        break;
                    case WoodUi.CardUseType.OFFICER:
                        RoundManager.Instance.wood.OfficerNum++;//장교추가
                        RoundManager.Instance.nowPlayer.cardDecks[costType].Remove(this);
                        isUse = true;

                        Uimanager.Instance.woodUi.cardUseType = WoodUi.CardUseType.NONE;
                        break;
                }
            }
        }
        

        onActive?.Invoke();
        //Debug.Log("카드 액티브");
    }
}