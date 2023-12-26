using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleScene : MonoBehaviour
{
    private bool isMove = true;
    private IEnumerator player1MoveCo;
    private IEnumerator player2MoveCo;
    
    public CinemachineVirtualCamera[] cinemachines = new CinemachineVirtualCamera[6];
    public Canvas uiCanvas1;
    public Canvas uiCanvas2;
    public Canvas uiCanvas3;
    public Image battleUI;
    public Image battleCardImage;
    public CinemachineVirtualCamera cinemachineCamera;
    public float player1StopNum = 0;
    public float player2StopNum = 0;

    private void Start()
    {
        player1MoveCo = Player1MoveCo();
        player2MoveCo = Player2MoveCo();
        switch(BattleManager.Instance.battleP1.name)
        {
            case "Cat":
                transform.GetChild(0).gameObject.SetActive(true);
                StartCoroutine(player1MoveCo);
                break;
            case "Bird":
                transform.GetChild(1).gameObject.SetActive(true);
                StartCoroutine(player1MoveCo);
                break;
            case "Wood":
                transform.GetChild(2).gameObject.SetActive(true);
                StartCoroutine(player1MoveCo);
                break;
        }
        switch(BattleManager.Instance.battleP2.name)
        {
            case "Cat":
                transform.GetChild(3).gameObject.SetActive(true);
                StartCoroutine(player2MoveCo);
                break;
            case "Bird":
                transform.GetChild(4).gameObject.SetActive(true);
                StartCoroutine(player2MoveCo);
                break;
            case "Wood":
                transform.GetChild(5).gameObject.SetActive(true);
                StartCoroutine(player2MoveCo);
                break;
        }
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        for(int i = 0; i < 3; i++)
        {
            if(transform.GetChild(i).gameObject.activeSelf == true)
            {
                cinemachines[0].LookAt = transform.GetChild(i).gameObject.transform;
                cinemachines[1].LookAt = transform.GetChild(i).gameObject.transform;
                cinemachines[2].LookAt = transform.GetChild(i).gameObject.transform;
            }
        }
        for(int j = 3; j < 6; j++)
        {
            if (transform.GetChild(j).gameObject.activeSelf == true)
            {
                cinemachines[3].LookAt = transform.GetChild(j).gameObject.transform;
                cinemachines[4].LookAt = transform.GetChild(j).gameObject.transform;
                cinemachines[5].LookAt = transform.GetChild(j).gameObject.transform;
            }
        }
    }
    
    public void UION()
    {
        uiCanvas1.gameObject.SetActive(true);
        uiCanvas2.gameObject.SetActive(true);
        uiCanvas3.gameObject.SetActive(false);
        battleCardImage.gameObject.SetActive(false);
        battleUI.gameObject.SetActive(false);
        cinemachineCamera.Priority = 11;
    }
    IEnumerator Player1MoveCo()
    {
        while(player1StopNum < 19.5f)
        {
            player1StopNum += Time.deltaTime;
            transform.Translate(Vector3.forward * 1.5f * Time.deltaTime);
            yield return null;
        }
        UION();
    }
    IEnumerator Player2MoveCo()
    {
        while (player2StopNum < 19.5f)
        {
            player2StopNum += Time.deltaTime;
            transform.Translate(Vector3.back * 1.5f * Time.deltaTime);
            yield return null;
        }
    }
}