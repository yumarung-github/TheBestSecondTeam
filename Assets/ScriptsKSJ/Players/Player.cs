using CustomInterface;
// using SJ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 선진
    /// </summary>
    public PlayerInventory inven;
    public Dictionary<ANIMAL_COST_TYPE, List<Card>> cardDecks = new Dictionary<ANIMAL_COST_TYPE, List<Card>>();
    private Dictionary<ANIMAL_COST_TYPE, int> haveAnimalMoney = new Dictionary<ANIMAL_COST_TYPE, int>();
    public List<Card> craftedCards = new List<Card>();

    public bool isOver;//현재 자기턴이 아니면 false상태
    int score;//테스트용 없애도됨
    public List<string> hasNodeNames = new List<string>();//어떤 타일을 지배하고 있는지 체크하는 용도
    public GameObject prefabSoldier;
    public int battleSoldierNum;
    public Dictionary<string, List<Soldier>> hasSoldierDic = new Dictionary<string, List<Soldier>>();
    //병사를 가지고있는 타일에 병사 리스트를 저장
    //병사명수를 정해서 리스트의 끝부터 하나씩 꺼내서 쓰게 하면됨.


    protected RoundManager roundManager;//많이 쓸거같아서 넣어놨음.

    public int Score
    { get { return score; } }

    public Dictionary<ANIMAL_COST_TYPE, int> HaveAnimalMoney
    {
        get => haveAnimalMoney;
        set { haveAnimalMoney = value; }
    }
    public void AddCard(Card card, ANIMAL_COST_TYPE cardType)
    {
        //Debug.Log(card.gameObject.name);
        if (!cardDecks.ContainsKey(cardType))
        {
            cardDecks[cardType] = new List<Card>();
        }
        cardDecks[cardType].Add(card);
        inven.AddCard(card);
        //Debug.Log(cardDecks.ContainsKey(cardType));
    }

    public void SetMoney(ANIMAL_COST_TYPE ACT, int money)
    {
        haveAnimalMoney[ACT] = money;
    }

    protected void Start()
    {
        haveAnimalMoney.Add(ANIMAL_COST_TYPE.FOX, 0);
        haveAnimalMoney.Add(ANIMAL_COST_TYPE.RAT, 0);
        haveAnimalMoney.Add(ANIMAL_COST_TYPE.RABBIT, 0);
        haveAnimalMoney.Add(ANIMAL_COST_TYPE.BIRD, 0);
        SetMoney(ANIMAL_COST_TYPE.FOX, 3);
        roundManager = RoundManager.Instance;
        isOver = true;                
    }
    public void SetPlayer()
    {
        RoundManager.Instance.nowPlayer = this;
    }
    public virtual GameObject SpawnSoldier(string tileName, Transform targetTransform)
    {
        
        Vector3 tempVec = Vector3.zero;
        if (hasSoldierDic.ContainsKey(tileName))//병사가 존재하는지 체크
        {
            tempVec = new Vector3(hasSoldierDic[tileName].Count, 0,0);//명수에 따라 소환하는 위치를 바꿔야해서
        }
        GameObject addedSoldier = Instantiate(prefabSoldier, targetTransform.position + tempVec, Quaternion.identity);
        //더해줄 병사를 임의로 저장해주고
        SetHasNode(tileName, addedSoldier.GetComponent<Soldier>());//그타일에 방금 만든 병사를 저장해줌.
        return addedSoldier;//생성한 병사를 return시킴
    }
    public void SetHasNode(string tempName,Soldier tempSoldier)//리스트를 생성하고 더해주기위함.
    {
        if(hasSoldierDic.ContainsKey(tempName)==false) {//병사딕셔너리에 이게 있는지 체크하고 
            //없으면 list를 새로 생성해준다.
            hasSoldierDic.Add(tempName, new List<Soldier>());
        }
        hasSoldierDic[tempName].Add(tempSoldier);//딕셔너리에 추가해줌.
    }
}
