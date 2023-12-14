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
        Uimanager.Instance.playerUI.turnText.text = "현재 턴 : 이어리 왕조";
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.BIRD_MORNING);
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        rm.NowPlayer = bird;
        Uimanager.Instance.playerUI.SpawnSoldier();
        BattleManager.Instance.InitBattle();
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
        Uimanager.Instance.playerUI.turnText.text = "현재 턴 : 이어리 왕조 / 아침";
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.BIRD_AFTERNOON);
        Uimanager.Instance.birdInven.SetActive(true);
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
        Uimanager.Instance.playerUI.turnText.text = "현재 턴 : 이어리 왕조 / 점심";
        Uimanager.Instance.playerUI.SpawnSoldier();
        Uimanager.Instance.playerUI.ResetBtn(true);
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.BIRD_DINNER);
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
        Uimanager.Instance.playerUI.ResetBtn(false);
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
        Uimanager.Instance.playerUI.turnText.text = "현재 턴 : 이어리 왕조 / 저녁";
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.WOOD_WAIT);
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
        bird.isOver = true;
        wood.isOver = false;
        rm.NowPlayer = null;
        Uimanager.Instance.birdInven.SetActive(false);
    }
}