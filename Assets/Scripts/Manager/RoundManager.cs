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

    [Header("[�÷��̾��]")]
    public Cat cat;
    public Bird bird;
    public Wood wood;
    public ManagerStateMahchine<RoundManager> roundSM;
    public Player nowPlayer;
    public bool moveOver;
    [Header("���� ���õ� ����")]
    public List<Soldier> nowSoldier = new List<Soldier>();
    [Header("���� �׽�Ʈ")]
    public Button spawnButton;
    public Button moveBtn;
    public MapExtra mapExtra;
    public TextMeshProUGUI turnText;
    public enum SoldierTestType
    {
        None,
        Move,
        Select
    }
    public SoldierTestType testType;

    [Header("[��]")]
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
    public void SetSpawnBtn()
    {
        spawnButton.onClick.RemoveAllListeners();
        string tempName = nowPlayer.hasNodeNames[0];
        spawnButton.onClick.AddListener(() => {
            nowPlayer.SpawnSoldier(tempName,
            mapExtra.mapTiles.Find(node => node.nodeName == tempName).transform);
        });
    }
}

