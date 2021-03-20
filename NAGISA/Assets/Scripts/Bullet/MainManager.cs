﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 弾幕ステージ全体を統括する最上位マネージャー
public class MainManager : MonoBehaviour
{
    [HideInInspector]
    public static ResourcesLoader<GameObject> resourcesLoader = new ResourcesLoader<GameObject>();
    [HideInInspector]
    public CommandManager commandManager = new CommandManager();
    [HideInInspector]
    public string loadFileName;

    void Start()
    {
        resourcesLoader.LoadAllObjects("Prefabs");
        commandManager.Initialize();
        //loadFileName = Application.persistentDataPath + "/bullet/" + "Room_Test.txta";
        //loadFileName = "dummy";
        commandManager.LoadScript(loadFileName);
    }

    void Update()
    {
        commandManager.Run();
    }
}
