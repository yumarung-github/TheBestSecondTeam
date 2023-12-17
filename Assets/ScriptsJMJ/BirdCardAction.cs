using CustomInterface;
using System;
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

public class BirdCardAction : MonoBehaviour
{
    public Button resetButton;

    public CARDSLOT_TYPE cardUse_type;
    public List<Card> birdCard;
    List<NodeMember> tiles = new List<NodeMember>();

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

        birdCard.Add(card);
    }
    public void Use()
    {
        for (int i = 0; i <= birdCard.Count - 1; i++)
        {
            switch (cardUse_type)
            {
                case CARDSLOT_TYPE.SPAWN:
                    {
                        Uimanager.Instance.birdUI.sequence.StartCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(0));
                        SetSpawnNode();
                        Uimanager.Instance.birdUI.sequence.StartCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(0));
                    }
                    break;
                case CARDSLOT_TYPE.MOVE:
                    {
                        Uimanager.Instance.birdUI.sequence.StartCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(1));
                        SetMoveNode();
                        Uimanager.Instance.birdUI.sequence.StartCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(1));
                    }
                    break;

                case CARDSLOT_TYPE.BATTLE:
                    {
                        Uimanager.Instance.birdUI.sequence.StartCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(2));
                        Uimanager.Instance.birdUI.sequence.StartCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(2));
                    }
                    break;
                case CARDSLOT_TYPE.BULID:
                    {
                        Uimanager.Instance.birdUI.sequence.StartCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(3));
                        SetBulidNode();
                        Uimanager.Instance.birdUI.sequence.StartCoroutine(Uimanager.Instance.birdUI.sequence.PointCo(3));
                    }
                    break;
            }
        }
    }


    public void SetBulidNode()
    {
        RoundManager.Instance.testType = RoundManager.SoldierTestType.Build;
        foreach (KeyValuePair<string, List<Soldier>> soldierTileCheck in RoundManager.Instance.bird.hasSoldierDic)
        {
            NodeMember tile = RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == soldierTileCheck.Key);
            bool issoldierTile = birdCard[CurCard].costType == tile.nodeType;
            bool isbirdCardCheck = birdCard[CurCard].costType == ANIMAL_COST_TYPE.BIRD;
            bool ishasBuilding = RoundManager.Instance.bird.hasBuildingDic[soldierTileCheck.Key] == null;
            //병사가 위치한 타일들 체크 = tile
            if ((issoldierTile || isbirdCardCheck) && ishasBuilding)
            {
                tile.gameObject.transform.GetComponent<Renderer>().material.color = Color.cyan;
            }
            else
            {
                RoundManager.Instance.bird.NowLeader = LEADER_TYPE.NONE;
            }
        }
    }
    public void SetMoveNode()
    {
        RoundManager.Instance.testType = RoundManager.SoldierTestType.MoveSelect;
        foreach (KeyValuePair<string, List<Soldier>> soldierTileCheck in RoundManager.Instance.bird.hasSoldierDic)
        {
            NodeMember tile = RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == soldierTileCheck.Key);
            if (birdCard[CurCard].costType == ANIMAL_COST_TYPE.BIRD)
            {
                tile.gameObject.transform.GetComponent<Renderer>().material.color = Color.blue;
                tile.isTileCheck = true;

            }
            else if (birdCard[CurCard].costType == tile.nodeType)
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
    public void SetSpawnNode()
    {
        foreach (KeyValuePair<string, List<GameObject>> buildingTileCheck in RoundManager.Instance.bird.hasBuildingDic)
        {
            NodeMember tile1 = RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == buildingTileCheck.Key);
            if (tile1.nodeType.Equals(birdCard[CurCard].costType))
            {
                tiles.Add(tile1);
            }
            if (birdCard[CurCard].costType == ANIMAL_COST_TYPE.BIRD)
            {
                RoundManager.Instance.testType = RoundManager.SoldierTestType.BirdSpawn;
                tile1.gameObject.transform.GetComponent<Renderer>().material.color = Color.black;
                tile1.isTileCheck = true;
            }
            else if (birdCard[CurCard].costType == tile1.nodeType)
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
        for (int i = 0; i < birdCard.Count - 1; i++)
        {
            birdCard.RemoveAt(i);
        }
    }
}
