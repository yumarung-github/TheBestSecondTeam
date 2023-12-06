using CustomInterface;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cat : Player
{
    
    private new void Start()
    {
        base.Start();
        //Debug.Log(animator.GetLayerIndex("Idle"));
        isOver = false;
        roundManager.cat = this;
        roundManager.nowPlayer = this;
        hasNodeNames.Add("생쥐3");
    }

}
