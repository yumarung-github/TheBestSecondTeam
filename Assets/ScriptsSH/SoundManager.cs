using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingleTon<SoundManager>
{
    List<AudioClip> bgmList;
    List<AudioClip> effectSoundList;
    GameObject soundPlayer;


    [Header("bgm목록들")]
    public AudioClip bgm1;
    public AudioClip bgm2;
    public AudioClip bgm3;

    

    [Header("이펙트사운드")]
    public AudioClip btnSound;
    public AudioClip battleSound;
    public AudioClip buildiSound;
    public AudioClip moveSound;
    public AudioClip turnEndSound;
    public AudioClip clickSound;





    private void Start()
    {
        bgmList = new List<AudioClip>();
        bgmList.Add(bgm1);
        bgmList.Add(bgm2);
        bgmList.Add(bgm3);


        effectSoundList = new List<AudioClip>();
        effectSoundList.Add(btnSound);
        effectSoundList.Add(battleSound);
        effectSoundList.Add(buildiSound);
        effectSoundList.Add(moveSound);
        effectSoundList.Add(turnEndSound);
        effectSoundList.Add(clickSound);
    }

}
