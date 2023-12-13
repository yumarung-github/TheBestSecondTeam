using CustomInterface;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cat : Player
{
    public Dictionary<string, int> deadSoldierNum = new Dictionary<string, int>();
    IEnumerator flashCo;
    Color originColor1;
    Color originColor2;
    Color originColor3;
    Color originColor4;

    private new void Start()
    {
        base.Start();
        //Debug.Log(animator.GetLayerIndex("Idle"));
        isOver = false;
        roundManager.cat = this;
        roundManager.nowPlayer = this;
        hasNodeNames.Add("생쥐3");
        ColorSetting();
        flashCo = FlashCoroutine();
    }

    private void ColorSetting()
    {
        originColor1 = RoundManager.Instance.mapExtra.mapTiles[0].transform.GetComponent<Renderer>().material.color;
        originColor2 = RoundManager.Instance.mapExtra.mapTiles[2].transform.GetComponent<Renderer>().material.color;
        originColor3 = RoundManager.Instance.mapExtra.mapTiles[8].transform.GetComponent<Renderer>().material.color;
        originColor4 = RoundManager.Instance.mapExtra.mapTiles[11].transform.GetComponent<Renderer>().material.color;
    }
    public void FlashTile()
    {
        StartCoroutine(flashCo);
    }
    IEnumerator FlashCoroutine()
    {
        while (true)
        {
            Debug.Log("TEST");
            RoundManager.Instance.mapExtra.mapTiles[0].transform.GetComponent<Renderer>().material.color = Color.green;
            RoundManager.Instance.mapExtra.mapTiles[2].transform.GetComponent<Renderer>().material.color = Color.green;
            RoundManager.Instance.mapExtra.mapTiles[8].transform.GetComponent<Renderer>().material.color = Color.green;
            RoundManager.Instance.mapExtra.mapTiles[11].transform.GetComponent<Renderer>().material.color = Color.green;
            yield return new WaitForSeconds(1f);
            RoundManager.Instance.mapExtra.mapTiles[0].transform.GetComponent<Renderer>().material.color = originColor1;
            RoundManager.Instance.mapExtra.mapTiles[2].transform.GetComponent<Renderer>().material.color = originColor2;
            RoundManager.Instance.mapExtra.mapTiles[8].transform.GetComponent<Renderer>().material.color = originColor3;
            RoundManager.Instance.mapExtra.mapTiles[11].transform.GetComponent<Renderer>().material.color = originColor4;
            yield return new WaitForSeconds(1f);
            yield return null;
        }
    }
}