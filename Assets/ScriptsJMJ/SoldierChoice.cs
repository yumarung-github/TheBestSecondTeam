using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


    public class SoldierChoice : MonoBehaviour
    {

        public Button plusButton;
        public Button minusButton;
        public TextMeshProUGUI curSol;
        public TextMeshProUGUI maxSol;

        int maxSoldier;
        int curSoldier;

    int CurSoldier
    {
        get { return curSoldier; }
        set
        {
            curSoldier = value;
            if (curSoldier <= 0)
                curSoldier = 0;
            if (curSoldier > maxSoldier)
                curSoldier = maxSoldier;
            curSol.text = CurSoldier.ToString();

        }
    }

    private void Start()
        {
            curSoldier = RoundManager.Instance.mapController.soldierNum;
            maxSoldier =RoundManager.Instance.mapController.soldiers.Count;
            plusButton.onClick.AddListener(() => { CurSoldier++; });
            minusButton.onClick.AddListener(() => { CurSoldier--; });
            curSol.text = CurSoldier.ToString();
            maxSol.text = maxSoldier.ToString();
        }


    }


