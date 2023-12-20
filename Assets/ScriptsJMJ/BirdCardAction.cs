using CustomInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum CARDSLOT_TYPE
{
    MOVE,
    SPAWN,
    BATTLE,
    BULID
}

public class BirdCardAction : MonoBehaviour
{
    public Button resetButton;
    public TextMeshProUGUI[] countAnimals;

    private CARDSLOT_TYPE cardUse_type;
    public List<Card> copySlot;
    public List<Card> birdCards;
    public List<bool> isOver = new List<bool>();
    public List<NodeMember> tiles;
    public int curNum;

    int foxCard = 0;
    int rabbitCard = 0;
    int ratCard = 0;
    int birdCard = 0;
    int curCard = 0;

    bool isBreakRule;

    Coroutine actionCo;
    private void Awake()
    {
        countAnimals = GetComponentsInChildren<TextMeshProUGUI>();
        countAnimals[0].text = "x " + foxCard.ToString();
        countAnimals[1].text = "x " + rabbitCard.ToString();
        countAnimals[2].text = "x " + ratCard.ToString();
        countAnimals[3].text = "x " + birdCard.ToString();
        isOver.Add(false);
        curNum = 0;
    }

    private void Start()
    {
        resetButton.onClick.AddListener(CardReset);
        tiles = new List<NodeMember>();
        copySlot = new List<Card>();
        Uimanager.Instance.birdUI.nextButton.onClick.AddListener(() => { copySlot.Clear(); });
    }
    public int CurCard
    {
        get { return curCard; }
        set
        {
            curCard = value;
            if (curCard <= birdCards.Count - 1)
                curCard = birdCards.Count - 1;
        }
    }
    public void AddBirdCard(Card card)
    {
        birdCards.Add(card);
        CountAnimals(card.costType);
    }
    public void AddCard(Card card)
    {
        Bird bird = RoundManager.Instance.bird;
        birdCards.Add(card);
        CountAnimals(card.costType);
        isOver.Add(false);
        bird.inputCard++;
        if(bird.inputCard > 0)
            Uimanager.Instance.birdUI.nextButton.gameObject.SetActive(true);
        copySlot.Add(card);
    }    
    IEnumerator ActionCoroutine()
    {
        curNum = 0;
        while(curNum <= birdCards.Count - 1)
        {
            Debug.Log(curNum + " " + isOver.Count);
            while (true)
            {
                if(isOver[curNum])
                {
                    break;
                }

                yield return null;
            }
            curNum++;
            switch (cardUse_type)
            {
                case CARDSLOT_TYPE.SPAWN:
                    {
                        Debug.Log(cardUse_type + "종류" + curNum);
                        SetSpawnNode();
                    }
                    break;
                case CARDSLOT_TYPE.MOVE:
                    {
                        Debug.Log(cardUse_type + "종류" + curNum);
                        SetMoveNode();
                    }
                    break;

                case CARDSLOT_TYPE.BATTLE:
                    {
                        Debug.Log(cardUse_type + "종류" + curNum);
                        SetBattleNode();
                    }
                    break;
                case CARDSLOT_TYPE.BULID:
                    {
                        Debug.Log(cardUse_type + "종류" + curNum);
                        SetBulidNode();
                    }
                    break;
            }
            yield return null;
            
        }
        Debug.Log("규율 하나 끝");
        curNum = 0;
        yield return null;
    }
    public void StartActionCo()
    {
        Debug.Log("실행");
        actionCo = RoundManager.Instance.StartCoroutine(ActionCoroutine());
    }
    public void SetBattleNode()
    {
        tiles.Clear();
        isBreakRule = false;
        foreach (KeyValuePair<string, List<Soldier>> battleTileCheck in RoundManager.Instance.bird.hasSoldierDic)
        {
            NodeMember tile = RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == battleTileCheck.Key);
            bool isbattlePlayer = RoundManager.Instance.cat.hasSoldierDic.ContainsKey(tile.nodeName) || RoundManager.Instance.wood.hasSoldierDic.ContainsKey(tile.nodeName);
            bool isBattletile = birdCards[CurCard].costType == tile.nodeType;

            if (birdCards[CurCard].costType == ANIMAL_COST_TYPE.BIRD && isbattlePlayer)
            {
                tile.isTileCheck = true;
                RoundManager.Instance.SetEffect(tile);
                RoundManager.Instance.testType = RoundManager.SoldierTestType.Battle;
                isBreakRule = true;
            }
            else if (isBattletile)
            {
                tiles.Add(tile);
            }
        }
        if (tiles != null)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                RoundManager.Instance.SetEffect(tiles[i]);
                tiles[i].isTileCheck = true;
                RoundManager.Instance.testType = RoundManager.SoldierTestType.Battle;
                isBreakRule = true;
            }

            if (tiles.Contains(RoundManager.Instance.mapController.nowTile))
            {
                RoundManager.Instance.testType = RoundManager.SoldierTestType.Battle;
                isBreakRule = true;
            }
        }
        else if (!isBreakRule)
        {
            RoundManager.Instance.bird.NowLeader = LEADER_TYPE.NONE;
            RoundManager.Instance.bird.BreakingRule();
        }
    }
    public void SetBulidNode()
    {
        tiles.Clear();
        isBreakRule = false;
        Debug.Log("빌드");
        foreach (KeyValuePair<string, List<Soldier>> soldierTileCheck in RoundManager.Instance.bird.hasSoldierDic)
        {
            NodeMember tile = RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == soldierTileCheck.Key);
            if (tile.nodeType.Equals(birdCards[CurCard].costType))
            {
                tiles.Add(tile);
                isBreakRule = true;
            }
            if (birdCards[CurCard].costType == ANIMAL_COST_TYPE.BIRD)
            {
                RoundManager.Instance.testType = RoundManager.SoldierTestType.Build;
                RoundManager.Instance.SetEffect(tile);
                tile.isTileCheck = true;
                isBreakRule = true;
            }
        }
        
        if (!isBreakRule)
        {
            Debug.Log("빌드 브레이킹 룰");
            RoundManager.Instance.bird.NowLeader = LEADER_TYPE.NONE;
            RoundManager.Instance.bird.BreakingRule();
        }
        for (int j = 0; j < tiles.Count; j++)
        {
            RoundManager.Instance.testType = RoundManager.SoldierTestType.Build;
            RoundManager.Instance.SetEffect(tiles[j]);
            tiles[j].isTileCheck = true;

        }

    }
    public void SetMoveNode()
    {
        tiles.Clear();
        isBreakRule = false;
        foreach (KeyValuePair<string, List<Soldier>> soldierTileCheck in RoundManager.Instance.bird.hasSoldierDic)
        {
            NodeMember tile = RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == soldierTileCheck.Key);
            if (tile.nodeType.Equals(birdCards[CurCard].costType))
            {
                tiles.Add(tile);
            }
            if (birdCards[CurCard].costType == ANIMAL_COST_TYPE.BIRD)
            {
                RoundManager.Instance.testType = RoundManager.SoldierTestType.MoveSelect;
                RoundManager.Instance.SetEffect(tile);
                tile.isTileCheck = true;
                isBreakRule = true;
            }
            else if (birdCards[CurCard].costType == tile.nodeType)
            {
                for (int j = 0; j < tiles.Count; j++)
                {
                    RoundManager.Instance.testType = RoundManager.SoldierTestType.MoveSelect;
                    RoundManager.Instance.SetEffect(tiles[j]);
                    tiles[j].isTileCheck = true;
                    isBreakRule = true;
                }
            }
        }
        if (!isBreakRule)
        {
            RoundManager.Instance.bird.NowLeader = LEADER_TYPE.NONE;
            RoundManager.Instance.bird.BreakingRule();
        }
    }

    public void SetSpawnNode()
    {
        foreach (KeyValuePair<string, List<GameObject>> buildingTileCheck in RoundManager.Instance.bird.hasBuildingDic)
        {
            NodeMember tile1 = RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == buildingTileCheck.Key);
            if (tile1.nodeType.Equals(birdCards[CurCard].costType))
            {
                tiles.Add(tile1);
            }
            if (birdCards[CurCard].costType == ANIMAL_COST_TYPE.BIRD)
            {
                RoundManager.Instance.testType = RoundManager.SoldierTestType.BirdSpawn;
                RoundManager.Instance.SetEffect(tile1);
                tile1.isTileCheck = true;
                isBreakRule = true;
            }
            else if (birdCards[CurCard].costType == tile1.nodeType)
            {
                for (int j = 0; j < tiles.Count; j++)
                {
                    RoundManager.Instance.testType = RoundManager.SoldierTestType.BirdSpawn;
                    RoundManager.Instance.SetEffect(tiles[j]);
                    tiles[j].isTileCheck = true;
                    isBreakRule = true;
                }
            }
        }
        if (!isBreakRule)
        {
            RoundManager.Instance.bird.NowLeader = LEADER_TYPE.NONE;
            RoundManager.Instance.bird.BreakingRule();
        }
    }

    public void CardReset()
    {
        for (int i = 0; i < birdCards.Count - 1; i++)
        {
            birdCards.RemoveAt(i);
            isOver.Clear();
        }
    }

    public void CountAnimals(ANIMAL_COST_TYPE cost)
    {

        switch (cost)
        {
            case ANIMAL_COST_TYPE.FOX:
                foxCard++;
                countAnimals[0].text = "x " + foxCard.ToString();
                break;
            case ANIMAL_COST_TYPE.RABBIT:
                rabbitCard++;
                countAnimals[1].text ="x " + rabbitCard.ToString();
                break;
            case ANIMAL_COST_TYPE.RAT:
                ratCard++;
                countAnimals[2].text = "x " + ratCard.ToString();
                break;
            case ANIMAL_COST_TYPE.BIRD:
                birdCard++;
                countAnimals[3].text = "x " + birdCard.ToString();
                break;

        }

    }
    
}
