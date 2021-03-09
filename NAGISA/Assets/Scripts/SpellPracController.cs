using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SpellPracController : MonoBehaviour
{

    private Text stageSelectText;
    private Canvas typeSelectWindow;

    private RoomData[] roomData;
    private IEnumerator spellSelectCoroutine;
    private IEnumerator typeSelectCoroutine;
    int roomSelected;   //1start
    int spellSelected;  //1start

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
        }

        //** ルームデータ読み込み
        string f = Application.persistentDataPath + "/data/" + "room.json";
        roomData = JsonHelper.FromJson<RoomData>(File.ReadAllText(f));

        //** 初期表示
        roomDisp();
        roomSelected = 1;
        spellSelected = 1;
        spellSelectCoroutine = null;
        typeSelectCoroutine = null;
        typeSelectWindow.gameObject.SetActive(false);
    }

    void Update()
    {
        //** ルーム選択
        if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")>0.0f 
                && spellSelectCoroutine == null && typeSelectCoroutine == null){
            stageSelectText.text = stageSelectText.text.Replace("*"," ");
            roomSelected = (int)Mathf.Clamp(roomSelected - 1,1.0f,(float)roomData.Length);
            stageSelectText.text = stageSelectText.text.Replace("    " + roomData[roomSelected-1].name,"   *" + roomData[roomSelected-1].name);
        }
        if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")<0.0f 
                && spellSelectCoroutine == null && typeSelectCoroutine == null){
            stageSelectText.text = stageSelectText.text.Replace("*"," ");
            roomSelected = (int)Mathf.Clamp(roomSelected + 1,1.0f,(float)roomData.Length);
            stageSelectText.text = stageSelectText.text.Replace("    " + roomData[roomSelected-1].name,"   *" + roomData[roomSelected-1].name);
        }
        //** 確定
        if(Input.GetButtonDown("Submit") 
                && spellSelectCoroutine == null && typeSelectCoroutine == null){
            spellSelectCoroutine = CreateSpellCoroutine();
            StartCoroutine(spellSelectCoroutine);
        }        
    }

    //** スペル選択コルーチン生成
    private IEnumerator CreateSpellCoroutine(){

        yield return OnSpellSelect();

        //Debug.Log("spell stop前：" + spellSelectCoroutine);
        StopCoroutine(spellSelectCoroutine);
        //Debug.Log("spellSelectCoroutine stop comp");
        spellSelectCoroutine = null;
        //ルーム選択に戻す
        roomDisp();
        roomSelected = 1;
        spellSelected = 1;

    }

    private IEnumerator OnSpellSelect(){
        //** 初期表示
        spellDisp();
        yield return null;

        //** スペル選択ループ
        bool finishFlg = true;  //↓のwhile文を抜けて良いかどうか（項目詳細から戻ってきたときに再度他メニューを選択可能にするための制御）
        while(!finishFlg || !Input.GetKeyDown(KeyCode.X)){
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")>0.0f && typeSelectCoroutine == null){
                stageSelectText.text = stageSelectText.text.Replace("*"," ");
                spellSelected = (int)Mathf.Clamp(spellSelected - 1,1.0f,(float)roomData[roomSelected-1].spell.Count);
                stageSelectText.text = stageSelectText.text.Replace(
                                            "    " + roomData[roomSelected-1].spell[spellSelected-1],
                                            "   *" + roomData[roomSelected-1].spell[spellSelected-1]);
            }
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")<0.0f && typeSelectCoroutine == null){
                stageSelectText.text = stageSelectText.text.Replace("*"," ");
                spellSelected = (int)Mathf.Clamp(spellSelected + 1,1.0f,(float)roomData[roomSelected-1].spell.Count);
                stageSelectText.text = stageSelectText.text.Replace(
                                            "    " + roomData[roomSelected-1].spell[spellSelected-1],
                                            "   *" + roomData[roomSelected-1].spell[spellSelected-1]);
            }
            if(Input.GetButtonDown("Submit") && typeSelectCoroutine == null){
                typeSelectCoroutine = CreateTypeCoroutine();
                StartCoroutine(typeSelectCoroutine);
                finishFlg = false;
            }

            if(!finishFlg && typeSelectCoroutine == null){ finishFlg = true;}//次の階層のコルーチンから復帰したらフラグ戻す

            yield return null;
        }

        yield return new WaitUntil( () => Input.GetKeyDown(KeyCode.X));
    }

    //** 自機タイプ選択コルーチン
    private IEnumerator CreateTypeCoroutine(){

        //** 自機タイプ選択画面起動
        typeSelectWindow.gameObject.SetActive(true);

        yield return OnTypeSelect();
        //yield return null;

        //Debug.Log("type stop前：" + typeSelectCoroutine);
        StopCoroutine(typeSelectCoroutine);
        typeSelectCoroutine = null;

        //** 自機タイプ選択画面終了
        typeSelectWindow.gameObject.SetActive(false);

        //スペル選択に戻す
        spellDisp();
        spellSelected = 1;

    }

    private IEnumerator OnTypeSelect(){
        //** 初期表示
        
        yield return null;

        //Debug.Log("typeSelectCoroutine = " + typeSelectCoroutine);
        yield return new WaitUntil( () => Input.GetKeyDown(KeyCode.X));
    }

    //** ルーム選択の初期表示
    private void roomDisp(){
        stageSelectText.text = "ルーム選択：\n";
        for(int i=0; i<roomData.Length; i++){
            if(i==0){
                stageSelectText.text += "   *" + roomData[i].name + "\n";
            }else{
                stageSelectText.text += "    " + roomData[i].name + "\n";
            }
        }
    }

    //** スペル選択の初期表示
    private void spellDisp(){
        stageSelectText.text = "スペル選択：\n";
        for(int i=0; i<roomData[roomSelected-1].spell.Count; i++){            
            if(i==0){
                stageSelectText.text += "   *" + roomData[roomSelected-1].spell[i] + "\n";
            }else{
                stageSelectText.text += "    " + roomData[roomSelected-1].spell[i] + "\n";
            }
        }
    }

}
