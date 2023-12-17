using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Sequence : MonoBehaviour
{

    public Image[] pointImage;

    Color color;
    Color orijinColor;

    bool isCheck;

    private void Start()
    {
        isCheck = RoundManager.Instance.nowPlayer == RoundManager.Instance.bird;
        orijinColor = new Color(1f,1f,1f,1f);
        color = new Color(1f, 1f, 1f,0f);

    }
    
    public IEnumerator PointCo(int i)
    {
        while(isCheck) 
        {
            pointImage[i].color = orijinColor;
            yield return new WaitForSeconds(1);
            pointImage[i].color = color;
            yield return new WaitForSeconds(1);
        }
    }
    
}
