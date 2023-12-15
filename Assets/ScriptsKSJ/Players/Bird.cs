using CustomInterface;
using sihyeon;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SearchService;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public enum LEADER_TYPE
{
    NONE,
    ARCHITECT,
    PROPHET,
    COMMANDER,
    TYRANT
}




public class Bird : Player
{
    [SerializeField] 
    private LEADER_TYPE nowLeader;
    public GameObject[] prafab = new GameObject[4];
    BirdCardSlot birdCardSlot;

    int spawn = 0;
    int move = 1;
    int battle = 2;
    int bulid = 3;

    public bool isSpwaned;
    public bool isMoved;
    public bool isBattled;
    public bool isBuilded;

    public LEADER_TYPE NowLeader
    {
        get => nowLeader;

        set
        {
            nowLeader = value;
            switch (nowLeader)
            {
                case LEADER_TYPE.NONE:
                        
                    break;
                case LEADER_TYPE.ARCHITECT:
                    AddDiscipline(move);
                    AddDiscipline(spawn);
                    break;
                case LEADER_TYPE.PROPHET:
                    AddDiscipline(battle);
                    AddDiscipline(spawn);
                    break;
                case LEADER_TYPE.COMMANDER:
                    AddDiscipline(move);
                    AddDiscipline(battle);
                    break;
                case LEADER_TYPE.TYRANT:
                    AddDiscipline(move);
                    AddDiscipline(bulid);
                    break;
            }
        }
    }



    private new void Start()
    {
        base.Start();
        birdCardSlot = Uimanager.Instance.birdUI.birdSlot;
        // roundManager.bird = this;
        hasNodeNames.Add("생쥐1");
        NowLeader = LEADER_TYPE.NONE;

    }


    public override GameObject SpawnSoldier(string tileName, Transform targetTransform)
    {
        
        if (birdCardSlot.birdCard[birdCardSlot.CurCard].costType == RoundManager.Instance.mapController.nowTile.nodeType)
        {
            Vector3 tempVec = Vector3.zero;
            if (hasSoldierDic.ContainsKey(tileName))//병사가 존재하는지 체크
            {
                tempVec = new Vector3(hasSoldierDic[tileName].Count, 0, 0);//명수에 따라 소환하는 위치를 바꿔야해서
            }
            if(this.nowLeader == LEADER_TYPE.PROPHET)
            {
                GameObject dubleSoldier = Instantiate(prefabSoldier, targetTransform.position + tempVec, Quaternion.identity);
                SetHasNode(tileName, dubleSoldier.GetComponent<Soldier>());//그타일에 방금 만든 병사를 저장해줌.
                return dubleSoldier;//생성한 병사를 return시킴
            }
            GameObject addedSoldier = Instantiate(prefabSoldier, targetTransform.position + tempVec, Quaternion.identity);
            //더해줄 병사를 임의로 저장해주고
            SetHasNode(tileName, addedSoldier.GetComponent<Soldier>());//그타일에 방금 만든 병사를 저장해줌.
            birdCardSlot.CurCard++;
            return addedSoldier;//생성한 병사를 return시킴

        }
        isSpwaned = false;
        return null;
    }

    public override void SetHasBuildingNode(string tileName, Transform targetTransform, GameObject building)
    {
        if (birdCardSlot.birdCard[birdCardSlot.CurCard].costType == RoundManager.Instance.mapController.nowTile.nodeType)
        {
            hasBuildingDic[tileName].Add(building);
            BuildingManager.Instance.InstantiateBuilding(building,targetTransform);
            birdCardSlot.CurCard++;
        }
        else
            isBuilded = false;
    }

    public void AddDiscipline(int num)
    {
        GameObject temp = Instantiate(prafab[num], transform);
        temp.GetComponent<Discipline>().bird = this;
    }

}
