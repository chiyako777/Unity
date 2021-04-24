using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCustomManager : MonoBehaviour
{
    [HideInInspector]
    public static ResourcesLoader<GameObject> gameObjectLoader = new ResourcesLoader<GameObject>();
    [HideInInspector]
    public static ResourcesLoader<Sprite> spriteLoader = new ResourcesLoader<Sprite>();

    void Start()
    {
        //** リソースロード
        gameObjectLoader.LoadAllObjects("Other","loadSceneObject");
        spriteLoader.LoadAllObjects("Sprite","loadSceneSprite");
        //** フラグ初期化
        FlagManager.flagDictionary["coroutine"] = false;   

    }

    void Update()
    {
        
    }

}
