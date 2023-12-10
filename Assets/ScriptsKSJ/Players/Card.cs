using CustomInterface;
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
    private ANIMAL_TYPE animalType;
    public BattleCard(Card card) : base(card)
    {
        this.damage = card.damage;
        this.defense = card.defense;
        this.animalType = card.animalType;
    }
    public override void UseCard()
    {
        if (GameManager.Instance.player.CardDeck.ContainsKey(animalType))
        {
            Debug.Log("내용채우기");
        }
    }
}
public class ProduceCard : CardStrategy
{
    private int cost;
    private ANIMAL_COST_TYPE costType;
    private Card owner;
    public ProduceCard(Card card) : base(card)
    {
        this.cost = card.cost;
        this.costType = card.costType;
        this.owner = card.owner;
    }

    public override void UseCard()
    {
        if (cost > GameManager.Instance.player.HaveAnimalMoney[costType])
        {
            Debug.Log("사용못함");
        }
        else
        {
            Debug.Log("사용 쌉가능");
            owner.DestroyObj();
        }
    }
}

public class Card : MonoBehaviour,IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Card owner;
    public List<CardStrategy> strategyList;
    public Sprite sprite;
    public int damage;
    public int defense;
    public int cost;
    public ANIMAL_COST_TYPE costType;
    public ANIMAL_TYPE animalType;

    public CARD_SKILL_TYPE skillType;
    public Skill skill;
    public void OnPointerDown(PointerEventData eventData)
    {
        //스킬 사용하는 곳 
        //임시로 디버깅로그로 정보만 띄움
        RoundManager.Instance.nowPlayer.selectedCard = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Uimanager.Instance.cardWindow.SetActive(true);
        Uimanager.Instance.cardName.text = "카드 이름 : " + skill.SkillName;
        Uimanager.Instance.cardInfo.text = "카드 정보 \n" + skill.SkillInfo;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Uimanager.Instance.cardWindow.SetActive(false);
    }
    public void Active()
    {
        foreach (CardStrategy strategy in strategyList)
        {
            strategy.UseCard();
        }
    }
    public void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}