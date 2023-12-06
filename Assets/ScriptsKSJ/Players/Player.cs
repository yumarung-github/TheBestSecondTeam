using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public bool isOver;//���� �ڱ����� �ƴϸ� false����
    public int score;//�׽�Ʈ�� ���ֵ���
    public List<string> hasNodeNames = new List<string>();//� Ÿ���� �����ϰ� �ִ��� üũ�ϴ� �뵵
    public GameObject prefabSoldier;

    public Dictionary<string, List<Soldier>> hasSoldierDic = new Dictionary<string, List<Soldier>>();
    //���縦 �������ִ� Ÿ�Ͽ� ���� ����Ʈ�� ����
    //�������� ���ؼ� ����Ʈ�� ������ �ϳ��� ������ ���� �ϸ��.
    
    
    protected RoundManager roundManager;//���� ���Ű��Ƽ� �־����.

    protected void Start()
    {
        roundManager = RoundManager.Instance;
        isOver = true;                
    }
    public void SetPlayer()
    {
        RoundManager.Instance.nowPlayer = this;
    }
    public GameObject SpawnSoldier(string tileName, Transform targetTransform)
    {
        Vector3 tempVec = Vector3.zero;
        if (hasSoldierDic.ContainsKey(tileName))//���簡 �����ϴ��� üũ
        {
            tempVec = new Vector3(hasSoldierDic[tileName].Count, 0,0);//����� ���� ��ȯ�ϴ� ��ġ�� �ٲ���ؼ�
        }
        GameObject addedSoldier = Instantiate(prefabSoldier, targetTransform.position + tempVec, Quaternion.identity);
        //������ ���縦 ���Ƿ� �������ְ�
        SetHasNode(tileName, addedSoldier.GetComponent<Soldier>());//��Ÿ�Ͽ� ��� ���� ���縦 ��������.
        return addedSoldier;//������ ���縦 return��Ŵ
    }
    public void SetHasNode(string tempName,Soldier tempSoldier)//����Ʈ�� �����ϰ� �����ֱ�����.
    {
        if(hasSoldierDic.ContainsKey(tempName)==false) {//�����ųʸ��� �̰� �ִ��� üũ�ϰ� 
            //������ list�� ���� �������ش�.
            hasSoldierDic.Add(tempName, new List<Soldier>());
        }
        hasSoldierDic[tempName].Add(tempSoldier);//��ųʸ��� �߰�����.
    }
}
