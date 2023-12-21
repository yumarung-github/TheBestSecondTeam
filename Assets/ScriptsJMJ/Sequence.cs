using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Sequence : MonoBehaviour
{

    public Image[] pointImage;

    Color color;
    Color originColor;

    bool isCheck;

    private void Start()
    {
        isCheck = RoundManager.Instance.nowPlayer == RoundManager.Instance.bird;
        originColor = new Color(1f,1f,1f,1f);
        color = new Color(1f, 1f, 1f,0f);

    }
    
    public IEnumerator PointCo(int i)
    {
        while(isCheck) 
        {
            pointImage[i].color = originColor;
            yield return new WaitForSeconds(1);
            pointImage[i].color = color;
            yield return new WaitForSeconds(1);
        }
    }
    
}
