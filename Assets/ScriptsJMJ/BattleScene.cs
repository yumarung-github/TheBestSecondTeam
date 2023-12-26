using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PLAYER
{
    p1,
    p2,
}

public class BattleScene : MonoBehaviour
{
    public PLAYER playertype;
    private bool isMove = true;
    int p1number;
    int p2number;
    public CinemachineVirtualCamera[] cinemachines = new CinemachineVirtualCamera[6];
    private void Start()
    {
        //PlayerSetting(1, 2);
        switch (playertype)
        {
            case PLAYER.p1:
                Invoke("StopObj", 5.5f);
                Invoke("Player1SetActiveFalse", 8f);
                transform.GetChild(p1number).gameObject.SetActive(true);
                for (int i = 0; i < 3; i++)
                {
                    cinemachines[i].LookAt = transform.GetChild(p1number).gameObject.transform;
                }
                break;
            case PLAYER.p2:
                this.gameObject.SetActive(false);
                Invoke("StartP2", 7.5f);
                Invoke("StopObj", 13f);
                Invoke("Player2SetActiveFalse", 15f);
                for (int i = 3; i < 6; i++)
                {
                    cinemachines[i].LookAt = transform.GetChild(p2number).gameObject.transform;
                }
                break;
        }
    }

    private void Player1SetActiveFalse()
    {
        transform.GetChild(p1number).gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
    private void Player2SetActiveFalse()
    {
        transform.GetChild(p2number).gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
    void StopObj()
    {
        isMove = false;
    }
    private void StartP2()
    {
        this.gameObject.SetActive(true);
        transform.GetChild(p2number).gameObject.SetActive(true);
    }
    private void Update()
    {
        if (isMove)
            transform.Translate(Vector3.forward * 3f * Time.deltaTime, Space.Self);
    }
    public void PlayerAction()
    {
        transform.position += (Vector3.forward * 1.5f);
    }
    public void PlayerSetting(int p1num, int p2num)
    {
        p1number = p1num;
        p2number = p2num;
    }
}