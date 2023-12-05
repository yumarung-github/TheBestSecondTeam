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
    public Cat cat;
    public Bird bird;
    public Wood wood;
    public ManagerStateMahchine<RoundManager> roundSM;
    public Player nowPlayer;
    public bool moveOver;
    [Header("현재 선택된 병사")]
    public List<Soldier> nowSoldier = new List<Soldier>();
    [Header("스폰 테스트")]
    public Button spawnButton;
    public Button selectButton;
    public Button moveBtn;
    public Button nextBtn;
    public MapExtra mapExtra;
    public TextMeshProUGUI turnText;
    public enum SoldierTestType
    {
        None,
        Move,
        Select,
        Spawn
    }
    public SoldierTestType testType;

    [Header("[맵]")]
    public MapController mapController;

    private new void Awake()
    {
        base.Awake();
        roundSM = new ManagerStateMahchine<RoundManager>(this);
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
        
    }
    private void Update()
    {
        roundSM.Update();
    }
    public void SetSpawnBtn()//소환버튼 설정
    {
        spawnButton.onClick.RemoveAllListeners();
        string tempName = nowPlayer.hasNodeNames[0];
        spawnButton.onClick.AddListener(() => {
            nowPlayer.SpawnSoldier(tempName,
            mapExtra.mapTiles.Find(node => node.nodeName == tempName).transform);
        });
    }
    public void SetMoveBtn()
    {
        moveBtn.onClick.RemoveAllListeners();
        moveBtn.onClick.AddListener(() =>
        {
            testType = SoldierTestType.Move;
        });
    }
    public void SetSelectBtn()
    {
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() =>
        {
            testType = SoldierTestType.Select;
        });
    }
    public void SetNext(MASTATE_TYPE curState)
    {
        nextBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.AddListener(() =>
        {
            roundSM.SetState(curState);

        });

    }
}

