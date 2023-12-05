using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInterface;

public class BirdWaitState : RmState
{
    public BirdWaitState()
    {
        name = "새대기";
    }
    public override void Enter()
    {
        rm.turnText.text = "현재 턴 : 이어리 왕조";
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rm.nowPlayer = bird;
            rm.SetSpawnBtn();
            sm.SetState(MASTATE_TYPE.BIRD_MORNING);
        }
    }
    public override void Exit()
    {

    }
}
public class BirdMorningState : RmState
{
    public BirdMorningState()
    {
        name = "새아침";
    }
    public override void Enter()
    {
        rm.turnText.text = "현재 턴 : 이어리 왕조 / 아침";
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.BIRD_AFTERNOON);
            Debug.Log("이동가능");
        }
    }
    public override void Exit()
    {

    }
}
public class BirdAfternoonState : RmState
{
    public BirdAfternoonState()
    {
        name = "새점심";
    }
    public override void Enter()
    {
        rm.turnText.text = "현재 턴 : 이어리 왕조 / 점심";
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.BIRD_DINNER);
            Debug.Log("이동불가");
        }
    }
    public override void Exit()
    {

    }
}
public class BirdDinnerState : RmState
{
    public BirdDinnerState()
    {
        name = "새저녁";
    }
    public override void Enter()
    {
        rm.turnText.text = "현재 턴 : 이어리 왕조 / 저녁";
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.WOOD_WAIT);
        }
    }
    public override void Exit()
    {
        bird.isOver = true;
        wood.isOver = false;
        rm.nowPlayer = null;
    }
}