using CustomInterface;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BirdUI : MonoBehaviour
{
    public BirdCardInventory BirdInventory;
    public BirdCardAction[] birdSlot;
    public Sequence sequence;

    public GameObject birdLeaderSelect;
    public GameObject birdCardBox;
    public GameObject SequenceBox;

    public Button nextButton;
    public TextMeshProUGUI soldierCount;

    private int hasSoldierCount;
    public int testint;
    private void Start()
    {
        nextButton.gameObject.SetActive(true);
        nextButton.onClick.AddListener(() => { RoundManager.Instance.roundSM.SetState(MASTATE_TYPE.BIRD_MORNING2); });
        nextButton.onClick.AddListener(() => { nextButton.gameObject.SetActive(false); });
        nextButton.onClick.AddListener(() => { birdCardBox.SetActive(false); });
        hasSoldierCount = 20 - RoundManager.Instance.bird.hasSoldierDic.Count;
        soldierCount.text = RoundManager.Instance.bird.hasSoldierDic.Count.ToString();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            foreach(KeyValuePair<string,List<Soldier>> asd in RoundManager.Instance.bird.hasSoldierDic)
            {
                testint += asd.Value.Count;
            }
            Debug.Log("testint"+testint);
        }
    }
}