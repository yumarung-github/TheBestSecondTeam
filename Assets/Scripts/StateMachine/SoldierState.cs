using CustomInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoldierState
{
    public SoldierStateMachine sm; //�� ������ ������
    public string testString;
    public SoldierState()
    {

    }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
public class IdleState : SoldierState
{
    public IdleState()
    {
        testString = "Idle";
    }
    public override void Enter()
    {
        Debug.Log(sm.curState.testString + "����");
        sm.soldier.animator.SetFloat("speed", 0);
    }
    public override void Update()
    {
        if (sm.soldier.agentSpeed > 0)
        {
            Debug.Log("�����Ұž�");
            sm.SetState(STATE_TYPE.MOVE);
        }
        
        //Debug.Log("������");
    }
    public override void Exit()
    {
        Debug.Log(sm.curState.testString + "����");
    }
}
public class MoveState : SoldierState
{
    public MoveState()
    {
        testString = "Auto";
    }
    public override void Enter()
    {
        Debug.Log(sm.curState.testString + "����");
    }
    public override void Update()
    {
        //Debug.Log("������");
        if (sm.soldier.agentSpeed == 0)
        {
            sm.SetState(STATE_TYPE.IDLE);
        }
        sm.soldier.animator.SetFloat("speed", sm.soldier.agentSpeed);
    }
    public override void Exit()
    {
        Debug.Log(sm.curState.testString + "����");
        sm.soldier.agent.enabled = false;
    }
}