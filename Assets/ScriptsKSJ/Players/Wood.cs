using CustomInterface;
using sihyeon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wood : Player
{
    int tokenNum;
    [SerializeField]
    Transform particlesParent;
    public List<GameObject> particles = new List<GameObject>();
    public Dictionary<int,int> tokenScoreDic = new Dictionary<int,int>();
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
    [SerializeField]
    private int officerNum;//장교 기지가 생길때마다 추가
    public int OfficerNum
    {
        get { return officerNum; }
        set
        {
            RemainSoldierNum--;//장교가 추가되면 병사수가 줄어듬
            officerNum = value;
            Uimanager.Instance.woodUi.officerText.text = officerNum.ToString();
            Uimanager.Instance.woodUi.SetActionNum(battleActionNum, officerNum);
        }
    }
    [SerializeField]
    private int battleActionNum;
    public int BattleActionNum
    {
        get { return battleActionNum; }
        set {
            battleActionNum = value;
            if(officerNum == battleActionNum)
            {
                BattleBtnsOnOff(false);
            }
            Uimanager.Instance.woodUi.SetActionNum(battleActionNum, officerNum);
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
                OfficerNum++;//기지가 생기면 장교1명추가
            }
            else
            {
                OfficerNum--;//기지가 부숴지면 장교1명감소
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
                OfficerNum++;//기지가 생기면 장교1명추가
            }
            else
            {
                OfficerNum--;//기지가 부숴지면 장교1명감소
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
                OfficerNum++;//기지가 생기면 장교1명추가
            }
            else
            {
                OfficerNum--;//기지가 부숴지면 장교1명감소
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
        tokenNum = 0;
        RemainSoldierNum = soldierMaxNum;
        RoundManager.Instance.wood.supportVal.Add(ANIMAL_COST_TYPE.FOX, 0);
        RoundManager.Instance.wood.supportVal.Add(ANIMAL_COST_TYPE.RABBIT, 0);
        RoundManager.Instance.wood.supportVal.Add(ANIMAL_COST_TYPE.RAT, 0);
        RoundManager.Instance.wood.supportVal.Add(ANIMAL_COST_TYPE.BIRD, 0);
        buildCost = 1;//초기화1
        SetTokenScore();
        List<Effect> list = particlesParent.GetComponentsInChildren<Effect>().ToList();
        foreach (Effect p in list)
        {
            particles.Add(p.gameObject);
            p.gameObject.SetActive(false);
        }
        supportVal[ANIMAL_COST_TYPE.RAT] = 2;//테스트용 지울거
        //supportVal[ANIMAL_COST_TYPE.BIRD] = 1;//테스트용 지울거
        SetSupportUI(ANIMAL_COST_TYPE.RAT);//테스트용 지울거
        //SetSupportUI(ANIMAL_COST_TYPE.BIRD);//테스트용 지울거
    }
    public override GameObject SpawnSoldier(string tileName, Transform targetTransform)
    {
        if(remainSoldierNum > 0)
        {
            RemainSoldierNum--;//UI도 변경
            BattleActionNum++;
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
        if (isRatBuiilding || isRabbitBuiilding || isFoxBuiilding)
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
        
        int soldierCost = FindSoldierCost(tempMem);//병사가 3명이상있으면 코스트 계산
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
                    SetBaseValue(tempMem.nodeType, true);//기지 체크해주는거
                    Score += DestroyAllGetScore(tempMem);//건물 부수는거
                    for (int i = 0; i < RoundManager.Instance.cat.hasSoldierDic[tempMem.nodeName].Count; i++)//병사 다 없애는거
                    {
                        Soldier tempSol = RoundManager.Instance.cat.hasSoldierDic[tempMem.nodeName][i];
                        Debug.Log(tempSol.name);
                        RoundManager.Instance.cat.hasSoldierDic[tempMem.nodeName].Remove(tempSol);
                        Destroy(tempSol.gameObject);
                    }
                    for (int i = 0; i < RoundManager.Instance.bird.hasSoldierDic[tempMem.nodeName].Count; i++)//병사 다 없애는거
                    {
                        Soldier tempSol = RoundManager.Instance.bird.hasSoldierDic[tempMem.nodeName][i];
                        Debug.Log(tempSol.name);
                        RoundManager.Instance.bird.hasSoldierDic[tempMem.nodeName].Remove(tempSol);
                        Destroy(tempSol.gameObject);
                    }
                }
                else//동조라면
                {
                    
                    Debug.Log(RoundManager.Instance.nowPlayer.hasBuildingDic.Count);
                    tokenNum = 0;
                    foreach (KeyValuePair<string, List<GameObject>> kv in RoundManager.Instance.nowPlayer.hasBuildingDic)
                    {
                        for(int i = 0; i < kv.Value.Count; i++)
                        {
                            if(kv.Value[i].GetComponent<Building>().type == Building_TYPE.WOOD_TOKEN)
                            {
                                tokenNum++;
                            }
                        }
                    }

                    Score += tokenScoreDic[tokenNum];
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
            Debug.Log(roundManager.cat.hasBuildingDic[node.nodeName].Count);
            for(int i=0; i < roundManager.cat.hasBuildingDic[node.nodeName].Count; i++)
            {
                GameObject tempObject = roundManager.cat.hasBuildingDic[node.nodeName][i];
                roundManager.cat.hasBuildingDic[node.nodeName].RemoveAt(i);
                Destroy(tempObject);
                Debug.Log(roundManager.cat.hasBuildingDic[node.nodeName].Count);
            }
        }
        if (roundManager.bird.hasBuildingDic.ContainsKey(node.nodeName) && roundManager.bird.hasBuildingDic[node.nodeName].Count > 0)
        {
            Debug.Log("우드" + roundManager.bird.hasBuildingDic[node.nodeName].Count + "점 획득");
            calScore += roundManager.bird.hasBuildingDic[node.nodeName].Count;
            for (int i = 0; i < roundManager.bird.hasBuildingDic[node.nodeName].Count - 1; i++)
            {
                GameObject tempObject = roundManager.bird.hasBuildingDic[node.nodeName][i];
                roundManager.bird.hasBuildingDic[node.nodeName].RemoveAt(i);
                Destroy(tempObject);
                Debug.Log(roundManager.bird.hasBuildingDic[node.nodeName].Count);
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

    public void BattleBtnsOnOff(bool onOff)
    {
        if(onOff)
        {
            battleActionNum = 0;
        }
        Uimanager.Instance.playerUI.battleBtn.enabled = onOff;
        Uimanager.Instance.playerUI.moveBtn.enabled = onOff;
        Uimanager.Instance.playerUI.spawnBtn.enabled = onOff;
    }

    public void SetTokenScore()//토큰 설치했을때 점수
    {
        tokenScoreDic.Add(1, 0);
        tokenScoreDic.Add(2, 1);
        tokenScoreDic.Add(3, 1);
        tokenScoreDic.Add(4, 1);
        tokenScoreDic.Add(5, 2);
        tokenScoreDic.Add(6, 2);
        tokenScoreDic.Add(7, 3);
        tokenScoreDic.Add(8, 4);
        tokenScoreDic.Add(9, 4);
        tokenScoreDic.Add(10, 4);
    }
     
    public void SetTileEffectRevoit()
    {
        
    }
    public void SetTileEffectAgree()
    {
        List<string> seedMem = new List<string>();
        List<string> minusMem = new List<string>();
        
        foreach (KeyValuePair<string, List<GameObject>> kv in hasBuildingDic)
        {
            if (kv.Value.Count > 0)
            {
                seedMem.Add(RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == kv.Key).nodeName);
                minusMem.Add(RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == kv.Key).nodeName);
                Debug.Log(seedMem[0]);
            }
        }
        
        if(seedMem.Count > 0)
        {
            int tempNum = seedMem.Count;
            //주변 노드만 켜질수있어야하고 체크해야함.
            for (int i = 0; i < tempNum; i++)
            {
                Node tempnode = roundManager.mapExtra.graph.nodeList.Find(node => node.name == seedMem[i]);
                foreach(Edge tempEdge in tempnode.edgesInNode)
                {
                    Debug.Log(tempnode.edgesInNode.Count);
                    if (!seedMem.Exists(name => name == tempEdge.sNode.name))
                    {
                        seedMem.Add(tempEdge.sNode.name);
                        Debug.Log("추가" + tempEdge.sNode.name);
                    }
                    if (!seedMem.Exists(name => name == tempEdge.eNode.name))
                    {
                        seedMem.Add(tempEdge.eNode.name);
                        Debug.Log("추가" + tempEdge.eNode.name);
                    }
                }
            }
            tempNum = seedMem.Count;
            for (int i = 0; i < tempNum; i++)
            {
                NodeMember temp = roundManager.mapExtra.mapTiles.Find(node => node.nodeName == seedMem[i]);
                if (supportVal[temp.nodeType] + supportVal[ANIMAL_COST_TYPE.BIRD] < 1)
                {
                    seedMem.Remove(temp.nodeName);
                    Debug.Log("제거됨" + temp.nodeName);
                }//코스트 계산 필요 위에도             
            }
            foreach(string tempName in minusMem)
            {
                seedMem.Remove(tempName);
            }
            for (int i = 0; i < seedMem.Count; i++)
            {
                NodeMember member = roundManager.mapExtra.mapTiles.Find(node => node.nodeName == seedMem[i]);
                //Debug.Log(seedMem[i]);
                member.transform.GetChild(0).GetComponent<Effect>().gameObject.SetActive(true);
                Debug.Log(member.nodeName + "이펙트킴");
            }
        }
        else
        {
            List<NodeMember> buildingExist = new List<NodeMember>();
            foreach (NodeMember temp in RoundManager.Instance.mapExtra.mapTiles)
            {
                if (supportVal[temp.nodeType] + supportVal[ANIMAL_COST_TYPE.BIRD] >= 1)//코스트 계산 필요 위에도
                    buildingExist.Add(temp);
            }
            foreach (KeyValuePair<string, List<GameObject>> kv in hasBuildingDic)
            {
                if (kv.Value.Count > 0)
                {
                    buildingExist.Remove(RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == kv.Key));
                }
            }
            Debug.Log(buildingExist.Count);
            foreach (NodeMember temp in buildingExist)
            {
                Debug.Log(temp.nodeName);
                temp.transform.GetChild(0).GetComponent<Effect>().gameObject.SetActive(true);
            }
        }        
    }
    public void SetOffAllEffect()
    {
        foreach(GameObject effect in particles)
        {
            effect.SetActive(false);
        }
    }
}
