using CustomInterface;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wood : Player
{
    private new void Start()
    {
        base.Start();
        roundManager.wood = this;
        hasNodeNames.Add("¿©¿ì1");
    }
}
