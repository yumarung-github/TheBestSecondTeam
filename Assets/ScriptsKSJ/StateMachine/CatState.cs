using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInterface;
using sihyeon;

public class CatWaitState : RmState
{
    public CatWaitState()
    {
        name = "캣대기";
    }
    public override void Enter()
    {
        rm.nowPlayer = cat;
        if (RoundManager.Instance.cat.isDisposable)
        {
            cat.FlashTile();
            RoundManager.Instance.testType = RoundManager.SoldierTestType.CatSet;
        }
        
        cat.testSetBtn();      
        
        Uimanager.Instance.playerUI.turnText.text = "게임 준비";
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.CAT_MORNING);
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        
    }
}
public class CatMorningState : RmState
{
    public CatMorningState()
    {
        name = "캣아침";
    }
    public override void Enter()
    {
        rm.nowPlayer = cat;
        Uimanager.Instance.playerUI.turnText.text = "현재 턴 : 고양이 후작/ 아침 / 선택";
        Uimanager.Instance.playerUI.SetBuildBtn();
        Uimanager.Instance.playerUI.SpawnSoldier();
        Uimanager.Instance.playerUI.SetBattleBtn();

        Uimanager.Instance.playerUI.ResetBtn(false);

        BattleManager.Instance.InitBattle();
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.CAT_AFTERNOON);
        Uimanager.Instance.catInven.SetActive(true);
        // 야전 병원
        Dictionary<ANIMAL_COST_TYPE, int> deadSoldierCheck = RoundManager.Instance.cat.deadSoldierNum;
        Dictionary<ANIMAL_COST_TYPE, List<Card>> playerCard = RoundManager.Instance.cat.cardDecks;

        List<ANIMAL_COST_TYPE> deadSoldierType = new List<ANIMAL_COST_TYPE>(deadSoldierCheck.Keys);


        for (int i = 0; i < deadSoldierType.Count; i++)
        {
            if (playerCard.ContainsKey(deadSoldierType[i]))
            {
                Debug.Log(RoundManager.Instance.cat.cardDecks[deadSoldierType[i]].Count);
            }
            else
                return;
        }

        foreach (KeyValuePair<string, List<GameObject>> kv in RoundManager.Instance.nowPlayer.hasBuildingDic)
        {
            for (int i = 0; i < kv.Value.Count; i++)
            {
                if (kv.Value[i].GetComponent<Building>().type == Building_TYPE.CAT_SAWMILL)
                {
                    Debug.Log(kv.Key);
                    NodeMember mem = RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == kv.Key);
                    RoundManager.Instance.cat.SpawnBuilding(mem.nodeName, mem.transform, BuildingManager.Instance.catSawMillPrefab);
                }
            }
        }
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        Uimanager.Instance.playerUI.ResetBtn(true);
    }
}
public class CatAfternoonState : RmState
{
    public CatAfternoonState()
    {
        name = "캣점심";
    }
    public override void Enter()
    {
        Uimanager.Instance.playerUI.turnText.text = "현재 턴 : 고양이 후작/ 점심 / 이동";
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.CAT_DINNER);
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        Uimanager.Instance.playerUI.ResetBtn(false);
    }
}
public class CatDinnerState : RmState
{
    public CatDinnerState()
    {
        name = "캣저녁";
    }
    public override void Enter()
    {
        Uimanager.Instance.playerUI.turnText.text = "현재 턴 : 고양이 후작/ 저녁";
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.BIRD_WAIT);
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
        rm.mapController.nowTile = null;
        cat.isOver = true;
        bird.isOver = false;
        rm.nowPlayer = null;
        Uimanager.Instance.playerUI.ResetBtn(false);
        Uimanager.Instance.catInven.SetActive(false);
    }
}

