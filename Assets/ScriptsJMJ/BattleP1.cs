using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleP1 : MonoBehaviour
{
    public BattleManager battleManager;
    public Image[] dices;
    public Image[] soldirs;
    public TextMeshProUGUI playerName;

    public void ActionP1()
    {
        playerName.text = battleManager.battleP1.name;
        for (int i = 0; i < battleManager.battleP1Soldiers.Count; i++)
        {
            soldirs[i].gameObject.SetActive(true);
        }
        StartCoroutine(RandomDice());
    }

    IEnumerator RandomDice()
    {
        yield return new WaitForSeconds(3);
        switch (battleManager.diceP2Num)
        {

            case 0:
                dices[0].gameObject.SetActive(true);
                break;

            case 1:
                dices[1].gameObject.SetActive(true);
                break;

            case 2:
                dices[2].gameObject.SetActive(true);
                break;

            case 3:
                dices[3].gameObject.SetActive(true);
                break;

        }

        yield return new WaitForSeconds(3);

        int dieSoldir = battleManager.battleP1Soldiers.Count - battleManager.diceP1Num;
        for (int i = battleManager.battleP1Soldiers.Count; i == dieSoldir; i--)
        {
            soldirs[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < dices.Length; i++)
        {
            dices[i].gameObject.SetActive(false);
        }
    }

}
