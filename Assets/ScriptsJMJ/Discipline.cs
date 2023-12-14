using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.GridLayoutGroup;


public enum RULE_TYPE
{
    MUST_SPAWN,
    MUST_MOVE,
    MUST_BATTLE,
    MUST_BULID
    //2개의 규율은 무조건 수행
}



public class Discipline : MonoBehaviour
{
    public Bird bird;
    public Func<bool> IsBreakRule;
    public UnityEvent OnBreakingRule;
    public RULE_TYPE ruleType;



    private void Start()
    {
        OnBreakingRule.AddListener(() => { RoundManager.Instance.bird.NowLeader = LEADER_TYPE.NONE; });
        OnBreakingRule.AddListener(() => { Uimanager.Instance.birdUI.birdLeaderSelect.SetActive(true); });

        if (ruleType == RULE_TYPE.MUST_MOVE)
            IsBreakRule = () => { return bird.isMoved == false; };
        if (ruleType == RULE_TYPE.MUST_SPAWN)
            IsBreakRule = () => { return bird.isSpwaned == false; };
        if (ruleType == RULE_TYPE.MUST_BULID)
            IsBreakRule = () => { return bird.isBuilded == false; };
        if (ruleType == RULE_TYPE.MUST_BATTLE)
            IsBreakRule = () => { return bird.isBattled == false; };
    }

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
       {
           NextTurn();
       }
    }
    public void NextTurn()
    {
        if (IsBreakRule.Invoke())
        {
            BreakingRule();
        }  
        else
        {

        }
    }

    public void BreakingRule()
    {
        Debug.LogWarning("룰을 어김");
        OnBreakingRule.Invoke();
        Destroy(gameObject);
        
    }



}

