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
                        foreach (KeyValuePair<string, List<GameObject>> buildingTileCheck in RoundManager.Instance.bird.hasBuildingDic)
                        {
                            NodeMember tile1 = RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == buildingTileCheck.Key);
                            List<NodeMember> tiles = new List<NodeMember>();
                            if(tile1.nodeType.Equals(birdCard[CurCard].costType))
                            {
                                tiles.Add(tile1);
                            }
                            if (birdCard[CurCard].costType == ANIMAL_COST_TYPE.BIRD)
                            {
                                RoundManager.Instance.testType = RoundManager.SoldierTestType.BirdSpawn;
                                tile1.gameObject.transform.GetComponent<Renderer>().material.color = Color.black;
                            }
                            else if (birdCard[CurCard].costType == tile1.nodeType)
                            {
                                for(int j = 0; j < tiles.Count; j++)
                                {
                                    RoundManager.Instance.testType = RoundManager.SoldierTestType.BirdSpawn;
                                    tiles[j].gameObject.transform.GetComponent<Renderer>().material.color = Color.black;
                                }
                            }
                            else
                            {
                                Debug.Log("들어옴4");
                                RoundManager.Instance.bird.NowLeader = LEADER_TYPE.NONE;
                                RoundManager.Instance.bird.BreakingRule();
                            }
                        }
                    }
                    break;
                case CARDSLOT_TYPE.MOVE:
                    {
                        Debug.Log("무브무브");
                        Debug.Log(birdCard.Count);
                        RoundManager.Instance.testType = RoundManager.SoldierTestType.MoveSelect;
                        foreach (KeyValuePair<string, List<Soldier>> soldierTileCheck in RoundManager.Instance.bird.hasSoldierDic)
                        {
                            NodeMember tile = RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == soldierTileCheck.Key);
                            tile.gameObject.transform.GetComponent<Renderer>().material.color = Color.blue;
                        }
                        //curCaed 를 올려줘야함
                    }
                    break;

                case CARDSLOT_TYPE.BATTLE:
                    {
                    }
                    break;
                case CARDSLOT_TYPE.BULID:
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
                    break;
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
