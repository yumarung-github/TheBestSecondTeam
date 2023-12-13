using CustomInterface;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cat : Player
{
    public Dictionary<string, int> deadSoldierNum = new Dictionary<string, int>();//죽은 병사들 명수
    //맵컨트롤러에 야전병원 select라는거 추가해서 선택한 노드의 name받아서 딕셔너리에서 찾으면
    //명수 찾을 수 있게
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
