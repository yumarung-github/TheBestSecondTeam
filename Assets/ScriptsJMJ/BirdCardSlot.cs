using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CARDSLOT_TYPE
{
    MOVE,
    SPAWN,
    BATTLE,
    BULID
}

public class BirdCardSlot : MonoBehaviour
{
    public List<Card> birdCard;
    public CARDSLOT_TYPE cardUse_type;
    public Button resetButton;
    
    int curCard = 0;

    private void Start()
    {
        resetButton.onClick.AddListener(CardReset);
    }
    public int CurCard
    {
        get { return curCard; }
        set 
        { 
            curCard = value;
            if (curCard <= birdCard.Count - 1)
                curCard = birdCard.Count - 1;
        }
    }
    public void AddBirdCard(Card card)
    {
        birdCard.Add(card);
    }
    public void AddCard(Card card)
    {
        Bird bird = RoundManager.Instance.bird;
        GameObject prediscipline;

        birdCard.Add(card);
    }
    public void Use()
    {
        for (int i = 0; i < birdCard.Count - 1; i++)
        {

            switch (cardUse_type)
            {
                case CARDSLOT_TYPE.MOVE:
                    {
                        RoundManager.Instance.testType = RoundManager.SoldierTestType.Move;
                        //curCaed 를 올려줘야함
                    }
                    break;
                case CARDSLOT_TYPE.SPAWN:
                    {
                        RoundManager.Instance.testType = RoundManager.SoldierTestType.Spawn;
                    }
                    break;
                case CARDSLOT_TYPE.BATTLE:
                    {

                    }
                    break;
                case CARDSLOT_TYPE.BULID:
                    {
                        RoundManager.Instance.testType = RoundManager.SoldierTestType.Build;
                    }
                    break;
            }
        }
    }

    public void CardReset()
    {
        for(int i = 0; i < birdCard.Count -1; i++)
        {
            birdCard.RemoveAt(i);
        }
    }
}
