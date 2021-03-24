using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class SpellPracController : MonoBehaviour
{


    private Canvas typeSelectWindow;
    private Canvas startConfirmWindow;
    private Text stageSelectText;
    private Text typeListText;
    private Text typeDescText;
    private Text confirmText;

    private RoomData[] roomData;
    private IEnumerator spellSelectCoroutine;
    private IEnumerator typeSelectCoroutine;
    private IEnumerator confirmCoroutine;

    int roomSelected;   //1start
    int spellSelected;  //1start
    int typeSelected;  //1start

    private List<string> typeList;
    private List<string> typeDescList;

    void Start()
    {
        //** コンポーネント取得
        Text[] text = GameObject.FindObjectsOfType<Text>();
        foreach(Text t in text){
            if(t.name == "stageSelect_text"){
                stageSelectText = t;
            }
        }
        Canvas[] canvas = GameObject.FindObjectsOfType<Canvas>();
        foreach(Canvas c in canvas){
            if(c.name == "typeSelect_window"){
                typeSelectWindow = c;
            }
            if(c.name == "practiceStartConfirm_window"){
                startConfirmWindow = c;
            }
        }

        //** ルームデータ読み込み
        string f = Application.dataPath + "/StaticData" + "/data/" + "room.json";
        roomData = JsonHelper.FromJson<RoomData>(File.ReadAllText(f));

        //** 初期化
        roomSelected = 1;
        spellSelected = 1;
        typeSelected = 1;
        roomDisp();
        spellSelectCoroutine = null;
        typeSelectCoroutine = null;
        confirmCoroutine = null;
        typeSelectWindow.gameObject.SetActive(false);
        startConfirmWindow.gameObject.SetActive(false);
        
        typeList = new List<string>(){"ホーミング","リフレク","ワープ"};
        typeDescList = new List<string>(){"敵を常に追尾して攻撃してくれるオプションがつきます。ホーミングショットのパワーは控えめ。回避に専念できるので万人向け",
                                            "使用中動けなくなる代わりに、結界を張って敵弾を跳ね返せます。結界は４方向に移動可能。うまく使えばボム節約できます。再使用にはチャージが必要",
                                            "画面の反対側の端へワープできます。変なパターンを打ってくる敵はこれで対抗すると良いかも。"};
    }

    void Update()
    {
        //** ルーム選択
        if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")>0.0f 
                && spellSelectCoroutine == null && typeSelectCoroutine == null && confirmCoroutine == null){
            stageSelectText.text = stageSelectText.text.Replace("*"," ");
            roomSelected = (int)Mathf.Clamp(roomSelected - 1,1.0f,(float)roomData.Length);
            stageSelectText.text = stageSelectText.text.Replace("    " + roomData[roomSelected-1].name,"   *" + roomData[roomSelected-1].name);
        }
        if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")<0.0f 
                && spellSelectCoroutine == null && typeSelectCoroutine == null && confirmCoroutine == null){
            stageSelectText.text = stageSelectText.text.Replace("*"," ");
            roomSelected = (int)Mathf.Clamp(roomSelected + 1,1.0f,(float)roomData.Length);
            stageSelectText.text = stageSelectText.text.Replace("    " + roomData[roomSelected-1].name,"   *" + roomData[roomSelected-1].name);
        }
        //** 確定
        if(Input.GetButtonDown("Submit") 
                && spellSelectCoroutine == null && typeSelectCoroutine == null && confirmCoroutine == null){
            spellSelectCoroutine = CreateSpellCoroutine();
            StartCoroutine(spellSelectCoroutine);
        }
        //** タイトルへ戻る
        if(Input.GetKeyDown(KeyCode.X) 
                && spellSelectCoroutine == null && typeSelectCoroutine == null && confirmCoroutine == null){
            SceneManager.LoadScene("Title");
        }
    }

    //** スペル選択コルーチン生成
    private IEnumerator CreateSpellCoroutine(){

        yield return OnSpellSelect();

        StopCoroutine(spellSelectCoroutine);
        spellSelectCoroutine = null;
        //ルーム選択に戻す
        roomDisp();
        spellSelected = 1;

    }

    private IEnumerator OnSpellSelect(){
        //** 初期表示
        spellDisp();
        yield return null;

        //** スペル選択ループ
        bool finishFlg = true;  //↓のwhile文を抜けて良いかどうか（項目詳細から戻ってきたときに再度他メニューを選択可能にするための制御）
        while(!finishFlg || !Input.GetKeyDown(KeyCode.X)){
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")>0.0f 
                    && typeSelectCoroutine == null && confirmCoroutine == null){
                stageSelectText.text = stageSelectText.text.Replace("*"," ");
                spellSelected = (int)Mathf.Clamp(spellSelected - 1,1.0f,(float)roomData[roomSelected-1].spell.Count);
                stageSelectText.text = stageSelectText.text.Replace(
                                            "    " + roomData[roomSelected-1].spell[spellSelected-1],
                                            "   *" + roomData[roomSelected-1].spell[spellSelected-1]);
            }
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")<0.0f 
                    && typeSelectCoroutine == null && confirmCoroutine == null){
                stageSelectText.text = stageSelectText.text.Replace("*"," ");
                spellSelected = (int)Mathf.Clamp(spellSelected + 1,1.0f,(float)roomData[roomSelected-1].spell.Count);
                stageSelectText.text = stageSelectText.text.Replace(
                                            "    " + roomData[roomSelected-1].spell[spellSelected-1],
                                            "   *" + roomData[roomSelected-1].spell[spellSelected-1]);
            }
            if(Input.GetButtonDown("Submit") && typeSelectCoroutine == null && confirmCoroutine == null){
                typeSelectCoroutine = CreateTypeCoroutine();
                StartCoroutine(typeSelectCoroutine);
                finishFlg = false;
            }

            if(!finishFlg && typeSelectCoroutine == null && confirmCoroutine == null){ finishFlg = true;}//次の階層のコルーチンから復帰したらフラグ戻す

            yield return null;
        }

        yield return new WaitUntil( () => Input.GetKeyDown(KeyCode.X));
    }

    //** 自機タイプ選択コルーチン
    private IEnumerator CreateTypeCoroutine(){

        //** 自機タイプ選択画面起動
        typeSelectWindow.gameObject.SetActive(true);

        yield return OnTypeSelect();

        StopCoroutine(typeSelectCoroutine);
        typeSelectCoroutine = null;

        //** 自機タイプ選択画面終了
        typeSelectWindow.gameObject.SetActive(false);

        //スペル選択に戻す
        spellDisp();
        typeSelected = 1;

    }

    private IEnumerator OnTypeSelect(){

        //** 自機タイプ選択UIコンポーネント取得
        Text[] text = GameObject.FindObjectsOfType<Text>();
        foreach(Text t in text){
            if(t.name == "typeList_text"){
                typeListText = t;
            }
            if(t.name == "typedesc_text"){
                typeDescText = t;
            }
        }

        //** 初期表示
        typeDisp();
        yield return null;

        //** 自機タイプ選択ループ
        bool finishFlg = true;  //↓のwhile文を抜けて良いかどうか（項目詳細から戻ってきたときに再度他メニューを選択可能にするための制御)
        bool confirmFlg = true; //確定可能フラグ（スタート確認画面で「いいえ」選択<submit>で戻ってきたとき、すぐまた確認画面に入ってしまうのを回避するための制御）
        while(!finishFlg || !Input.GetKeyDown(KeyCode.X)){
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")>0.0f && confirmCoroutine == null){
                typeListText.text = typeListText.text.Replace("*"," ");
                typeSelected = (int)Mathf.Clamp(typeSelected - 1,1.0f,(float)typeList.Count);
                typeListText.text = typeListText.text.Replace("    " + typeList[typeSelected-1],"   *" + typeList[typeSelected-1]);
                typeDescText.text = typeDescList[typeSelected-1];
            }
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")<0.0f && confirmCoroutine == null){
                typeListText.text = typeListText.text.Replace("*"," ");
                typeSelected = (int)Mathf.Clamp(typeSelected + 1,1.0f,(float)typeList.Count);
                typeListText.text = typeListText.text.Replace("    " + typeList[typeSelected-1],"   *" + typeList[typeSelected-1]);
                typeDescText.text = typeDescList[typeSelected-1];
            }
            //** 確定
            if(confirmFlg && Input.GetButtonDown("Submit") && confirmCoroutine == null){
                //** 最終確認へ
                confirmCoroutine = CreateConfirmCoroutine();
                StartCoroutine(confirmCoroutine);
                finishFlg = false;
                confirmFlg = false;
            }
            if(!finishFlg && !confirmFlg && confirmCoroutine == null){ finishFlg = true; confirmFlg = true;}//次の階層のコルーチンから復帰したらフラグ戻す
            yield return null;
        }

        yield return new WaitUntil( () => Input.GetKeyDown(KeyCode.X));
    }

    //** 最終確認コルーチン作成
    private IEnumerator CreateConfirmCoroutine(){
        
        //** 確認画面起動
        startConfirmWindow.gameObject.SetActive(true);

        yield return OnConfirm();

        //Debug.Log("OnConfirm抜けた");
        StopCoroutine(confirmCoroutine);
        confirmCoroutine = null;

        //** 確認画面終了
        startConfirmWindow.gameObject.SetActive(false);

        //タイプ選択に戻す
        typeDisp();

    }

    private IEnumerator OnConfirm(){

        //** コンポーネント取得
        Text[] text = GameObject.FindObjectsOfType<Text>();
        foreach(Text t in text){
            if(t.name == "practiceStartConfirm_text"){
                confirmText = t;
            }
        }

        //** 初期表示
        bool isYes = true;
        confirmDisp(isYes);
        yield return null;

        //** スタート確認
        while(!Input.GetKeyDown(KeyCode.X)){
            if(Input.GetButtonDown("Horizontal")){
                isYes = (!isYes) ? true : false;
                confirmDisp(isYes);
            }
            if(Input.GetButtonDown("Submit") && isYes){
                //** スタート
                //★★★★★★★★★★★★★★★
                //Debug.Log("Bullet Sceneへ");
                SceneManager.sceneLoaded += OnBulletLoaded;
                SceneManager.LoadScene("Bullet");

            }else if(Input.GetButtonDown("Submit") && !isYes){
                yield break;
            }
            yield return null;
        }
        
        yield return new WaitUntil( () => Input.GetKeyDown(KeyCode.X));
    }

    //** ルーム選択の表示
    private void roomDisp(){
        stageSelectText.text = "ルーム選択：\n";
        for(int i=0; i<roomData.Length; i++){
            if(i == roomSelected-1){
                stageSelectText.text += "   *" + roomData[i].name + "\n";
            }else{
                stageSelectText.text += "    " + roomData[i].name + "\n";
            }
        }
    }

    //** スペル選択の表示
    private void spellDisp(){
        stageSelectText.text = "スペル選択：\n";
        for(int i=0; i<roomData[roomSelected-1].spell.Count; i++){            
            if(i == spellSelected-1){
                stageSelectText.text += "   *" + roomData[roomSelected-1].spell[i] + "\n";
            }else{
                stageSelectText.text += "    " + roomData[roomSelected-1].spell[i] + "\n";
            }
        }
    }

    //** 自機タイプ選択の表示
    private void typeDisp(){
        typeListText.text = "自機タイプ選択：\n";
        for(int i=0; i<typeList.Count; i++){
            if(i == typeSelected-1){
                typeListText.text += "   *" + typeList[i] + "\n";
                typeDescText.text = typeDescList[i];
            }else{
                typeListText.text += "    " + typeList[i] + "\n";
            }
        }
    }

    //** 確認画面の表示
    private void confirmDisp(bool isYes){
        confirmText.text = "";
        confirmText.text += roomData[roomSelected-1].name + "\n";
        confirmText.text += roomData[roomSelected-1].spell[spellSelected-1] + "\n";
        confirmText.text += typeList[typeSelected-1] + "\n";
        confirmText.text += "はじめますか？" + "\n";

        if(isYes){
            confirmText.text += "    *はい    いいえ";
        }else{
            confirmText.text += "     はい   *いいえ";
        }
    }

    //** Bulletシーン遷移前に呼ばれる
    private void OnBulletLoaded(Scene nextScene,LoadSceneMode mode){
        //Debug.Log("OnSceneLoaded:scene.name =  " + nextScene.name);
        //Debug.Log("OnSceneLoaded:mode =  " + mode);

        //MainManagerに、ロードするデータファイル名、シーン名を渡す
        GameObject mainController = GameObject.Find("bullet_main_controller");
        var mainManager = mainController.GetComponent<MainManager>();
        mainManager.loadFileName = Application.dataPath + "/StaticData" + "/bullet/" + "Room_Test.txt";
        MainManager.beforeScene = "SpellPracticeSetting";

        //自機タイプ
        string type = "";
        switch(typeSelected){
            case 1:
                type = "Homing";
                break;
            case 2:
                type = "Reflec";
                break;
            case 3:
                //warp:未実装
                type = "Warp";
                break;
            default:
                break;
        }

        //PlayerControllerに、自機タイプ、残機数、ボム数を渡す
        GameObject player = GameObject.Find("bullet_player");
        var playerController = player.GetComponent<PlayerController>();
        playerController.optionType = type;
        playerController.life = 5;      //※残機・ボム⇒暫定で決め打ち いずれは選べるようにする
        playerController.bomb = 5;
        if(typeSelected == 2){
            playerController.power = 2.5f;
        }else{
            playerController.power = 1.0f;
        }

        //InfoControllerに、自機タイプ、ルーム名を渡す
        GameObject bulletInfo = GameObject.Find("bulletinfo_controller");
        var infoController = bulletInfo.GetComponent<InfoController>();
        infoController.option = type;
        infoController.room = roomData[roomSelected-1].name;

        SceneManager.sceneLoaded -= OnBulletLoaded;
    }

}
