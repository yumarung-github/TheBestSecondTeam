using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCardInventory : MonoBehaviour
{
    public BirdCardSlot[] birdCardSlot;
    public void UseSlot()
    {
        for(int i = 0; i < birdCardSlot.Length -1; i++) 
        {
            if (birdCardSlot[i].birdCard != null)
                birdCardSlot[i].Use();
            else
                return;

        }
    }
}
