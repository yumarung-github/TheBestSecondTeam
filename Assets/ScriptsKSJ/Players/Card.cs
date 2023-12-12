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
        if (RoundManager.Instance.nowPlayer.cardDecks.ContainsKey(costType))
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
            //card.DestroyObj();
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
        if(RoundManager.Instance.nowPlayer is Bird)
        {

        }
        else if(RoundManager.Instance.nowPlayer is Wood)
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
                    if (!RoundManager.Instance.wood.supportVal.ContainsKey(costType))
                    {
                        RoundManager.Instance.wood.supportVal.Add(costType, 0);
                    }
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
        
            
            //Debug.Log("카드 액티브");
    }
    public void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}