using CustomInterface;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wood : Player
{
    public Dictionary<ANIMAL_COST_TYPE, int> revoitVal = new Dictionary<ANIMAL_COST_TYPE, int>();
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
        get { return soldierMaxNum; }
        set
        {
            remainSoldierNum--;//장교가 추가되면 병사수가 줄어듬
            soldierMaxNum = value;
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
}
