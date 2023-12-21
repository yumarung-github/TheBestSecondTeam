using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public BattleManager battleManager;

    private Player player;
    public Image[] dice;
    public Image[] soldirs;
    public TextMeshProUGUI playerName;

    private void Start()
    {
        player = battleManager.battleP1;

    }

    public void ActiveBattleCanvas()
    {

    }

    IEnumerator RandomDice()
    {
        yield return new WaitForSeconds(3);

    }
}
