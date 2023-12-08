using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerUI : MonoBehaviour
{
    public GameObject slodierMove; //모병시 UI

    SoldierChoice soldierChoice;

    public Button move;
    public Button spawn;
    public Button next;

    public Player player;

    public bool isOn;
    public bool moveCheck;

    private void Start()
    {
        move.onClick.AddListener(MoveSoldier);
        spawn.onClick.AddListener(SpawnSoldier);
        soldierChoice = GetComponentInChildren<SoldierChoice>();
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

    public void SpawnSoldier()
    {
        spawn.onClick.RemoveAllListeners();
        string tempName = RoundManager.Instance.nowPlayer.hasNodeNames[0];
        spawn.onClick.AddListener(() =>
        {
            RoundManager.Instance.nowPlayer.SpawnSoldier(tempName,
            RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == tempName).transform);
        });
    }
}



