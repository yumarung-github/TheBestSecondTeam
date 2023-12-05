using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInterface;

public class CatWaitState : RmState
{
    public CatWaitState()
    {
        name = "캣대기";
    }
    public override void Enter()
    {
        Debug.Log("캣대기중");
        if (cat != null && wood != null && bird != null)
        {
            Debug.Log("dd");
        }
        rm.turnText.text = "현재 턴 : 고양이 후작";

    }
    public override void Update()
    {
        if (cat.isOver == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                sm.SetState(MASTATE_TYPE.CAT_MORNING);
                rm.nowPlayer = cat;
                rm.testType = RoundManager.SoldierTestType.Select;
                rm.SetSpawnBtn();
            }
        }
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
        rm.turnText.text = "현재 턴 : 고양이 후작/ 아침 / 선택";
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.CAT_AFTERNOON);
            Debug.Log("이동가능");
            rm.testType = RoundManager.SoldierTestType.Move;
        }
    }
    public override void Exit()
    {

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
        rm.turnText.text = "현재 턴 : 고양이 후작/ 점심 / 이동";
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.CAT_DINNER);
            Debug.Log("이동불가");
            rm.mapController.nowTile = null;
            rm.testType = RoundManager.SoldierTestType.None;
        }
    }
    public override void Exit()
    {

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
        rm.turnText.text = "현재 턴 : 고양이 후작/ 저녁";
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.BIRD_WAIT);
        }
    }
    public override void Exit()
    {
        cat.isOver = true;
        bird.isOver = false;
        rm.nowPlayer = null;
    }
}

