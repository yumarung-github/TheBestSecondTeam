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
        //Debug.Log("캣대기중");
        if (cat != null && wood != null && bird != null)//디버깅용
        {
            //Debug.Log("dd");
        }
        rm.turnText.text = "현재 턴 : 고양이 후작";
        rm.nowPlayer = cat;
        rm.SetNext(MASTATE_TYPE.CAT_MORNING);//다음버튼에 다음으로 넘어갈수있게 넣어줌
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
        name = "캣아침";
    }
    public override void Enter()
    {
        rm.turnText.text = "현재 턴 : 고양이 후작/ 아침 / 선택";
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
        name = "캣점심";
    }
    public override void Enter()
    {
        rm.turnText.text = "현재 턴 : 고양이 후작/ 점심 / 이동";
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
        name = "캣저녁";
    }
    public override void Enter()
    {
        rm.turnText.text = "현재 턴 : 고양이 후작/ 저녁";
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

