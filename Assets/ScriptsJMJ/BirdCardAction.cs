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

    public void SetBattleNode()
    {
        RoundManager.Instance.testType = RoundManager.SoldierTestType.Battle;
        foreach (KeyValuePair<string, List<Soldier>> battleTileCheck in RoundManager.Instance.bird.hasSoldierDic)
        {
            NodeMember tile = RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == battleTileCheck.Key);
            bool isBattletile = birdCard[CurCard].costType == tile.nodeType;
            if (isBattletile) 
            {
                tile.gameObject.transform.GetComponent<Renderer>().material.color = Color.gray;
                BattleManager.Instance.InitBattle();
            }
            else
                RoundManager.Instance.bird.NowLeader = LEADER_TYPE.NONE;
        }
    }
    public void SetBulidNode()
    {
        //tiles.Clear();
        Debug.Log("빌드");
        foreach (KeyValuePair<string, List<Soldier>> soldierTileCheck in RoundManager.Instance.bird.hasSoldierDic)
        {
            NodeMember tile = RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == soldierTileCheck.Key);
            if (tile.nodeType.Equals(birdCard[CurCard].costType))
            {
                tiles.Add(tile);
            }
            if (birdCard[CurCard].costType == ANIMAL_COST_TYPE.BIRD)
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
            if (tile.nodeType.Equals(birdCard[CurCard].costType))
            {
                tiles.Add(tile);
            }
            if (birdCard[CurCard].costType == ANIMAL_COST_TYPE.BIRD)
            {
                RoundManager.Instance.testType = RoundManager.SoldierTestType.MoveSelect;
                tile.gameObject.transform.GetComponent<Renderer>().material.color = Color.blue;
                tile.isTileCheck = true;
                
            }
            else if (birdCard[CurCard].costType == tile.nodeType)
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
