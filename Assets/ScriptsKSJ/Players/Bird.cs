using CustomInterface;
using sihyeon;
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

    int curIndex;
    int spawn = 0;
    int move = 1;
    int battle = 2;
    int build = 3;

    public bool isSpwaned;
    public bool isMoved  ;
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

        isSpwaned = true;
        isMoved = true;
        isBattled = true;
        isBuilded = true;
    }


    public override GameObject SpawnSoldier(string tileName, Transform targetTransform)
    {       
        BirdCardSlot cardSlot = inventory.birdCardSlot[inventory.curSlot];
        curIndex = inventory.birdCardSlot[inventory.curSlot].CurCard;
        bool isCheckBird = cardSlot.birdCard[curIndex].costType == ANIMAL_COST_TYPE.BIRD;
        bool isCheckType = cardSlot.birdCard[curIndex].costType == RoundManager.Instance.mapController.nowTile.nodeType;


        if (isCheckBird || isCheckType)
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
            cardSlot.CurCard++;
            return addedSoldier;//생성한 병사를 return시킴

        }
        isSpwaned = false;
        return null;
    }

    public override void SetHasBuildingNode(string tileName, Transform targetTransform, GameObject building)
    {
        BirdCardSlot cardSlot = inventory.birdCardSlot[inventory.curSlot];
        curIndex = inventory.birdCardSlot[inventory.curSlot].CurCard;
        if (cardSlot.birdCard[curIndex].costType == RoundManager.Instance.mapController.nowTile.nodeType)
        {
            hasBuildingDic[tileName].Add(building);
            BuildingManager.Instance.InstantiateBuilding(building);
            cardSlot.CurCard++;
        }
        else
            isBuilded = false;
    }
}