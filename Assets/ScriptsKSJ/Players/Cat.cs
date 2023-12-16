using CustomInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Player
{
    private int actionPoint;
    // 각 행동은 1의 액션포인트 소모 기본적으로 매턴 2를 가지고 들어간다
    // 예외적으로 카드 하나를 소모후 매고용으로 1포인트 더 
    public NodeMember baseNode;

    private int woodProductNum;
    public int WoodProductNum
    {
        get { return remainSoldierNum; }
        set
        {
            woodProductNum = value;
            Uimanager.Instance.catUI.woodProductText.text = woodProductNum.ToString();
        }
    }

    public int soldierMaxNum;//병사 최대 명수
    private int remainSoldierNum;
    public int RemainSoldierNum
    {
        get { return remainSoldierNum; }
        set
        {
            remainSoldierNum = value;
            Uimanager.Instance.catUI.remainSolText.text = remainSoldierNum.ToString();
        }
    }

    private int sawmillbuildNum;

    private int workshopbuildNum;

    private int barrackbuildNum;


    public Dictionary<ANIMAL_COST_TYPE, int> deadSoldierNum = new Dictionary<ANIMAL_COST_TYPE, int>();
    public bool isDisposable = true;
    IEnumerator flashCo;
    Color originColor1;
    Color originColor2;
    Color originColor3;
    Color originColor4;

    private new void Start()
    {
        base.Start();
        //Debug.Log(animator.GetLayerIndex("Idle"));
        isOver = false;
        roundManager.cat = this;
        roundManager.nowPlayer = this;
        //hasNodeNames.Add("생쥐3");
        ColorSetting();
        flashCo = FlashCoroutine();

        soldierMaxNum = 25;
        remainSoldierNum = soldierMaxNum;

    }

    private void ColorSetting()
    {
        originColor1 = RoundManager.Instance.mapExtra.mapTiles[0].transform.GetComponent<Renderer>().material.color;
        originColor2 = RoundManager.Instance.mapExtra.mapTiles[2].transform.GetComponent<Renderer>().material.color;
        originColor3 = RoundManager.Instance.mapExtra.mapTiles[8].transform.GetComponent<Renderer>().material.color;
        originColor4 = RoundManager.Instance.mapExtra.mapTiles[11].transform.GetComponent<Renderer>().material.color;
    }
    public void FlashTile()
    {
        StartCoroutine(flashCo);
    }
    IEnumerator FlashCoroutine()
    {
        while (RoundManager.Instance.cat.isDisposable)
        {
            RoundManager.Instance.mapExtra.mapTiles[0].transform.GetComponent<Renderer>().material.color = Color.green;
            RoundManager.Instance.mapExtra.mapTiles[2].transform.GetComponent<Renderer>().material.color = Color.green;
            RoundManager.Instance.mapExtra.mapTiles[8].transform.GetComponent<Renderer>().material.color = Color.green;
            RoundManager.Instance.mapExtra.mapTiles[11].transform.GetComponent<Renderer>().material.color = Color.green;
            yield return new WaitForSeconds(1f);
            RoundManager.Instance.mapExtra.mapTiles[0].transform.GetComponent<Renderer>().material.color = originColor1;
            RoundManager.Instance.mapExtra.mapTiles[2].transform.GetComponent<Renderer>().material.color = originColor2;
            RoundManager.Instance.mapExtra.mapTiles[8].transform.GetComponent<Renderer>().material.color = originColor3;
            RoundManager.Instance.mapExtra.mapTiles[11].transform.GetComponent<Renderer>().material.color = originColor4;
            yield return new WaitForSeconds(1f);
            yield return null;
        }
    }
    public override GameObject SpawnSoldier(string tileName, Transform targetTransform)
    {
        Vector3 tempVec = Vector3.zero;
        if (hasSoldierDic.ContainsKey(tileName))//병사가 존재하는지 체크
        {
            tempVec = new Vector3(hasSoldierDic[tileName].Count, 0, 0);//명수에 따라 소환하는 위치를 바꿔야해서
        }
        GameObject addedSoldier = Instantiate(prefabSoldier, targetTransform.position + tempVec, Quaternion.identity);
        //더해줄 병사를 임의로 저장해주고
        SetHasNode(tileName, addedSoldier.GetComponent<Soldier>());//그타일에 방금 만든 병사를 저장해줌.

        RemainSoldierNum--;
        return addedSoldier;//생성한 병사를 return시킴
    }
    public override void SpawnBuilding(string tileName, Transform targetTransform, GameObject building)
    {

        if (hasBuildingDic.ContainsKey(tileName) == false)
        {
            hasBuildingDic.Add(tileName, new List<GameObject>());
        }
        if (hasBuildingDic[tileName].Contains(building) == false)//예외처리 실수
        {
            SetHasBuildingNode(tileName, targetTransform, building); // 리스트에 넣고
        }
        else
        {
            Debug.Log("이미 건설됨");
        }

    }

    public void UseActionPoint()
    {
        if (actionPoint > 0)
            actionPoint--;
        else
            Debug.Log("포인트없음");
    }


    public void Employment() // 매 고용
    {
        if (actionPoint < 3)
            actionPoint++;
        else
            Debug.Log("야근못함");
    }




}