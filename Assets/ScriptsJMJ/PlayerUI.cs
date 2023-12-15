using CustomInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class PlayerUI : MonoBehaviour
{
    public GameObject battleWindow;
    public Button[] buttons = new Button[2];
    public GameObject soldierMove; //모병시 UI
    public TextMeshProUGUI turnText;    // 현재 플레이어 턴 텍스트


    [Header("버튼")]
    public Button spawnBtn;
    public Button battleBtn;
    public Button buildBtn;
    public Button moveBtn;
    public Button nextBtn;

    Player player;
    public bool isOn;

    private void Start()
    {
        player = RoundManager.Instance.nowPlayer;
        moveBtn.onClick.AddListener(() => { RoundManager.Instance.testType = RoundManager.SoldierTestType.MoveSelect; });
        isOn = true;
        //buttons = battleWindow.transform.GetComponentsInChildren<Button>();
    }
    public void MoveSoldier()
    {
        if (isOn)
        {
            soldierMove.SetActive(true);
            RoundManager.Instance.testType = RoundManager.SoldierTestType.Move;

        }
        else
            soldierMove.SetActive(false);
        isOn = !isOn;
    }


    // 버튼들은 현재 라운드상태머신의 고양이 대기상태에서 초기화중임.

    // 소환 버튼 설정
    public void SpawnSoldier()
    {
        spawnBtn.onClick.RemoveAllListeners();       
        
        spawnBtn.onClick.AddListener(() =>
        {
            if (RoundManager.Instance.nowPlayer.hasNodeNames.Count > 0)
            {
                string tempName = RoundManager.Instance.nowPlayer.hasNodeNames[0];
                RoundManager.Instance.nowPlayer.SpawnSoldier(tempName,
                RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == tempName).transform);
            }            
        });
    }

    public void SetNextBtn(MASTATE_TYPE curState)
    {
        nextBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.AddListener(() =>
        {
            RoundManager.Instance.roundSM.SetState(curState);
        });
    }

    public void SetBuildBtn()
    {
        buildBtn.onClick.RemoveAllListeners();
        //string tempName = RoundManager.Instance.nowPlayer.hasNodeNames[0];
        buildBtn.onClick.AddListener(() => {
            RoundManager.Instance.testType = RoundManager.SoldierTestType.Build;
        });
    }
    public void SetBattleBtn()
    {
        battleBtn.onClick.RemoveAllListeners();
        //string tempName = RoundManager.Instance.nowPlayer.hasNodeNames[0];
        battleBtn.onClick.AddListener(() => { Debug.Log("전투 !"); });
        battleBtn.onClick.AddListener(() => 
        {
            RoundManager.Instance.testType = RoundManager.SoldierTestType.Battle;
            
        });


    }
    public void ResetBtn(bool turn)
    {
        battleBtn.gameObject.SetActive(turn);
        moveBtn.gameObject.SetActive(turn);
        spawnBtn.gameObject.SetActive(turn);
        buildBtn.gameObject.SetActive(turn);
    }
}



