using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        
    }

    void Update()
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
