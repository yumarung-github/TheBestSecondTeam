using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Uimanager : SingleTon<Uimanager>
{
    [Header("[카드 정보창]")]
    
    public GameObject cardWindow;

    public TextMeshProUGUI cardName;

    public TextMeshProUGUI cardInfo;
    private new void Awake()
    {
        base.Awake();

    }

    void Update()
    {
        
    }
}
