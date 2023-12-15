using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCardInventory : MonoBehaviour
{
    public BirdCardSlot[] birdCardSlot;
    public int curSlot;

    public void Start()
    {
        birdCardSlot = Uimanager.Instance.birdUI.birdSlot;
    }
    public void UseSlot()
    {
        Debug.Log("유즈슬롯 들어옴");
        for(int i = 0; i < birdCardSlot.Length -1; i++) 
        {
            if (birdCardSlot[i].birdCard != null)
            {
                curSlot = i;
                Debug.Log(curSlot);
                birdCardSlot[i].Use();
            }
        }
    }

    public void CheckAddMaxCard() //카드가 몇장들어왔는지 확인 하려는 함수
    {

    }
}