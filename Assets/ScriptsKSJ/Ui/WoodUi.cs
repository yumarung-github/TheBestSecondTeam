using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WoodUi : MonoBehaviour
{
    [Header("[버튼들]")]
    public Button agreeBtn;//동조
    public Button revoitBtn;//반란
    public Button craftBtn;//제작
    public Button officerBtn;//장교
    public Button supporterBtn;//지지자결집

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
    public TextMeshProUGUI foxSupportNumText;
    public TextMeshProUGUI ratSupportNumText;
    public TextMeshProUGUI rabbitSupportNumText;
    public TextMeshProUGUI birdSupportNumText;

    [Header("[카드 체크]")]
    public List<Card> cards = new List<Card>();
    public enum CardUseType 
    {
        NONE,
        CRAFT,
        SUPPORT
    }
    public CardUseType cardUseType;

    void Start()
    {
        
    }
    public void SetCraftBtn()
    {
        craftBtn.onClick.AddListener(() =>
        {
            cardUseType = CardUseType.CRAFT;
        });
    }    
    public void SetSupportBtn()
    {
        supporterBtn.onClick.AddListener(() =>
        {
            cardUseType = CardUseType.SUPPORT;
        });
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
