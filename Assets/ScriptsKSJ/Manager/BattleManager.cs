using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : SingleTon<BattleManager>
{
    public Player battleP1;
    public Player battleP2;

    public List<Soldier> battleP1Soldiers;
    public List<Soldier> battleP2Soldiers;

    public TextMeshProUGUI battleP1Text;
    public TextMeshProUGUI battleP2Text;

    private new void Awake()
    {
        base.Awake();
        
    }
    public void InitBattle()
    {
        foreach(Button button in Uimanager.Instance.playerUI.buttons)
        {
            button.onClick.RemoveAllListeners();
        }
        RoundManager.Instance.cat.battleSoldierNum = 0;
        RoundManager.Instance.bird.battleSoldierNum = 0;
        RoundManager.Instance.wood.battleSoldierNum = 0;
        //초기화
        battleP1 = RoundManager.Instance.nowPlayer;

        int tempNum = 0;
        if(RoundManager.Instance.cat.isOver == true)
        {
            Uimanager.Instance.playerUI.buttons[tempNum].transform.GetComponentInChildren<TextMeshProUGUI>().text =
                RoundManager.Instance.nowPlayer.gameObject.name + " vs Cat";
            Uimanager.Instance.playerUI.buttons[tempNum].onClick.AddListener(() =>
            {                
                if (RoundManager.Instance.cat.hasSoldierDic.ContainsKey(RoundManager.Instance.mapController.nowTile.nodeName))
                {
                    battleP2Soldiers = RoundManager.Instance.cat.
                    hasSoldierDic[RoundManager.Instance.mapController.nowTile.nodeName];
                    RoundManager.Instance.cat.battleSoldierNum = battleP2Soldiers.Count;
                }
                battleP2 = RoundManager.Instance.cat;
                StartBattle();
                
            });
            tempNum++;
        }
        if(RoundManager.Instance.bird.isOver == true)
        {
            Uimanager.Instance.playerUI.buttons[tempNum].transform.GetComponentInChildren<TextMeshProUGUI>().text =
                RoundManager.Instance.nowPlayer.gameObject.name + " vs Bird";
            
            Uimanager.Instance.playerUI.buttons[tempNum].onClick.AddListener(() =>
            {
                if (RoundManager.Instance.bird.hasSoldierDic.ContainsKey(RoundManager.Instance.mapController.nowTile.nodeName))
                {
                    battleP2Soldiers = RoundManager.Instance.bird.
                    hasSoldierDic[RoundManager.Instance.mapController.nowTile.nodeName];
                    RoundManager.Instance.bird.battleSoldierNum = battleP2Soldiers.Count;
                }
                battleP2 = RoundManager.Instance.bird;
                StartBattle();
                
            });
            tempNum++;
        }
        if(RoundManager.Instance.wood.isOver == true)
        {
            Uimanager.Instance.playerUI.buttons[tempNum].transform.GetComponentInChildren<TextMeshProUGUI>().text =
                RoundManager.Instance.nowPlayer.gameObject.name + " vs Wood";
            Uimanager.Instance.playerUI.buttons[tempNum].onClick.AddListener(() =>
            {
                if (RoundManager.Instance.wood.hasSoldierDic.ContainsKey(RoundManager.Instance.mapController.nowTile.nodeName))
                {
                    battleP2Soldiers = RoundManager.Instance.wood.
                    hasSoldierDic[RoundManager.Instance.mapController.nowTile.nodeName];
                    RoundManager.Instance.wood.battleSoldierNum = battleP2Soldiers.Count;
                }
                battleP2 = RoundManager.Instance.wood;
                StartBattle();
                
            });
            tempNum++;
        }//현재 턴이 아닌애들
    }
    public void StartBattle()//주사위가 나온사용자가 그 값을 가지게 해놨음 세세한건 나중에 수정
    {
        battleP1Soldiers = RoundManager.Instance.nowPlayer.hasSoldierDic[RoundManager.Instance.mapController.nowTile.nodeName];
        RoundManager.Instance.nowPlayer.battleSoldierNum = battleP1Soldiers.Count;
        int diceP1Num = Random.Range(0, 4);//p2가 나온숫자 p1의 병사가 죽어야하는 숫자
        int diceP2Num = Random.Range(0, 4);//p1이 나온숫자 p2의 병사가 죽어야하는 숫자

        Debug.Log("배틀시작");
        diceP1Num = (diceP1Num > battleP2.battleSoldierNum) ? battleP2.battleSoldierNum : diceP1Num;
        Debug.Log(diceP2Num);
        for (int i = 0; i< diceP1Num; i++)
        {
            if (battleP1.battleSoldierNum == 0)
            {
                break;
            }
            GameObject tempObj = battleP1Soldiers[battleP1.battleSoldierNum - 1].gameObject;
            battleP1Soldiers.RemoveAt(battleP1.battleSoldierNum - 1);
            Destroy(tempObj);
            battleP1.battleSoldierNum--;
            if(battleP1 is Wood wood)
            {
                wood.RemainSoldierNum++;
            }
            if (battleP1 is Cat cat)
            {
                if (!cat.deadSoldierNum.ContainsKey(RoundManager.Instance.mapController.nowTile.nodeType))
                    cat.deadSoldierNum.Add(RoundManager.Instance.mapController.nowTile.nodeType, 0);
                cat.deadSoldierNum[RoundManager.Instance.mapController.nowTile.nodeType]++;
            }            
        }
        diceP2Num = (diceP2Num > battleP1.battleSoldierNum) ? battleP1.battleSoldierNum : diceP2Num;
        for (int i = 0; i < diceP2Num; i++)
        {
            if(battleP2.battleSoldierNum == 0)
            {
                break;
            }
            GameObject tempObj = battleP2Soldiers[battleP2.battleSoldierNum - 1].gameObject;
            battleP2Soldiers.RemoveAt(battleP2.battleSoldierNum - 1);
            Destroy(tempObj);
            battleP2.battleSoldierNum--;
            if (battleP2 is Wood wood)
            {
                wood.RemainSoldierNum++;
            }
            if(battleP2 is Cat cat)
            {
                if (!cat.deadSoldierNum.ContainsKey(RoundManager.Instance.mapController.nowTile.nodeType))
                    cat.deadSoldierNum.Add(RoundManager.Instance.mapController.nowTile.nodeType, 0);
                cat.deadSoldierNum[RoundManager.Instance.mapController.nowTile.nodeType]++;
            }
            
        }
        if (RoundManager.Instance.cat.deadSoldierNum.ContainsKey(RoundManager.Instance.mapController.nowTile.nodeType))
        {
            Debug.Log(RoundManager.Instance.cat.deadSoldierNum[RoundManager.Instance.mapController.nowTile.nodeType]);

        }
        Debug.Log(diceP1Num);
        Debug.Log(diceP2Num);
        Uimanager.Instance.playerUI.battleWindow.SetActive(false);
    }
}