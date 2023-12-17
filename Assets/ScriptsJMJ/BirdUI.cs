using CustomInterface;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        nextButton.onClick.AddListener(() => { RoundManager.Instance.roundSM.SetState(MASTATE_TYPE.BIRD_MORNING); });
        nextButton.onClick.AddListener(() => { birdCardBox.SetActive(false); });
    }
}