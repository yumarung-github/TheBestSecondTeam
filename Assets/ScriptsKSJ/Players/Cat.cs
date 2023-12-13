using CustomInterface;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cat : Player
{
    public Dictionary<string, int> deadSoldierNum = new Dictionary<string, int>();
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
        hasNodeNames.Add("생쥐3");
        ColorSetting();
        flashCo = FlashCoroutine();
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
        while (true)
        {
            Debug.Log("TEST");
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
    public void SetSoldierAllTile(string tileName) //모든 타일에 병력 소환 //
    {
        //제외 해야할 타일 목록들//
        int foxTile1 = 0;
        int foxTile4 = 2;
        int ratTile1 = 8;
        int ratTile4 = 11;
        //노드 멤버를 불러와야함
        //리스트의 0 2 8 11 이 여우 1 여우 4 생쥐 1 생쥐 4임
        List<NodeMember> nodeMem = RoundManager.Instance.mapExtra.mapTiles;
        if (tileName == "여우 1")
        {
            for (int i = 0; i < nodeMem.Count; i++)
            {
                if (i != ratTile4)
                    SpawnSoldier(nodeMem[i].nodeName, nodeMem[i].transform); // 모든 타일 병력생성
            }
            //생쥐 4 제외 병력생성
        }
        else if (tileName == "여우 4")
        {
            for (int i = 0; i < nodeMem.Count; i++)
            {
                if (i != ratTile1)
                    SpawnSoldier(nodeMem[i].nodeName, nodeMem[i].transform); // 모든 타일 병력생성
            }
            //생쥐 1 제외 병력 생성
        }
        else if (tileName == "생쥐 1")
        {
            for (int i = 0; i < nodeMem.Count; i++)
            {
                if (i != foxTile4)
                    SpawnSoldier(nodeMem[i].nodeName, nodeMem[i].transform); // 모든 타일 병력생성
            }
            //여우 4 제외 병력 생성
        }
        else if (tileName == "생쥐 4")
        {
            for (int i = 0; i < nodeMem.Count; i++)
            {
                if (i != foxTile1)
                    SpawnSoldier(nodeMem[i].nodeName, nodeMem[i].transform);
            }
            //여우 1 제외 병력 생성
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
}