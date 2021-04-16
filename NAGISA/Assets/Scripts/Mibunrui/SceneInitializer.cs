using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    void Start()
    {
        //** フラグ初期化
        FlagManager.flagDictionary["coroutine"] = false;        
    }

    void Update()
    {
        
    }
}
