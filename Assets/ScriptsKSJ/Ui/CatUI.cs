using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatUI : MonoBehaviour
{
    public Button emotionBtn;
    [Header("[프로필]")]
    public GameObject profileWindow;
    public GameObject profile;
    public GameObject sawmillImage;
    public GameObject workshopImage;
    public GameObject barracksImage;

    public GameObject woodProductImage;
    public GameObject remainSolImage;

    [Header("[값]")]
    public TextMeshProUGUI remainSolText;
    public TextMeshProUGUI craftCardText;
    public TextMeshProUGUI actionNumText;
}
