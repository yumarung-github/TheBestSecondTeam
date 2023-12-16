using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatUI : MonoBehaviour
{
    public Button emotionBtn;
    [Header("[프로필]")]
    public GameObject profileWindow;
    public GameObject profile;


    [Header("[값]")]
    public TextMeshProUGUI remainSolText;
    public TextMeshProUGUI woodProductText;
    public TextMeshProUGUI craftCardText;
    
    public TextMeshProUGUI actionNumText;
    
    public TextMeshProUGUI sawmillConsumeText;
    public TextMeshProUGUI workshopConsumeText;
    public TextMeshProUGUI barracksConsumeText;

    public TextMeshProUGUI sawmillCostText;
    public TextMeshProUGUI workshopCostText;
    public TextMeshProUGUI barracksCostText;
}
