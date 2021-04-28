using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** Mapシーンの全体制御
public class Manager : MonoBehaviour
{

    [HideInInspector]
    public static ResourcesLoader<GameObject> gameObjectLoader = new ResourcesLoader<GameObject>();
    [HideInInspector]
    public static ResourcesLoader<Sprite> spriteLoader = new ResourcesLoader<Sprite>();
    [HideInInspector]
    public static GeneralData generalData;
    [HideInInspector]
    public static UserData userData;

    public static bool initFlg = false;
    public static bool returnFlg = false;
    public static int nowTransitionId;

    void Awake(){
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        //** データ初期ロード
        gameObjectLoader.LoadAllObjects("Map","loadMap");
        spriteLoader.LoadAllObjects("Sprite","loadSprite");

        //** フラグ初期化
        FlagManager.flagDictionary["coroutine"] = false;
    }

    void Update()
    {
        //** 初期化
        if(!initFlg 
            && FlagManager.flagDictionary.ContainsKey("loadMap") 
            && (bool)FlagManager.flagDictionary["loadMap"]
            && FlagManager.flagDictionary.ContainsKey("loadSprite")
            && (bool)FlagManager.flagDictionary["loadSprite"]){
            Debug.Log("Map初期化");

            //** Mapデータ設定
            generalData = new GeneralData();

            //** Userデータ設定
            userData = new UserData();

            //** 初期マップ読み込み
            EventQueue ev = new EventQueue("Trans",9999,20);
            if(EventManager.IsAdd(ev.level)){
                EventManager.AddEvent(ev);
            }

            initFlg = true;
        }

        //** 他シーンからの復帰時
        if(returnFlg && !(bool)FlagManager.flagDictionary["coroutine"]){
            //Debug.Log("Manager:マップ復帰時処理開始");
            nowTransitionId = 9999;     //test
            EventQueue ev = new EventQueue("TransReturn",nowTransitionId,20);
            if(EventManager.IsAdd(ev.level)){
                EventManager.AddEvent(ev);
            }
            returnFlg = false;
        }
    }
}
