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
        rm.nowPlayer = cat;

        // 다음버튼에 다음으로 넘어갈수있게 넣어줌
        Uimanager.Instance.turnText.text = "현재 턴 : 고양이 후작";
        Uimanager.Instance.SetNextBtn(MASTATE_TYPE.CAT_MORNING);
    }
    public override void Update()
    {
        if (cat.isOver == false)
        {

        }
    }
    public override void Exit()
    {
        Uimanager.Instance.SetBuildBtn();
        Uimanager.Instance.SetSelectBtn();
        Uimanager.Instance.SetSpawnBtn();
        Uimanager.Instance.SetMoveBtn();
        Uimanager.Instance.SetBattleBtn();
        Uimanager.Instance.ResetBtn(false);
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
        Uimanager.Instance.turnText.text = "현재 턴 : 고양이 후작/ 아침 / 선택";
        Uimanager.Instance.SetNextBtn(MASTATE_TYPE.CAT_AFTERNOON);
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        Uimanager.Instance.ResetBtn(true);
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
        Uimanager.Instance.turnText.text = "현재 턴 : 고양이 후작/ 점심 / 이동";
        Uimanager.Instance.SetNextBtn(MASTATE_TYPE.CAT_DINNER);
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        Uimanager.Instance.ResetBtn(false);
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
        Uimanager.Instance.turnText.text = "현재 턴 : 고양이 후작/ 저녁";
        Uimanager.Instance.SetNextBtn(MASTATE_TYPE.BIRD_WAIT);
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
        Uimanager.Instance.ResetBtn(false);

    }
}

