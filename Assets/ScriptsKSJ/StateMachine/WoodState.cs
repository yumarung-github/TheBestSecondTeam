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
        rm.turnText.text = "현재 턴 : 우드랜드";
        rm.SetNext(MASTATE_TYPE.WOOD_MORNING);//다음 버튼 새로 설정해줌.
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
        rm.SetSpawnBtn();
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
        rm.turnText.text = "현재 턴 : 우드랜드 / 아침";
        rm.SetNext(MASTATE_TYPE.WOOD_AFTERNOON);//다음 버튼 새로 설정해줌.
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
        rm.turnText.text = "현재 턴 : 우드랜드 / 점심";
        rm.SetNext(MASTATE_TYPE.WOOD_DINNER);//다음 버튼 새로 설정해줌.
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
        rm.turnText.text = "현재 턴 : 우드랜드 / 저녁";
        rm.SetNext(MASTATE_TYPE.CAT_WAIT);//다음 버튼 새로 설정해줌.
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        RoundManager.Instance.wood.isOver = true;
        RoundManager.Instance.cat.isOver = false;
        rm.nowPlayer = null;
    }
}