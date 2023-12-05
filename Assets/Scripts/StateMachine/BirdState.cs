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
        name = "����ħ";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : �̾ ���� / ��ħ";
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.BIRD_AFTERNOON);
            Debug.Log("�̵�����");
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
        name = "������";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : �̾ ���� / ����";
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.BIRD_DINNER);
            Debug.Log("�̵��Ұ�");
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
        name = "������";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : �̾ ���� / ����";
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