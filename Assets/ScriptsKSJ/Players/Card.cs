using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour,IPointerDownHandler, IPointerEnterHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Uimanager.Instance.cardName.text = "카드 이름 : " + skill.SkillName;
        Uimanager.Instance.cardInfo.text = "카드 정보 \n" + skill.SkillInfo;
    }
    public Skill skill;
    void Start()
    {

    }

    void Update()
    {
        
    }
}
