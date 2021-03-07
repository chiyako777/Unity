using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveMenu
{
    private Text saveListText;
    
    private int slotNum;
    private int selected;

    public SaveMenu(){
        //Debug.Log("createSaveMenu");
        //** コンポーネント取得
        Text[] text = GameObject.FindObjectsOfType<Text>();
        foreach(Text t in text){
            if(t.name == "saveList_text"){
                saveListText = t;
            }
        }

        if(saveListText == null){
            Debug.Log("saveListText 取得失敗");
        }

        //** セーブスロット数
        slotNum = 4;
    }

    public IEnumerator disp(){

        //** 初期表示
        string s = "";
        for(int i=0; i<slotNum; i++){
            string f = Application.persistentDataPath + "/save/" + "save" + (i+1) + ".json";
            if(File.Exists(f)){
                Debug.Log("セーブファイルあり");
            }else{
                s += "  セーブ" + (i+1) + ":     データなし\n                   0000/00/00 00:00:00" + "\n";
                Debug.Log("セーブファイルなし");
            }
        }
        saveListText.text = s;
        yield return null;

        while(!Input.GetKeyDown(KeyCode.X)){
            yield return null;
        }
        
        yield break;
    }

}
