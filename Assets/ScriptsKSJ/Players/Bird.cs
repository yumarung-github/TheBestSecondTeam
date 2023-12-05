using CustomInterface;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bird : Player
{
    private new void Start()
    {
        base.Start();
        roundManager.bird = this;
        hasNodeNames.Add("»ýÁã1");
    }

}
