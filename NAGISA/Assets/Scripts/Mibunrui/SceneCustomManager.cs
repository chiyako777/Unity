﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCustomManager : MonoBehaviour
{
    [HideInInspector]
    public static ResourcesLoader<GameObject> resourcesLoader = new ResourcesLoader<GameObject>();

    void Start()
    {
        //** リソースロード
        //resourcesLoader.LoadAllObjects("Prefabs/Resources_Other");
        //** フラグ初期化
        FlagManager.flagDictionary["coroutine"] = false;   
    }

    void Update()
    {
        
    }
}
