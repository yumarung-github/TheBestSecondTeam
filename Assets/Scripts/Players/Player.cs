using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public bool isOver;
    public int score;
    public List<string> hasNodeNames = new List<string>();
    public GameObject prefabSoldier;

    public Dictionary<string, List<Soldier>> hasSoldierDic = new Dictionary<string, List<Soldier>>();
    public Dictionary<string, List<Build>> hasBuildingDic = new Dictionary<string, List<Build>>();
    
    protected RoundManager roundManager;

    protected void Start()
    {
        roundManager = RoundManager.Instance;
        isOver = true;                
    }

    protected void Update()
    {
        
    }
    public void SetPlayer()
    {
        RoundManager.Instance.nowPlayer = this;
    }
    public GameObject SpawnSoldier(string tileName, Transform targetTransform)
    {
        Vector3 tempVec = Vector3.zero;
        if (hasSoldierDic.ContainsKey(tileName))
        {
            tempVec = new Vector3(hasSoldierDic[tileName].Count, 0,0);
        }
        GameObject addedSoldier = Instantiate(prefabSoldier, targetTransform.position + tempVec, Quaternion.identity);
        SetHasNode(tileName, addedSoldier.GetComponent<Soldier>());
        return addedSoldier;
    }
    public void SetHasNode(string tempName,Soldier tempSoldier)
    {
        if(hasSoldierDic.ContainsKey(tempName)==false) {
            hasSoldierDic.Add(tempName, new List<Soldier>());
        }
        hasSoldierDic[tempName].Add(tempSoldier);
    }
}
