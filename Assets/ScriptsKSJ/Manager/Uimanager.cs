using CustomInterface;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Uimanager : SingleTon<Uimanager>
{



    // 현재 플레이어 턴 텍스트
    
    public PlayerUI playerUI;
    public WoodUi woodUi;

    [Header("[카드 정보창]")]

    public GameObject cardWindow;

    public TextMeshProUGUI cardName;

    public TextMeshProUGUI cardInfo;
    private new void Awake()
    {
        base.Awake();
    }


}
