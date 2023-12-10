using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInterface;

public class WoodWaitState : RmState
{
    public WoodWaitState()
    {
        name = "우드대기";
    }
    public override void Enter()
    {
        Uimanager.Instance.playerUI.turnText.text = "현재 턴 : 우드랜드";
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.WOOD_MORNING1);
        Uimanager.Instance.woodUi.profileWindow.SetActive(true);
    }
    public override void Update()
    {
        if (wood.isOver == false)
        {
        }
    }
    public override void Exit()
    {
        rm.nowPlayer = wood;
        Uimanager.Instance.playerUI.SpawnSoldier();
        BattleManager.Instance.InitBattle();
    }
}
public class WoodMorning1State : RmState
{
    public WoodMorning1State()
    {
        name = "우드아침";
    }
    public override void Enter()
    {
        Uimanager.Instance.playerUI.turnText.text = "현재 턴 : 우드랜드 / 아침1";
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.WOOD_MORNING2);
        Uimanager.Instance.woodUi.SetWoodMorning1();
    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}
public class WoodMorning2State : RmState
{
    public WoodMorning2State()
    {
        name = "우드아침";
    }
    public override void Enter()
    {
        Uimanager.Instance.playerUI.turnText.text = "현재 턴 : 우드랜드 / 아침2";
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.WOOD_AFTERNOON);
        Uimanager.Instance.woodUi.SetWoodMorning2();
    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}
public class WoodAfternoonState : RmState
{
    public WoodAfternoonState()
    {
        name = "우드점심";
    }
    public override void Enter()
    {
        Uimanager.Instance.playerUI.turnText.text = "현재 턴 : 우드랜드 / 점심";
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.WOOD_DINNER);
        Uimanager.Instance.woodUi.SetAfternoon(true);
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        Uimanager.Instance.woodUi.SetAfternoon(false);
    }
}
public class WoodDinnerState : RmState
{
    public WoodDinnerState()
    {
        name = "우드저녁";
    }
    public override void Enter()
    {
        Uimanager.Instance.playerUI.turnText.text = "현재 턴 : 우드랜드 / 저녁";
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.CAT_WAIT);
        Uimanager.Instance.playerUI.ResetBtn(true);
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        RoundManager.Instance.wood.isOver = true;
        RoundManager.Instance.cat.isOver = false;
        rm.nowPlayer = null;
        Uimanager.Instance.playerUI.ResetBtn(false);
        Uimanager.Instance.woodUi.profileWindow.SetActive(false);
    }
}