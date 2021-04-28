using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FlagManager
{
    public static Dictionary<string,object> flagDictionary = new Dictionary<string,object>();

    //** 各フラグ初期化
    //flagDictionary["coroutine"] = false;

    //** memo:Flg種類

    //coroutine
    
    //loadAudio:音源データの初期読み込み完了フラグ(bool)
    //loadAudioPlayer:音源プレイヤーの初期読み込み完了フラグ(bool)

    //loadMap:マップデータの初期読み込み完了フラグ(bool)
    //loadSprite:スプライトデータの初期読み込み完了フラグ(bool)
}