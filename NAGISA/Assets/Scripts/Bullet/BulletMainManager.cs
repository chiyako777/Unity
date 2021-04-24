using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 弾幕ステージ全体を統括する最上位マネージャー
public class BulletMainManager : MonoBehaviour
{
    private CommandManager commandManager = new CommandManager();
    [HideInInspector]
    public static ResourcesLoader<GameObject> resourcesLoader = new ResourcesLoader<GameObject>();
    [HideInInspector]
    public string loadFileName;
    [HideInInspector]
    public static string beforeScene;

    private bool initFlg = false;

    void Start()
    {
        resourcesLoader.LoadAllObjects("Bullet","loadBullet");
    }

    void Update()
    {
        if(!initFlg && FlagManager.flagDictionary.ContainsKey("loadBullet") && (bool)FlagManager.flagDictionary["loadBullet"]){
            Debug.Log("BulletMainManager : init");
            commandManager.Initialize();
            commandManager.LoadScript(loadFileName);
            initFlg = true;
        }

        commandManager.Run();
    }
}
