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
    private int selected;   //0start

    public ItemMenu(){

        Debug.Log("ItemMenu create");

        //** アイテムデータ取得
        itemList = new List<string>();
        itemList.Add("  アイテム1");
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
        selected = 0;
        updateSelectedMark();
        itemListText.text = dispString();
        fravorText.text = fravorDictionary[itemList[0].Substring(2)];
        yield return null;

        //** アイテム操作
        while(!Input.GetKeyDown(KeyCode.X)){

            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")>0.0f){
                selected = (int)Mathf.Clamp(selected - 2,0.0f,itemList.Count-1);
                updateSelectedMark();
                itemListText.text = dispString();
                fravorText.text = fravorDictionary[itemList[selected].Substring(2)];
                MusicManager.PlaySelectSE();
            }
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")<0.0f){
                selected = (int)Mathf.Clamp(selected + 2,0.0f,itemList.Count-1);
                updateSelectedMark();
                itemListText.text = dispString();
                fravorText.text = fravorDictionary[itemList[selected].Substring(2)];
                MusicManager.PlaySelectSE();
            }
            if(Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal")>0.0f){
                selected = (int)Mathf.Clamp(selected + 1,0.0f,itemList.Count-1);
                updateSelectedMark();
                itemListText.text = dispString();
                fravorText.text = fravorDictionary[itemList[selected].Substring(2)];
                MusicManager.PlaySelectSE();
            }
            if(Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal")<0.0f){
                selected = (int)Mathf.Clamp(selected - 1,0.0f,itemList.Count-1);
                updateSelectedMark();
                itemListText.text = dispString();
                fravorText.text = fravorDictionary[itemList[selected].Substring(2)];
                MusicManager.PlaySelectSE();
            }

            yield return null;
        }

        yield break;
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

    private void updateSelectedMark(){

        for(int i=0; i<itemList.Count; i++){
            if(i==selected){
                itemList[i] = itemList[i].Replace("  " , "* ");
            }else{
                itemList[i] = itemList[i].Replace("* " , "  ");
            }
        }        
    }

}
