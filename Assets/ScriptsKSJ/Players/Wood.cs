using CustomInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public class Wood : Player
{    
    public int soldierMaxNum;//병사 최대 명수
    private int remainSoldierNum;
    public int RemainSoldierNum
    {
        get { return remainSoldierNum; }
        set {            
            remainSoldierNum = value;
            Uimanager.Instance.woodUi.remainSolText.text = remainSoldierNum.ToString();
        }
    }
    private int officerNum;//장교 기지가 생길때마다 추가
    public int OfficerNum
    {
        get { return officerNum; }
        set
        {
            RemainSoldierNum--;//장교가 추가되면 병사수가 줄어듬
            officerNum = value;
            Uimanager.Instance.woodUi.officerText.text = officerNum.ToString();
        }
    }

    private bool isFoxBuiilding;
    public bool IsFoxBuiilding
    {
        get { return isFoxBuiilding; }
        set
        {
            if(value)
            {
                officerNum++;//기지가 생기면 장교1명추가
            }
            else
            {
                officerNum--;//기지가 부숴지면 장교1명감소
            }
            isFoxBuiilding = value;
        }
    }

    private bool isRatBuiilding;
    public bool IsRatBuiilding
    {
        get { return isRatBuiilding; }
        set
        {
            if (value)
            {
                officerNum++;//기지가 생기면 장교1명추가
            }
            else
            {
                officerNum--;//기지가 부숴지면 장교1명감소
            }
            isRatBuiilding = value;
        }
    }

    private bool isRabbitBuiilding;
    public bool IsRabbitBuiilding
    {
        get { return isRabbitBuiilding; }
        set
        {
            if (value)
            {
                officerNum++;//기지가 생기면 장교1명추가
            }
            else
            {
                officerNum--;//기지가 부숴지면 장교1명감소
            }
            isRabbitBuiilding = value;
        }
    }
    public Dictionary<ANIMAL_COST_TYPE, int> supportVal = new Dictionary<ANIMAL_COST_TYPE, int>();

    public int buildCost;
    private new void Start()
    {
        base.Start();
        roundManager.wood = this;
        hasNodeNames.Add("여우1");//임의로 가진 타일
        isFoxBuiilding = false;
        isRabbitBuiilding = false;
        isRatBuiilding = false;
        officerNum = 0;
        soldierMaxNum = 10;
        remainSoldierNum = soldierMaxNum;
        RoundManager.Instance.wood.supportVal.Add(ANIMAL_COST_TYPE.FOX, 0);
        RoundManager.Instance.wood.supportVal.Add(ANIMAL_COST_TYPE.RABBIT, 0);
        RoundManager.Instance.wood.supportVal.Add(ANIMAL_COST_TYPE.RAT, 0);
        RoundManager.Instance.wood.supportVal.Add(ANIMAL_COST_TYPE.BIRD, 0);
        buildCost = 1;//초기화1
    }
    public override GameObject SpawnSoldier(string tileName, Transform targetTransform)
    {
        if(remainSoldierNum > 0)
        {
            RemainSoldierNum--;//UI도 변경
            Vector3 tempVec = Vector3.zero;
            if (hasSoldierDic.ContainsKey(tileName))//병사가 존재하는지 체크
            {
                tempVec = new Vector3(hasSoldierDic[tileName].Count, 0, 0);//명수에 따라 소환하는 위치를 바꿔야해서
            }
            GameObject addedSoldier = Instantiate(prefabSoldier, targetTransform.position + tempVec, Quaternion.identity);
            //더해줄 병사를 임의로 저장해주고
            SetHasNode(tileName, addedSoldier.GetComponent<Soldier>());//그타일에 방금 만든 병사를 저장해줌.
            return addedSoldier;//생성한 병사를 return시킴
        }
        else
        {
            Debug.Log("병사가 더이상 없습니다");
            return null;
        }
        
    }
    public void SetSupportUI(ANIMAL_COST_TYPE tempType)
    {
        switch (tempType) {
            case ANIMAL_COST_TYPE.FOX:
                Uimanager.Instance.woodUi.foxSupportNumText.text = supportVal[ANIMAL_COST_TYPE.FOX].ToString();
                break;
            case ANIMAL_COST_TYPE.RABBIT:
                Uimanager.Instance.woodUi.rabbitSupportNumText.text = supportVal[ANIMAL_COST_TYPE.RABBIT].ToString();
                break;
            case ANIMAL_COST_TYPE.RAT:
                Uimanager.Instance.woodUi.ratSupportNumText.text = supportVal[ANIMAL_COST_TYPE.RAT].ToString();
                break;
            case ANIMAL_COST_TYPE.BIRD:
                Uimanager.Instance.woodUi.birdSupportNumText.text = supportVal[ANIMAL_COST_TYPE.BIRD].ToString();
                break;
        }
    }
    public void SetOfficerBtnOnoff()
    {
        if(isRabbitBuiilding || isRabbitBuiilding || isFoxBuiilding)
        {
            Uimanager.Instance.woodUi.officerBtn.enabled = true;
        }
        else
        {
            Uimanager.Instance.woodUi.officerBtn.enabled = false;
        }
    }
    public override void SpawnBuilding(string tileName, Transform targetTransform, GameObject building)
    {
        NodeMember tempMem = roundManager.mapExtra.mapTiles.Find(node => node.nodeName == tileName);
        
        int soldierCost = FindSoldierCost(tempMem);
        //Debug.Log(supportVal[tempMem.nodeType]);
        //Debug.Log(supportVal[ANIMAL_COST_TYPE.BIRD]);

        if (supportVal[tempMem.nodeType] + supportVal[ANIMAL_COST_TYPE.BIRD] >= buildCost + soldierCost)
        {//지지자 값 계산 같은종류+새카드 > 건물 코스트+병사 코스트 계산
            if (hasBuildingDic.ContainsKey(tileName) == false)
            {
                hasBuildingDic.Add(tileName, new List<GameObject>());
            }
            if (hasBuildingDic[tileName].Contains(building) == false && CostTypeCheck(tempMem.nodeType) == false)
            {
                SetHasBuildingNode(tileName, targetTransform, building);
                if(supportVal[tempMem.nodeType] < buildCost + soldierCost)
                {
                    int birdCostCal = supportVal[tempMem.nodeType] - (buildCost + soldierCost);
                    supportVal[tempMem.nodeType] = 0;
                    supportVal[ANIMAL_COST_TYPE.BIRD] += birdCostCal;
                }
                else
                    supportVal[tempMem.nodeType] -= buildCost + soldierCost;
                if(buildCost == 2)//반란이면
                {
                    SetBaseValue(tempMem.nodeType, true);
                    Score += DestroyAllGetScore(tempMem);
                }
            }
            else
            {
                Debug.Log("이미 건설됨");
            }      
            SetSupportUI(tempMem.nodeType);
            SetSupportUI(ANIMAL_COST_TYPE.BIRD);
        }
        else
        {
            Debug.Log("지지자가 부족합니다");
        }
    }
    int FindSoldierCost(NodeMember findTile)//현재 타일에 가장 큰 병사수를 가진거에 따라 코스트 리턴
    {
        int calNum = 0;
        int solCost = 0;
        int catNum = 0;
        int birdNum = 0;
        if (roundManager.cat.hasSoldierDic.ContainsKey(findTile.nodeName))
        {
            catNum = roundManager.cat.hasSoldierDic[findTile.nodeName].Count;
        }
        if (roundManager.bird.hasSoldierDic.ContainsKey(findTile.nodeName))
        {
            birdNum = roundManager.bird.hasSoldierDic[findTile.nodeName].Count;
        }
        //고양이후작이랑 이어리중에병사수가 더많은걸 찾음
        calNum = catNum >= birdNum ? catNum : birdNum;
        
        while(calNum >= 3) 
        {
            calNum = calNum / 3;
            solCost++;
        }
        return solCost;
    }

    public int DestroyAllGetScore(NodeMember node)
    {
        int calScore = 0;
        Debug.Log("모두 파괴");
        if(roundManager.cat.hasBuildingDic.ContainsKey(node.nodeName) && roundManager.cat.hasBuildingDic[node.nodeName].Count > 0)
        {
            Debug.Log("우드" + roundManager.cat.hasBuildingDic[node.nodeName].Count + "점 획득");
            calScore += roundManager.cat.hasBuildingDic[node.nodeName].Count;
            foreach(GameObject tempObject in roundManager.cat.hasBuildingDic[node.nodeName])
            {
                roundManager.cat.hasBuildingDic[node.nodeName].Remove(tempObject);
                Destroy(tempObject);
            }
        }
        if (roundManager.bird.hasBuildingDic.ContainsKey(node.nodeName) && roundManager.bird.hasBuildingDic[node.nodeName].Count > 0)
        {
            Debug.Log("우드" + roundManager.bird.hasBuildingDic[node.nodeName].Count + "점 획득");
            calScore += roundManager.bird.hasBuildingDic[node.nodeName].Count;
            foreach (GameObject tempObject in roundManager.bird.hasBuildingDic[node.nodeName])
            {
                roundManager.bird.hasBuildingDic[node.nodeName].Remove(tempObject);
                Destroy(tempObject);
            }
        }
        return calScore;
    }
    void SetBaseValue(ANIMAL_COST_TYPE type, bool onOff)
    {
        switch (type)
        {
            case ANIMAL_COST_TYPE.FOX:
                IsFoxBuiilding = onOff;
                break;
            case ANIMAL_COST_TYPE.RAT:
                IsRatBuiilding = onOff;
                break;
            case ANIMAL_COST_TYPE.RABBIT:
                IsRabbitBuiilding = onOff;
                break;
        }
    }
    bool CostTypeCheck(ANIMAL_COST_TYPE type)
    {
        bool check = false;
        switch (type)
        {
            case ANIMAL_COST_TYPE.FOX:
                check = isFoxBuiilding;
                break;
            case ANIMAL_COST_TYPE.RAT:
                check = isRatBuiilding;
                break;
            case ANIMAL_COST_TYPE.RABBIT:
                check = isRabbitBuiilding;
                break;
        }
        return check;
    }
}
