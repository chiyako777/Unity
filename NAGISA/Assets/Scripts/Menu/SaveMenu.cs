using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveMenu
{
    private Text saveListText;
    private Text saveConfirmText;
    private Image saveConfirmImage;
    private Text saveCompleteText;
    private Image saveCompleteImage;

    private int slotNum;
    private int selected;   //1start

    public SaveMenu(){
        //Debug.Log("createSaveMenu");
        //** コンポーネント取得
        Text[] text = GameObject.FindObjectsOfType<Text>();
        foreach(Text t in text){
            if(t.name == "saveList_text"){
                saveListText = t;
            }
            if(t.name == "saveConfirm_text"){
                saveConfirmText = t;
            }
            if(t.name == "saveComplete_text"){
                saveCompleteText = t;
            }
        }
        Image[] image = GameObject.FindObjectsOfType<Image>();
        foreach(Image i in image){
            if(i.name == "saveConfirm_Image"){
                saveConfirmImage = i;
            }
            if(i.name == "saveComplete_image"){
                saveCompleteImage = i;
            }
        }

        // if(saveListText == null || saveConfirmText == null || saveConfirmImage == null){
        //     Debug.Log("saveListText 取得失敗");
        // }

        //** セーブ確認・完了画面を非表示化
        saveConfirmImage.enabled = false;
        saveConfirmText.enabled = false;
        saveCompleteImage.enabled = false;
        saveCompleteText.enabled = false;

        //** セーブスロット数
        slotNum = 4;
    }

    public IEnumerator disp(){

        //** 初期表示
        bool execFlg = false;
        selected = 1;
        string s = "";
        for(int i=1; i<=slotNum; i++){
            string f = Application.dataPath + "/StaticData" + "/save/" + "save" + i + ".json";
            if(File.Exists(f)){
                Debug.Log("セーブファイルあり");
                //** セーブ時間を画面表示
                SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(f));
                Debug.Log("time = " + data.time);
                if(i==selected){
                    s += "* セーブ" + i + ":     データあり\n                   " +　data.time +  "\n";
                }else{
                    s += "  セーブ" + i + ":     データあり\n                   " +　data.time +  "\n";
                }
                
            }else{
                Debug.Log("セーブファイルなし");
                if(i==selected){
                    s += "* セーブ" + i + ":     データなし\n                   0000/00/00 00:00:00" + "\n";
                }else{
                    s += "  セーブ" + i + ":     データなし\n                   0000/00/00 00:00:00" + "\n";
                }
            }
        }
        saveListText.text = s;
        yield return null;

        //** セーブスロット選択
        while(!Input.GetKeyDown(KeyCode.X)){
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")>0.0f && !execFlg){
                saveListText.text = saveListText.text.Replace("* セーブ" + selected,"  セーブ" + selected);
                selected = (int)Mathf.Clamp(selected - 1,1.0f,(float)slotNum);
                saveListText.text = saveListText.text.Replace("  セーブ" + selected,"* セーブ" + selected);
            }
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")<0.0f && !execFlg){
                saveListText.text = saveListText.text.Replace("* セーブ" + selected,"  セーブ" + selected);
                selected = (int)Mathf.Clamp(selected + 1,1.0f,(float)slotNum);
                saveListText.text = saveListText.text.Replace("  セーブ" + selected,"* セーブ" + selected);
            }
            
            //** セーブ実行
            if(Input.GetButtonDown("Submit")){
                execFlg = true;
                saveConfirmImage.enabled = true;
                saveConfirmText.enabled = true;
                yield return OnExec();
                execFlg = false;
                saveConfirmImage.enabled = false;
                saveConfirmText.enabled = false;
            }

            yield return null;
        }
        
        yield break;
    }

    private IEnumerator OnExec(){
        Debug.Log("Save OnExec()");
        //** 初期表示
        bool isOnExec = false;
        bool isYes = false;
        saveConfirmText.text = "セーブ" + selected + ":セーブしますか？\n    はい\n*   いいえ";
        yield return null;

        while(!Input.GetKeyDown(KeyCode.X)){
            //** セーブ実行するか否か選択
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")>0.0f && !isOnExec){
                isYes = true;
                saveConfirmText.text = saveConfirmText.text.Replace("    はい","*   はい");
                saveConfirmText.text = saveConfirmText.text.Replace("*   いいえ","    いいえ");
            }
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")<0.0f && !isOnExec){
                isYes = false;
                saveConfirmText.text = saveConfirmText.text.Replace("*   はい","    はい");
                saveConfirmText.text = saveConfirmText.text.Replace("    いいえ","*   いいえ");
            }

            //** セーブ処理実行
            if(Input.GetButtonDown("Submit") && isYes){
                isOnExec = true;
                execSave();
                isOnExec = false;
                saveCompleteImage.enabled = true;
                saveCompleteText.enabled = true;
                yield return new WaitForSeconds(1);
                saveCompleteImage.enabled = false;
                saveCompleteText.enabled = false;
                yield break;
            }else if(Input.GetButtonDown("Submit") && !isYes){
                yield break;
            }

            yield return null;
        }
        //Debug.Log("OnExecのループ抜ける");

        yield return new WaitUntil( () => Input.GetKeyDown(KeyCode.X));
    }


    //** セーブデータ作成&セーブ実行
    private void execSave(){
        SaveData data = new SaveData();

        //現在日時
        System.DateTime d  = System.DateTime.Now;
        string ds = d.ToString("yyyy/MM/dd HH:mm:ss");
        //Debug.Log("DateTime.Now = " + ds);
        data.time = ds;
        
        //** jsonにシリアライズ
        string json = JsonUtility.ToJson(data);

        //** ファイル書き込み(存在しなければ新規作成)
        string f = Application.dataPath + "/StaticData" + "/save/" + "save" + selected + ".json";
        File.WriteAllText(f,json);

    }

}
