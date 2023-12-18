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

    public CARDSLOT_TYPE cardUse_type;
    public List<Card> birdCards;
    List<NodeMember> tiles;

    int foxCard = 0;
    int rabbitCard = 0;
    int ratCard = 0;
    int birdCard = 0;
    int curCard = 0;

    bool isBreakingRule;

    public bool IsBreakingRule
    {
        set 
        { 
            isBreakingRule = value;
            if (isBreakingRule == false)
                RoundManager.Instance.bird.BreakingRule();
        }
    }



    private void Awake()
    {
        countAnimals = GetComponentsInChildren<TextMeshProUGUI>();
        countAnimals[0].text = "x " + foxCard.ToString();
        countAnimals[1].text = "x " + rabbitCard.ToString();
        countAnimals[2].text = "x " + ratCard.ToString();
        countAnimals[3].text = "x " + birdCard.ToString();
    }

    private void Start()
    {
        resetButton.onClick.AddListener(CardReset);
        tiles = new List<NodeMember>();
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
        Debug.Log(card.costType);
    }
    public void AddCard(Card card)
    {
        Bird bird = RoundManager.Instance.bird;

        birdCards.Add(card);
        CountAnimals(card.costType);
    }
    public void Use()
    {
        for (int i = 0; i <= birdCards.Count - 1; i++)
        {
            switch (cardUse_type)
            {
                case CARDSLOT_TYPE.SPAWN:
                    {
                        Uimanager.Instance.birdUI.sequence.StartCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(0));
                        SetSpawnNode();
                        Uimanager.Instance.birdUI.sequence.StopCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(0));
                    }
                    break;
                case CARDSLOT_TYPE.MOVE:
                    {
                        Uimanager.Instance.birdUI.sequence.StartCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(1));
                        SetMoveNode();
                        Uimanager.Instance.birdUI.sequence.StopCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(1));
                    }
                    break;

                case CARDSLOT_TYPE.BATTLE:
                    {
                        Uimanager.Instance.birdUI.sequence.StartCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(2));
                        SetBattleNode();
                        Uimanager.Instance.birdUI.sequence.StopCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(2));
                    }
                    break;
                case CARDSLOT_TYPE.BULID:
                    {
                        Uimanager.Instance.birdUI.sequence.StartCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(3));
                        SetBulidNode();
                        Uimanager.Instance.birdUI.sequence.StopCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(3));
                    }
                    break;
            }
        }
    }


    public void SetBattleNode()
    {
        tiles.Clear();

        foreach (KeyValuePair<string, List<Soldier>> battleTileCheck in RoundManager.Instance.bird.hasSoldierDic)
        {
            NodeMember tile = RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == battleTileCheck.Key);
            bool isbattlePlayer = RoundManager.Instance.cat.hasSoldierDic.ContainsKey(tile.nodeName) || RoundManager.Instance.wood.hasSoldierDic.ContainsKey(tile.nodeName);
            bool isBattletile = birdCards[CurCard].costType == tile.nodeType;

            if (birdCards[CurCard].costType == ANIMAL_COST_TYPE.BIRD && isbattlePlayer)
            {
                tile.isTileCheck = true;
                tile.gameObject.transform.GetComponent<Renderer>().material.color = Color.gray;
                RoundManager.Instance.testType = RoundManager.SoldierTestType.Battle;
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
                tiles[i].gameObject.transform.GetComponent<Renderer>().material.color = Color.gray;
                /*            if (RoundManager.Instance.mapController.nowTile != tiles[i])
                            {
                                SetBattleNode();
                            }*/
                tiles[i].isTileCheck = true;
                RoundManager.Instance.testType = RoundManager.SoldierTestType.Battle;
            }

            if (tiles.Contains(RoundManager.Instance.mapController.nowTile))
                RoundManager.Instance.testType = RoundManager.SoldierTestType.Battle;
        }
        else if (tiles.Count > 0)
        {
            RoundManager.Instance.bird.NowLeader = LEADER_TYPE.NONE;
            RoundManager.Instance.bird.BreakingRule();
        }
    }
    public void SetBulidNode()
    {
        tiles.Clear();
        Debug.Log("빌드");
        foreach (KeyValuePair<string, List<Soldier>> soldierTileCheck in RoundManager.Instance.bird.hasSoldierDic)
        {
            NodeMember tile = RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == soldierTileCheck.Key);
            if (tile.nodeType.Equals(birdCards[CurCard].costType))
            {
                tiles.Add(tile);
            }
            if (birdCards[CurCard].costType == ANIMAL_COST_TYPE.BIRD)
            {
                RoundManager.Instance.testType = RoundManager.SoldierTestType.Build;
                tile.gameObject.transform.GetComponent<Renderer>().material.color = Color.cyan;
                tile.isTileCheck = true;

            }
        }
        if (tiles.Count == 0)
        {
            Debug.Log("빌드 브레이킹 룰");
            RoundManager.Instance.bird.NowLeader = LEADER_TYPE.NONE;
            RoundManager.Instance.bird.BreakingRule();
        }
        for (int j = 0; j < tiles.Count; j++)
        {
            RoundManager.Instance.testType = RoundManager.SoldierTestType.Build;
            tiles[j].gameObject.transform.GetComponent<Renderer>().material.color = Color.cyan;
            tiles[j].isTileCheck = true;

        }

    }
    public void SetMoveNode()
    {
        tiles.Clear();
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
                tile.gameObject.transform.GetComponent<Renderer>().material.color = Color.blue;
                tile.isTileCheck = true;

            }
            else if (birdCards[CurCard].costType == tile.nodeType)
            {
                for (int j = 0; j < tiles.Count; j++)
                {
                    RoundManager.Instance.testType = RoundManager.SoldierTestType.MoveSelect;
                    tiles[j].gameObject.transform.GetComponent<Renderer>().material.color = Color.black;
                    tiles[j].isTileCheck = true;

                }
            }
            else
            {

                RoundManager.Instance.bird.NowLeader = LEADER_TYPE.NONE;
                RoundManager.Instance.bird.BreakingRule();
            }
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
                tile1.gameObject.transform.GetComponent<Renderer>().material.color = Color.black;
                tile1.isTileCheck = true;
            }
            else if (birdCards[CurCard].costType == tile1.nodeType)
            {
                for (int j = 0; j < tiles.Count; j++)
                {
                    RoundManager.Instance.testType = RoundManager.SoldierTestType.BirdSpawn;
                    tiles[j].gameObject.transform.GetComponent<Renderer>().material.color = Color.black;
                    tiles[j].isTileCheck = true;
                }
            }
            else
            {
                RoundManager.Instance.bird.NowLeader = LEADER_TYPE.NONE;
                RoundManager.Instance.bird.BreakingRule();
            }
        }
    }

    public void CardReset()
    {
        for (int i = 0; i < birdCards.Count - 1; i++)
        {
            birdCards.RemoveAt(i);
        }
    }

    public void CountAnimals(ANIMAL_COST_TYPE cost)
    {

        switch (cost)
        {
            case ANIMAL_COST_TYPE.FOX:
                foxCard++;
                Debug.Log(foxCard);
                countAnimals[0].text = "x " + foxCard.ToString();
                break;
            case ANIMAL_COST_TYPE.RABBIT:
                rabbitCard++;
                Debug.Log(rabbitCard);
                countAnimals[1].text ="x " + rabbitCard.ToString();
                break;
            case ANIMAL_COST_TYPE.RAT:
                ratCard++;
                Debug.Log(ratCard);
                countAnimals[2].text = "x " + ratCard.ToString();
                break;
            case ANIMAL_COST_TYPE.BIRD:
                birdCard++;
                Debug.Log(birdCard);
                countAnimals[3].text = "x " + birdCard.ToString();
                break;

        }

    }
    
}
