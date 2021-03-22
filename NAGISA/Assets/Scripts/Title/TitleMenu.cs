using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    private Text menuText;
    private int selected;   //1start

    void Start()
    {
        //** タイトルメニュー設定
        selected = 1;
        Text[] text = GameObject.FindObjectsOfType<Text>();
        foreach(Text t in text){
            if(t.name == "TitleMenu_text"){
                menuText = t;
            }
        }
        menuText.text = "  *ニューゲーム   コンティニュー(選択不可)   弾幕プラクティス   ゲーム終了";

    }

    void Update()
    {
        //Debug.Log("Time.deltaTime = " + Time.deltaTime);
        Debug.Log("Time.timeScale = " + Time.timeScale);
        //** タイトルメニュー選択
        if(Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal")>0.0f){
            menuText.text = menuText.text.Replace("*"," ");
            selected = (int)Mathf.Clamp(selected + 1,0.0f,4.0f);
            updateSelectedMark();
        }
        if(Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal")<0.0f){
            menuText.text = menuText.text.Replace("*"," ");
            selected = (int)Mathf.Clamp(selected - 1,0.0f,4.0f);
            updateSelectedMark();
        }

        //** 確定
        if(Input.GetButtonDown("Submit")){
            switch(selected){
                case 1:     //ニューゲーム
                    SceneManager.LoadScene("Room_White");
                    break;
                case 2:     //コンティニュー
                    break;
                case 3:     //弾幕プラクティス
                    toSpellPractice();
                    break;
                case 4:     //ゲーム終了
                    break;
                default:
                    break;
            }
        }

    }

    private void updateSelectedMark(){
        switch(selected){
            case 1:     //ニューゲーム
                menuText.text = menuText.text.Replace("   ニューゲーム","  *ニューゲーム");
                break;
            case 2:     //コンティニュー
                menuText.text = menuText.text.Replace("   コンティニュー","  *コンティニュー");
                break;
            case 3:     //弾幕プラクティス
                menuText.text = menuText.text.Replace("   弾幕プラクティス","  *弾幕プラクティス");
                break;
            case 4:     //ゲーム終了
                menuText.text = menuText.text.Replace("   ゲーム終了","  *ゲーム終了");
                break;
            default:
                break;
        }
    }

    private void toSpellPractice(){
        SceneManager.LoadScene("SpellPracticeSetting");
    }
}
