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
        rm.nowPlayer = bird;
        Uimanager.Instance.playerUI.SetTurnTexts("이어리 왕조 \n대기");
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.BIRD_MORNING);
        if (RoundManager.Instance.bird.NowLeader == LEADER_TYPE.NONE)
            Uimanager.Instance.birdUI.birdLeaderSelect.SetActive(true);
        else
        {
            Uimanager.Instance.birdUI.birdLeaderSelect.SetActive(false);
            Uimanager.Instance.birdUI.birdCardBox.SetActive(true);
        }
    }
    public override void Update()
    {

    }
    public override void Exit()
    {
        Uimanager.Instance.playerUI.SpawnSoldier();
        
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
        Uimanager.Instance.playerUI.SetTurnTexts("이어리 왕조 \n아침");
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
        Uimanager.Instance.playerUI.SetTurnTexts("이어리 왕조 \n점심");
        Uimanager.Instance.birdUI.SequenceBox.SetActive(true);
        Uimanager.Instance.birdUI.BirdInventory.UseSlot();
        Uimanager.Instance.playerUI.ResetBtn(true);
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.BIRD_DINNER);
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
        Uimanager.Instance.birdUI.SequenceBox.SetActive(false);
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
        Uimanager.Instance.playerUI.SetTurnTexts("이어리 왕조 \n저녁");
        Uimanager.Instance.playerUI.SetNextBtn(MASTATE_TYPE.WOOD_WAIT);
    }
    public override void Update()
    {
    }
    public override void Exit()
    {
        bird.isOver = true;
        wood.isOver = false;
        rm.nowPlayer = null;
        Uimanager.Instance.birdInven.SetActive(false);
    }
}