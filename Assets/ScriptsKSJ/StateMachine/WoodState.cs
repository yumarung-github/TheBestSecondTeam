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
        Uimanager.Instance.turnText.text = "현재 턴 : 우드랜드";
        Uimanager.Instance.SetNextBtn(MASTATE_TYPE.WOOD_MORNING);
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
        Uimanager.Instance.SetSpawnBtn();
    }
}
public class WoodMorningState : RmState
{
    public WoodMorningState()
    {
        name = "우드아침";
    }
    public override void Enter()
    {
        Uimanager.Instance.turnText.text = "현재 턴 : 우드랜드 / 아침";
        Uimanager.Instance.SetNextBtn(MASTATE_TYPE.WOOD_AFTERNOON);
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
        Uimanager.Instance.turnText.text = "현재 턴 : 우드랜드 / 점심";
        Uimanager.Instance.SetNextBtn(MASTATE_TYPE.WOOD_DINNER);
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        
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
        Uimanager.Instance.turnText.text = "현재 턴 : 우드랜드 / 저녁";
        Uimanager.Instance.SetNextBtn(MASTATE_TYPE.CAT_WAIT);
        Uimanager.Instance.ResetBtn(true);
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        RoundManager.Instance.wood.isOver = true;
        RoundManager.Instance.cat.isOver = false;
        rm.nowPlayer = null;
        Uimanager.Instance.ResetBtn(false);
    }
}