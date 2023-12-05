using CustomInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierStateMachine 
{
    public Soldier soldier; 
    public SoldierState curState; 

    public Dictionary<STATE_TYPE, SoldierState> stateDic = new Dictionary<STATE_TYPE, SoldierState>();

    public SoldierStateMachine(Soldier soldier)
    {
        this.soldier = soldier;
    }

    public void AddStateDic(STATE_TYPE type, SoldierState state)//���µ�
    {
        if (stateDic.ContainsKey(type) == true)
        {
            return;
        }
        stateDic.Add(type, state);

        state.sm = this;
    }
    public void SetState(STATE_TYPE type)
    {
        if (stateDic.ContainsKey(type) == true)
        {
            if (curState != null)
            {
                curState.Exit();//���� ���°� ����
            }
            curState = stateDic[type];
            curState.Enter(); // ��������
        }

    }
    public void Update()
    {
        curState?.Update();
    }
}
