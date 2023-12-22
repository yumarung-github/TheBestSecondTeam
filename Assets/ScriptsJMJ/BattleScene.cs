using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleScene : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetKey(KeyCode.O))
        {
            transform.position += (Vector3.forward * Time.deltaTime);
        }
    }
    public void PlayerAction()
    {
        transform.position += (Vector3.forward * 1.5f);
    }
}
