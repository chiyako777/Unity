using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** ゲーム全体初期化
public class InitialManager : MonoBehaviour
{
    void Awake(){
        //** 全音源ロード
        SoundPlayer.LoadAllSounds();
        //Debug.Log("init 全音源ロード：" + init);

    }

    void Start()
    {
    }

    void Update()
    {
        
    }
}
