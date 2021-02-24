using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu
{
    private Text fravorText;
    private Text itemListText;

    private List<string> itemList;
    private Dictionary<string,string> fravorDictionary;
    private string selected;

    public ItemMenu(){

        Debug.Log("ItemMenu create");

        //** アイテムデータ取得
        itemList = new List<string>();
        itemList.Add("* アイテム1");
        itemList.Add("  アイテム2");
        itemList.Add("  アイテム3");
        fravorDictionary = new Dictionary<string,string>();
        fravorDictionary["アイテム1"] = "アイテム1のフレーバーテキスト";
        fravorDictionary["アイテム2"] = "アイテム2のフレーバーテキスト";
        fravorDictionary["アイテム3"] = "アイテム3のフレーバーテキスト";

        //** コンポーネント取得
        Text[] text = GameObject.FindObjectsOfType<Text>();
        foreach(Text t in text){
            if(t.name == "fravor_text"){
                fravorText = t;
            }else if(t.name == "itemList_text"){
                itemListText = t;
            }
        }

        if(fravorText == null || itemListText == null){
            Debug.Log("fravortext,itemListText 取得失敗");
        }

    }

    public IEnumerator disp(){
        Debug.Log("disp");

        //** 初期表示
        itemListText.text = dispString();
        fravorText.text = fravorDictionary[itemList[0].Substring(2)];
        yield return null;

        //** アイテム操作
        while(!Input.GetKeyDown(KeyCode.X)){
            yield return null;
            
        }

        //** Xキー押下でアイテム操作終了
        yield return new WaitUntil( () => Input.GetKeyDown(KeyCode.X));
    }

    private string dispString(){
        string list = "";
        for(int i=0; i<itemList.Count; i++){
            if(i%2==0){
                list += itemList[i];
            }else{
                list += "          " + itemList[i] + "\n";
            }
        }
        return list;
    }

}
