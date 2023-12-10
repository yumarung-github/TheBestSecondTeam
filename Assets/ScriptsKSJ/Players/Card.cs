using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour,IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Skill skill;
    public void OnPointerDown(PointerEventData eventData)
    {
        //스킬 사용하는 곳 
        //임시로 디버깅로그로 정보만 띄움
        RoundManager.Instance.nowPlayer.selectedCard = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Uimanager.Instance.cardWindow.SetActive(true);
        Uimanager.Instance.cardName.text = "카드 이름 : " + skill.SkillName;
        Uimanager.Instance.cardInfo.text = "카드 정보 \n" + skill.SkillInfo;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        Uimanager.Instance.cardWindow.SetActive(false);
    }
}
