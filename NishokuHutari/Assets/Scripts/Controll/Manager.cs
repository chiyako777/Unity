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
    public static int nowTransitionId;      //現在のマップに遷移してきたときのトランザクションID

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

            //** EventManager生成
            EventManager eventManager = Instantiate(gameObjectLoader.GetObjectHandle("Event_Manager"),new Vector3(0.0f,0.0f,0.0f),Quaternion.identity).GetComponent<EventManager>();
            eventManager.BindObject();

            //** 初期マップ読み込み
            EventQueue ev = new EventQueue("Trans",0,20);
            //EventQueue ev = new EventQueue("Trans",18,20);
            if(EventManager.IsAdd(ev.level)){
                EventManager.AddEvent(ev);
            }

            //** オープニングムービー再生
            EventQueue opening = new EventQueue("Story",1,21);
            if(EventManager.IsAdd(opening.level)){
                EventManager.AddEvent(opening);
            }

            initFlg = true;
        }

        //** 他シーンからの復帰時
        if(returnFlg && !(bool)FlagManager.flagDictionary["coroutine"]){
            Debug.Log("Manager:マップ復帰時処理開始:nowTransitionId = " + nowTransitionId);
            //Debug.Log("EventManager = " + GameObject.Find("Event_Manager(Clone)"));
            EventManager eventManager = GameObject.FindWithTag("EventManager").GetComponent<EventManager>();
            eventManager.BindObject();
            EventQueue ev = new EventQueue("TransReturn",nowTransitionId,20);
            if(EventManager.IsAdd(ev.level)){
                EventManager.AddEvent(ev);
            }
            returnFlg = false;
        }
    }
}
