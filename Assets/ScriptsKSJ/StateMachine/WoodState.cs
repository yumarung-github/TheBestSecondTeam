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
    }
    public override void Update()
    {
        if (wood.isOver == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                sm.SetState(MASTATE_TYPE.WOOD_MORNING);
                rm.nowPlayer = wood;
                rm.SetSpawnBtn();
                rm.testType = RoundManager.SoldierTestType.Select;
            }
        }
    }
    public override void Exit()
    {

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
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.WOOD_AFTERNOON);
            Debug.Log("이동가능");
            rm.testType = RoundManager.SoldierTestType.Move;
        }
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
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.WOOD_DINNER);
            Debug.Log("이동불가");
            rm.testType = RoundManager.SoldierTestType.None;
        }
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
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.CAT_WAIT);
        }
    }
    public override void Exit()
    {
        RoundManager.Instance.wood.isOver = true;
        RoundManager.Instance.cat.isOver = false;
        rm.nowPlayer = null;
    }
}