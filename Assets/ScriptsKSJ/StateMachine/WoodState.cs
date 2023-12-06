using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInterface;

public class WoodWaitState : RmState
{
    public WoodWaitState()
    {
        name = "�����";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : ��巣��";
        rm.SetNext(MASTATE_TYPE.WOOD_MORNING);//���� ��ư ���� ��������.
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
        name = "����ħ";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : ��巣�� / ��ħ";
        rm.SetNext(MASTATE_TYPE.WOOD_AFTERNOON);//���� ��ư ���� ��������.
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
        name = "�������";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : ��巣�� / ����";
        rm.SetNext(MASTATE_TYPE.WOOD_DINNER);//���� ��ư ���� ��������.
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
        name = "�������";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : ��巣�� / ����";
        rm.SetNext(MASTATE_TYPE.CAT_WAIT);//���� ��ư ���� ��������.
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