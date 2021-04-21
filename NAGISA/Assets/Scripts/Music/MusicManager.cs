﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    [SerializeField]
    private string BGM;

    void Start()
    {
        //** シーンBGM
        if(!SoundPlayer.IsPlayingBGM(BGM)) {
            //Debug.Log("Music Manager : " + BGM);
            
            //前のBGMが再生中だったら止める、オブジェクト破棄
            SoundPlayer.StopBGM();
            GameObject[] oldPlayer = GameObject.FindGameObjectsWithTag("BGMPlayer");
            for(int i=0; i<oldPlayer.Length; i++){
                Destroy(oldPlayer[i]);
            }

            //再生
            SoundPlayer.PlayBGM(BGM,0.25f,true);

            //シーン遷移してもプレイヤーを破棄しないようにする
            DontDestroyOnLoad(SoundPlayer.BGMPlayer);       
        }
    }

    void Update()
    {
        ClearSE();
    }

    //** 再生終わったSEを消す
    private static void ClearSE(){
        GameObject[] oldPlayer = GameObject.FindGameObjectsWithTag("SEPlayer");
        for(int i=0; i<oldPlayer.Length; i++){
            if(!oldPlayer[i].GetComponent<AudioSource>().isPlaying){
                Destroy(oldPlayer[i]);
            }
        }
    }

    //** 選択音
    public static void PlaySelectSE(){
        SoundPlayer.PlaySE("se_maoudamashii_system21");
    }

}
