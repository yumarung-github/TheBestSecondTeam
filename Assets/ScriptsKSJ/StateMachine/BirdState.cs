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
        rm.SetNext(MASTATE_TYPE.BIRD_MORNING);
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
        rm.nowPlayer = bird;
        rm.SetSpawnBtn();
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
        rm.SetNext(MASTATE_TYPE.BIRD_AFTERNOON);
    }
    public override void Update()
    {
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
        rm.SetNext(MASTATE_TYPE.BIRD_DINNER);
    }
    public override void Update()
    {
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
        rm.SetNext(MASTATE_TYPE.WOOD_WAIT);
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
        bird.isOver = true;
        wood.isOver = false;
        rm.nowPlayer = null;
    }
}