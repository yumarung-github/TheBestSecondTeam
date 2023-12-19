using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCardInventory : MonoBehaviour
{
    public BirdCardAction[] birdCardSlot;
    public int curSlot;
    bool firstSlotCheck;

    public void Start()
    {
        birdCardSlot = Uimanager.Instance.birdUI.birdSlot;
        this.transform.parent.gameObject.SetActive(false);
        firstSlotCheck = false;
    }

    public void UseSlot()
    {
        Debug.Log("유즈슬롯 들어옴");
        for (int i = 0; i <= birdCardSlot.Length - 1; i++)
        {
/*            if (birdCardSlot[i].birdCard == null)
                return;*/
             if (birdCardSlot[i].birdCards != null)
            {
                //birdCardSlot[i].Use(); //없앤거
                if (!firstSlotCheck)
                {
                    firstSlotCheck = true;
                    birdCardSlot[i].isOver[0] = true;
                }
                curSlot = i;
                birdCardSlot[i].StartActionCo();
            }
        }
    }

    public void CheckAddMaxCard() //카드가 몇장들어왔는지 확인 하려는 함수
    {

    }
}