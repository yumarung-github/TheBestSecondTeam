using CustomInterface;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Uimanager : SingleTon<Uimanager>
{

    public Button testBtn; // 임시 테스트버튼

    // 현재 플레이어 턴 텍스트

    public PlayerUI playerUI;
    public WoodUi woodUi;
    public BirdCardSlot birdCardSlot;

    [Header("[카드 정보창]")]

    public GameObject cardWindow;

    public TextMeshProUGUI cardName;

    public TextMeshProUGUI cardInfo;
    [Header("[인벤토리 UI]")]
    public GameObject catInven;
    public GameObject birdInven;
    public GameObject woodInven;
    private new void Awake()
    {
        base.Awake();
    }


}
