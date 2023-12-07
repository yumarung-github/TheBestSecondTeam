using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInterface;
using UnityEngine.UI;
using System;
using TMPro;
using Unity.VisualScripting;

public class RoundManager : SingleTon<RoundManager>
{

    [Header("[플레이어들]")]
    public Cat cat;//플레이어중에 고양이 
    public Bird bird;//플레이어중에 이어리
    public Wood wood;//플레이어중에 우드랜드
    public ManagerStateMahchine<RoundManager> roundSM;//라운드 상태머신
    public Player nowPlayer;// 현재 턴인 플레이어 (라운드 상태머신에서 자동으로 바뀜)
    public bool moveOver; // mapcontroller에서 최종 도착지까지 가기위해 이동이 끝나면 다음 타일로 이동하기 위해 선언해둔 것
    [Header("스폰 테스트")]
    public Button spawnButton;//모병(소환) 버튼
    public Button selectButton;//선택 버튼
    public Button moveBtn;//행군(이동) 버튼
    public Button nextBtn;//다음턴 버튼
    public MapExtra mapExtra;// 최단거리 이동 계산하기위해 스크립트 넣어놓은것
    public TextMeshProUGUI turnText;//현재 턴에 대한 텍스트
    public enum SoldierTestType//버튼으로 모병이나 선택이나 이런거 클릭할때의 기능이 바뀌게됨
    {
        None,
        Move,
        Select,
        Spawn
    }
    //예외처리 거의 안되어있음.
    public SoldierTestType testType;

    [Header("[맵]")]
    public MapController mapController;//맵 이동하는거나 선택하거나 클릭하는 모든것들 스크립트

    private new void Awake()
    {
        base.Awake();
        roundSM = new ManagerStateMahchine<RoundManager>(this);
        mapExtra = transform.GetComponent<MapExtra>();
    }
    // Start is called before the first frame update
    void Start()
    {
        moveOver = true;
        testType = SoldierTestType.None;
        roundSM.AddStateDic(MASTATE_TYPE.CAT_WAIT, new CatWaitState());
        roundSM.AddStateDic(MASTATE_TYPE.CAT_MORNING, new CatMorningState());
        roundSM.AddStateDic(MASTATE_TYPE.CAT_AFTERNOON, new CatAfternoonState());
        roundSM.AddStateDic(MASTATE_TYPE.CAT_DINNER, new CatDinnerState());
        roundSM.SetState(MASTATE_TYPE.CAT_WAIT);
        roundSM.AddStateDic(MASTATE_TYPE.BIRD_WAIT, new BirdWaitState());
        roundSM.AddStateDic(MASTATE_TYPE.BIRD_MORNING, new BirdMorningState());
        roundSM.AddStateDic(MASTATE_TYPE.BIRD_AFTERNOON, new BirdAfternoonState());
        roundSM.AddStateDic(MASTATE_TYPE.BIRD_DINNER, new BirdDinnerState());
        roundSM.AddStateDic(MASTATE_TYPE.WOOD_WAIT, new WoodWaitState());
        roundSM.AddStateDic(MASTATE_TYPE.WOOD_MORNING, new WoodMorningState());
        roundSM.AddStateDic(MASTATE_TYPE.WOOD_AFTERNOON, new WoodAfternoonState());
        roundSM.AddStateDic(MASTATE_TYPE.WOOD_DINNER, new WoodDinnerState());
        //상태머신에 상태들 추가한것
    }
    private void Update()
    {
        roundSM.Update();//상태가 변화되면 상태머신의 update가 실행된 상태.
    }
    //버튼들은 현재 라운드상태머신의 고양이 대기상태에서 초기화중임.
    public void SetSpawnBtn()//소환 버튼 설정
    {
        spawnButton.onClick.RemoveAllListeners();
        string tempName = nowPlayer.hasNodeNames[0];
        spawnButton.onClick.AddListener(() => {
            nowPlayer.SpawnSoldier(tempName,
            mapExtra.mapTiles.Find(node => node.nodeName == tempName).transform);
        });
    }
    public void SetMoveBtn()//이동 버튼 설정
    {
        moveBtn.onClick.RemoveAllListeners();
        moveBtn.onClick.AddListener(() =>
        {
            testType = SoldierTestType.Move;
        });
    }
    public void SetSelectBtn()//이동 버튼 설정
    {
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() =>
        {
            testType = SoldierTestType.Select;
        });
    }
    public void SetNext(MASTATE_TYPE curState)//이동 버튼 설정
    {
        nextBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.AddListener(() =>
        {
            roundSM.SetState(curState);

        });

    }
}

