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
    public Cat cat;//�÷��̾��߿� ����� 
    public Bird bird;//�÷��̾��߿� �̾
    public Wood wood;//�÷��̾��߿� ��巣��
    public ManagerStateMahchine<RoundManager> roundSM;//���� ���¸ӽ�
    public Player nowPlayer;// ���� ���� �÷��̾� (���� ���¸ӽſ��� �ڵ����� �ٲ�)
    public bool moveOver; // mapcontroller���� ���� ���������� �������� �̵��� ������ ���� Ÿ�Ϸ� �̵��ϱ� ���� �����ص� ��
    [Header("���� �׽�Ʈ")]
    public Button spawnButton;//��(��ȯ) ��ư
    public Button selectButton;//���� ��ư
    public Button moveBtn;//�౺(�̵�) ��ư
    public Button nextBtn;//������ ��ư
    public MapExtra mapExtra;// �ִܰŸ� �̵� ����ϱ����� ��ũ��Ʈ �־������
    public TextMeshProUGUI turnText;//���� �Ͽ� ���� �ؽ�Ʈ
    public enum SoldierTestType//��ư���� ���̳� �����̳� �̷��� Ŭ���Ҷ��� ����� �ٲ�Ե�
    {
        None,
        Move,
        Select,
        Spawn
    }
    //����ó�� ���� �ȵǾ�����.
    public SoldierTestType testType;

    [Header("[��]")]
    public MapController mapController;//�� �̵��ϴ°ų� �����ϰų� Ŭ���ϴ� ���͵� ��ũ��Ʈ

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
        //���¸ӽſ� ���µ� �߰��Ѱ�
    }
    private void Update()
    {
        roundSM.Update();//���°� ��ȭ�Ǹ� ���¸ӽ��� update�� ����� ����.
    }
    //��ư���� ���� ������¸ӽ��� ����� �����¿��� �ʱ�ȭ����.
    public void SetSpawnBtn()//��ȯ ��ư ����
    {
        spawnButton.onClick.RemoveAllListeners();
        string tempName = nowPlayer.hasNodeNames[0];
        spawnButton.onClick.AddListener(() => {
            nowPlayer.SpawnSoldier(tempName,
            mapExtra.mapTiles.Find(node => node.nodeName == tempName).transform);
        });
    }
    public void SetMoveBtn()//�̵� ��ư ����
    {
        moveBtn.onClick.RemoveAllListeners();
        moveBtn.onClick.AddListener(() =>
        {
            testType = SoldierTestType.Move;
        });
    }
    public void SetSelectBtn()//�̵� ��ư ����
    {
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() =>
        {
            testType = SoldierTestType.Select;
        });
    }
    public void SetNext(MASTATE_TYPE curState)//�̵� ��ư ����
    {
        nextBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.AddListener(() =>
        {
            roundSM.SetState(curState);

        });

    }
}

