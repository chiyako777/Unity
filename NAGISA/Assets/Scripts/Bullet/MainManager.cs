using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 弾幕ステージ全体を統括する最上位マネージャー
public class MainManager : MonoBehaviour
{
    public static ResourcesLoader<GameObject> resourcesLoader = new ResourcesLoader<GameObject>();
    public CommandManager commandManager = new CommandManager();

    void Start()
    {
        resourcesLoader.LoadAllObjects("Prefabs");
        commandManager.Initialize();
        commandManager.LoadScript("temp");
    }

    void Update()
    {
        commandManager.Run();
    }
}
