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
        name = "����ħ";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : ��巣�� / ��ħ";
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.WOOD_AFTERNOON);
            Debug.Log("�̵�����");
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
        name = "�������";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : ��巣�� / ����";
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.WOOD_DINNER);
            Debug.Log("�̵��Ұ�");
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
        name = "�������";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : ��巣�� / ����";
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