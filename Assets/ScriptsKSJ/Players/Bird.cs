using CustomInterface;
using sihyeon;
using System.Collections.Generic;
using UnityEngine;

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
    public Card birdCard;
    public BirdCardInventory inventory;
    public Building[] birdBuilding;
    int curIndex;
    int spawn = 0;
    int move = 1;
    int battle = 2;
    int build = 3;

    public bool isFirstCheck;
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
                    inventory.birdCardSlot[spawn].AddBirdCard(birdCard);
                    inventory.birdCardSlot[move].AddBirdCard(birdCard);
                    break;
                case LEADER_TYPE.PROPHET:
                    inventory.birdCardSlot[battle].AddBirdCard(birdCard);
                    inventory.birdCardSlot[spawn].AddBirdCard(birdCard);
                    break;
                case LEADER_TYPE.COMMANDER:
                    inventory.birdCardSlot[move].AddBirdCard(birdCard);
                    inventory.birdCardSlot[battle].AddBirdCard(birdCard);
                    break;
                case LEADER_TYPE.TYRANT:
                    inventory.birdCardSlot[move].AddBirdCard(birdCard);
                    inventory.birdCardSlot[build].AddBirdCard(birdCard);
                    break;
            }
        }
    }



    private new void Start()
    {
        base.Start();
        // roundManager.bird = this;
        hasNodeNames.Add("생쥐1");
        NowLeader = LEADER_TYPE.NONE;
        RoundManager.Instance.bird.hasBuildingDic.Add("여우2",new List<GameObject>()); // 테스트선진
        RoundManager.Instance.bird.hasBuildingDic.Add("여우4",new List<GameObject>()); // 테스트선진
    }


    public override GameObject SpawnSoldier(string tileName, Transform targetTransform)
    {
        //BirdCardSlot cardSlot = inventory.birdCardSlot[inventory.curSlot];
        //curIndex = inventory.birdCardSlot[inventory.curSlot].CurCard;
        //Debug.Log(inventory.birdCardSlot[inventory.curSlot]);
        //Debug.Log(curIndex);
        //Debug.Log(RoundManager.Instance.mapController.nowTile.nodeType);
        //bool isCheckBird = cardSlot.birdCard[curIndex].costType == ANIMAL_COST_TYPE.BIRD;
        //bool isCheckType = cardSlot.birdCard[curIndex].costType == RoundManager.Instance.mapController.nowTile.nodeType;
        // 안됨 이유 모름 - 선진
        bool isCheckBuilding = hasBuildingDic.Count > 0;

        if (isFirstCheck)
            base.SpawnSoldier(tileName, targetTransform);
        else if (isCheckBuilding /*&& (isCheckBirdisCheckType)*/)
        {
            Vector3 tempVec = Vector3.zero;
            if (hasSoldierDic.ContainsKey(tileName))//병사가 존재하는지 체크
            {
                tempVec = new Vector3(hasSoldierDic[tileName].Count, 0, 0);//명수에 따라 소환하는 위치를 바꿔야해서
            }
            if (this.nowLeader == LEADER_TYPE.PROPHET)
            {
                GameObject dubleSoldier = Instantiate(prefabSoldier, targetTransform.position + tempVec, Quaternion.identity);
                SetHasNode(tileName, dubleSoldier.GetComponent<Soldier>());//그타일에 방금 만든 병사를 저장해줌.
                return dubleSoldier;//생성한 병사를 return시킴
            }
            GameObject addedSoldier = Instantiate(prefabSoldier, targetTransform.position + tempVec, Quaternion.identity);
            SetHasNode(tileName, addedSoldier.GetComponent<Soldier>());//그타일에 방금 만든 병사를 저장해줌.
            //cardSlot.CurCard++;
            return addedSoldier;//생성한 병사를 return시킴

            // 2마리가 생성되게 해야함 - 선진
            // 그리고 지금 클릭 시, 상태가 변경되는거 고쳐야 됨.

            //더해줄 병사를 임의로 저장해주고
        }
        return null;
    }


    public override void SpawnBuilding(string tileName, Transform targetTransform, GameObject building)
    {
        if (hasBuildingDic.ContainsKey(tileName) == false)
            hasBuildingDic.Add(tileName, new List<GameObject>());
        if (hasBuildingDic[tileName].Contains(building) == false)//예외처리 실수
            SetHasBuildingNode(tileName, targetTransform, building); // 리스트에 넣고
        else
            Debug.Log("이미 건설됨");
    }
    public override void SetHasBuildingNode(string tileName, Transform targetTransform, GameObject building)
    {
        hasBuildingDic[tileName].Add(building);
        BuildingManager.Instance.InstantiateBuilding(building, targetTransform);
    }
    public void BreakingRule()
    {
        Debug.LogWarning("룰을 어김");
        RoundManager.Instance.bird.NowLeader = LEADER_TYPE.NONE;
        Uimanager.Instance.birdUI.birdLeaderSelect.SetActive(true);
    }
}