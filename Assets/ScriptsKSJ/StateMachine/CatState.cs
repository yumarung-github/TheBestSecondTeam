using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInterface;
using sihyeon;

public class CatWaitState : RmState
{
    bool isDisposable = true;
    public CatWaitState()
    {
        name = "캣대기";
    }
    public override void Enter()
    {
        if (isDisposable)
            cat.FlashTile();
        rm.nowPlayer = cat;
        cat.testSetBtn();//임의로 버튼설정        
        // 다음버튼에 다음으로 넘어갈수있게 넣어줌
        Uimanager.Instance.playerUI.turnText.text = "현재 턴 : 고양이 후작";
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.CAT_MORNING);
        
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        Uimanager.Instance.playerUI.SetBuildBtn();
        Uimanager.Instance.playerUI.SpawnSoldier();
        Uimanager.Instance.playerUI.SetBattleBtn();
        
        Uimanager.Instance.playerUI.ResetBtn(false);

        BattleManager.Instance.InitBattle();
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
        Uimanager.Instance.playerUI.turnText.text = "현재 턴 : 고양이 후작/ 아침 / 선택";
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.CAT_AFTERNOON);
        Uimanager.Instance.catInven.SetActive(true);
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

