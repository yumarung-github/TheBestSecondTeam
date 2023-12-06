using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInterface;

public class BirdWaitState : RmState
{
    public BirdWaitState()
    {
        name = "�����";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : �̾ ����";
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
        name = "����ħ";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : �̾ ���� / ��ħ";
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
        name = "������";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : �̾ ���� / ����";
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
        name = "������";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : �̾ ���� / ����";
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