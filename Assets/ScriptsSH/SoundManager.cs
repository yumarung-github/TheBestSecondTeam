using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingleTon<SoundManager>
{
    List<AudioClip> bgmList;
    GameObject soundPlayer;


    [Header("bgm목록들")]
    public AudioClip bgm1;
    public AudioClip bgm2;
    public AudioClip bgm3;

    [Header("버튼 사운드")]
    public AudioClip btnSound;

    [Header("")]
    public AudioClip battleSound;
    public AudioClip buildiSound;
    public AudioClip moveSound;
    public AudioClip turnEndSound;
    public AudioClip clickSound;





    private void Start()
    {
        bgmList = new List<AudioClip>();
        bgmList.Add(btnSound);
        bgmList.Add(battleSound);
        bgmList.Add(buildiSound); 
        bgmList.Add(moveSound);
        bgmList.Add(turnEndSound);
        bgmList.Add(clickSound);
    }

}
