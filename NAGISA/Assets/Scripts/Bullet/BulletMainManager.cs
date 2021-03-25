using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 弾幕ステージ全体を統括する最上位マネージャー
public class BulletMainManager : MonoBehaviour
{
    [HideInInspector]
    public static ResourcesLoader<GameObject> resourcesLoader = new ResourcesLoader<GameObject>();
    [HideInInspector]
    public CommandManager commandManager = new CommandManager();
    [HideInInspector]
    public string loadFileName;
    [HideInInspector]
    public static string beforeScene;

    void Start()
    {
        resourcesLoader.LoadAllObjects("Prefabs/Resource_BulletHell");
        commandManager.Initialize();
        commandManager.LoadScript(loadFileName);
    }

    void Update()
    {
        commandManager.Run();
    }
}
