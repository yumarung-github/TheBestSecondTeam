using CustomInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class PlayerUI : MonoBehaviour
{
    public GameObject slodierMove; //모병시 UI
    public TextMeshProUGUI turnText;    // 현재 플레이어 턴 텍스트


    [Header("버튼")]
    public Button spawnBtn;
    public Button battleBtn;
    public Button buildBtn;
    public Button moveBtn;
    public Button nextBtn;

    Player player;
    public bool isOn;
    public bool moveCheck;

    private void Start()
    {
        player = RoundManager.Instance.nowPlayer;
        moveBtn.onClick.AddListener(MoveSoldier);
        isOn = true;
    }

    public void MoveSoldier()
    {
        Debug.Log("이동할 병사를 선택 하세요");
        RoundManager.Instance.testType = RoundManager.SoldierTestType.Select;

        if (moveCheck)
        {
            isOn = !isOn;
            if (isOn)
            {
                slodierMove.SetActive(true);
                RoundManager.Instance.testType = RoundManager.SoldierTestType.Move;
            }
            else
                slodierMove.SetActive(false);
        }

    }


    // 버튼들은 현재 라운드상태머신의 고양이 대기상태에서 초기화중임.

    // 소환 버튼 설정
    public void SpawnSoldier()
    {
        spawnBtn.onClick.RemoveAllListeners();
        string tempName = RoundManager.Instance.nowPlayer.hasNodeNames[0];
        spawnBtn.onClick.AddListener(() =>
        {
            RoundManager.Instance.nowPlayer.SpawnSoldier(tempName,
            RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == tempName).transform);
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
        string tempName = RoundManager.Instance.nowPlayer.hasNodeNames[0];
        buildBtn.onClick.AddListener(() => {
            Debug.Log("건설 !");
        });
    }
    public void SetBattleBtn()
    {
        battleBtn.onClick.RemoveAllListeners();
        string tempName = RoundManager.Instance.nowPlayer.hasNodeNames[0];
        battleBtn.onClick.AddListener(() => {
            Debug.Log("전투 !");
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



