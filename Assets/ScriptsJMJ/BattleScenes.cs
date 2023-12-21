using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScenes : MonoBehaviour
{
    public GameObject player;

    public Transform battlePos;

    private void Update()
    {

        transform.Translate(battlePos.transform.position * Time.deltaTime,Space.World);
    }
}
