using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInterface;

public class CatWaitState : RmState
{
    public CatWaitState()
    {
        name = "Ĺ���";
    }
    public override void Enter()
    {
        Debug.Log("Ĺ�����");
        if (cat != null && wood != null && bird != null)
        {
            Debug.Log("dd");
        }
        rm.turnText.text = "���� �� : ����� ����";

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
        name = "Ĺ��ħ";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : ����� ����/ ��ħ / ����";
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.CAT_AFTERNOON);
            Debug.Log("�̵�����");
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
        name = "Ĺ����";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : ����� ����/ ���� / �̵�";
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sm.SetState(MASTATE_TYPE.CAT_DINNER);
            Debug.Log("�̵��Ұ�");
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
        name = "Ĺ����";
    }
    public override void Enter()
    {
        rm.turnText.text = "���� �� : ����� ����/ ����";
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

