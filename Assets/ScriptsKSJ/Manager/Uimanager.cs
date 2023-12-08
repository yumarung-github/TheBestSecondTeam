using CustomInterface;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Uimanager : SingleTon<Uimanager>
{
    [Header("스폰 테스트")]
    public ManagerStateMahchine<Uimanager> userInterfaceSM;
    public Button spawnBtn;
    public Button battleBtn;
    public Button buildBtn;
    public Button selectBtn;
    public Button moveBtn;
    public Button nextBtn;

    // 현재 플레이어 턴 텍스트
    public TextMeshProUGUI turnText;


    [Header("[카드 정보창]")]

    public GameObject cardWindow;

    public TextMeshProUGUI cardName;

    public TextMeshProUGUI cardInfo;
    private new void Awake()
    {
        base.Awake();
        userInterfaceSM = new ManagerStateMahchine<Uimanager>(this);
    }
    void Update()
    {

    }

    // 버튼들은 현재 라운드상태머신의 고양이 대기상태에서 초기화중임.
    
    // 소환 버튼 설정
    public void SetSpawnBtn()
    {
        spawnBtn.onClick.RemoveAllListeners();
        string tempName = RoundManager.Instance.nowPlayer.hasNodeNames[0];
        spawnBtn.onClick.AddListener(() => {
            RoundManager.Instance.nowPlayer.SpawnSoldier(tempName,
            RoundManager.Instance.mapExtra.mapTiles.Find(node => node.nodeName == tempName).transform);
        });
    }
    // 이동 버튼 설정
    public void SetMoveBtn()
    {
        moveBtn.onClick.RemoveAllListeners();
        moveBtn.onClick.AddListener(() =>
        {
            RoundManager.Instance.testType = RoundManager.SoldierTestType.Move;
        });
    }
    // 객체 선택
    public void SetSelectBtn()
    {
        selectBtn.onClick.RemoveAllListeners();
        selectBtn.onClick.AddListener(() =>
        {
            RoundManager.Instance.testType = RoundManager.SoldierTestType.Select;
        });
    }
    // 다음 버튼 설정
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
