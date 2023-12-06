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
        //Debug.Log("Ĺ�����");
        if (cat != null && wood != null && bird != null)
        {
            //Debug.Log("dd");
        }
        rm.turnText.text = "���� �� : ����� ����";
        rm.nowPlayer = cat;
        rm.SetNext(MASTATE_TYPE.CAT_MORNING);
    }
    public override void Update()
    {
        if (cat.isOver == false)
        {

        }
    }
    public override void Exit()
    {
        rm.SetSpawnBtn();
        rm.SetMoveBtn();
        rm.SetSelectBtn();
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
        rm.SetNext(MASTATE_TYPE.CAT_AFTERNOON);
    }
    public override void Update()
    {

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
        rm.SetNext(MASTATE_TYPE.CAT_DINNER);
    }
    public override void Update()
    {

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
        rm.SetNext(MASTATE_TYPE.BIRD_WAIT);
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
        rm.mapController.nowTile = null;
        cat.isOver = true;
        bird.isOver = false;
        rm.nowPlayer = null;
        
    }
}

