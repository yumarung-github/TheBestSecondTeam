using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WoodUi : MonoBehaviour
{
    [Header("[버튼들]")]
    public Button agreeBtn;
    public Button revoitBtn;
    public Button craftBtn;
    public Button officerBtn;
    public Button supporterBtn;

    public Button emotionBtn;
    [Header("[프로필]")]
    public GameObject profileWindow;
    public GameObject profile;
    public GameObject foxBuildImage;
    public GameObject ratBuildImage;
    public GameObject rabbitBuildImage;

    //public GameObject remainSolImage;
    //public GameObject officerImage;
    //public GameObject craftCardImage;

    [Header("[값]")]
    public TextMeshProUGUI remainSolText;
    public TextMeshProUGUI officerText;
    public TextMeshProUGUI craftCardText;

    [Header("[카드 체크]")]
    public List<Card> cards = new List<Card>();

    void Start()
    {
        
    }

    public void SetWoodMorning1()
    {
        revoitBtn.gameObject.SetActive(true);
    }
    public void SetWoodMorning2()
    {
        revoitBtn.gameObject.SetActive(false);
        agreeBtn.gameObject.SetActive(true);
    }
    public void SetAfternoon(bool onOff)
    {
        agreeBtn.gameObject.SetActive(false);
        craftBtn.gameObject.SetActive(onOff);
        officerBtn.gameObject.SetActive(onOff);
        supporterBtn.gameObject.SetActive(onOff);
    }
}
