using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CARDUSE_TYPE
{
    MOVE,
    SPAWN,
    BATTLE,
    BULID
}

public class BirdCardSlot : MonoBehaviour
{
    public List<Card> birdCard;
    public CARDUSE_TYPE cardUse_type;
    public GameObject moveSoldierOBJ;

    public int curCaed = 0;
    private void Start()
    {
      

    }

    public void Use()
    {
        

        switch (cardUse_type)
        {
            case CARDUSE_TYPE.MOVE:
                {
                    
                    RoundManager.Instance.testType = RoundManager.SoldierTestType.Move;
                        
                }
                break;
            case CARDUSE_TYPE.SPAWN:
                break;
            case CARDUSE_TYPE.BATTLE:
                break;
            case CARDUSE_TYPE.BULID:
                break;

        }

    }
}
