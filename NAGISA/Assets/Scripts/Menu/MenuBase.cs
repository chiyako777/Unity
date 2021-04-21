﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBase : MonoBehaviour
{
    [SerializeField]
    private Canvas menuWindow;
    [SerializeField]
    private List<Image> menuImage;
    [SerializeField]
    private Canvas itemWindow;
    [SerializeField]
    private Canvas saveWindow;

    private IEnumerator menuCoroutine;
    private IEnumerator detailCoroutine;
    private int selected;   //1start
    private Color defaultCol;

    void Start()
    {
        //Debug.Log("★start menu");
        FlagManager.flagDictionary["coroutine"] = false;
        defaultCol = new Color(0.9215f,0.4941f,0.7411f,1.0f);

        menuWindow.gameObject.SetActive(false);
        itemWindow.gameObject.SetActive(false);
        saveWindow.gameObject.SetActive(false);

    }
    
    void Update()
    {
        //Debug.Log("update");
        if(menuCoroutine == null && Input.GetKeyDown(KeyCode.X) && !(bool)FlagManager.flagDictionary["coroutine"]){
            Debug.Log("x押下");
            FlagManager.flagDictionary["coroutine"] = true;
            menuCoroutine = CreateMenuCoroutine();
            StartCoroutine(menuCoroutine);
        }
        
    }

    //** メニューコルーチン生成
    private IEnumerator CreateMenuCoroutine(){
        //Debug.Log("createCoroutine");
        //window起動
        menuWindow.gameObject.SetActive(true);

        //アクション呼び出し
        yield return OnAction();
        //Debug.Log("OnAction抜ける");

        //選択していた項目の色を戻す
        menuImage[selected-1].color = defaultCol;

        //window終了
        this.menuWindow.gameObject.SetActive(false);

        StopCoroutine(menuCoroutine);
        FlagManager.flagDictionary["coroutine"] = false;
        menuCoroutine = null;

    }

    //** メニュー選択コルーチン
    private IEnumerator OnAction(){
        //Debug.Log("OnAction");

        //** 初期選択
        menuImage[0].color = Color.blue;
        selected = 1;
        yield return null;

        bool finishFlg = true;  //↓のwhile文を抜けて良いかどうか（項目詳細から戻ってきたときに再度他メニューを選択可能にするための制御）
                                //戻る操作が基本的にXキーなので、ちゃんと1段階ずつ戻すための制御
        //** 選択
        while(!finishFlg || !Input.GetKeyDown(KeyCode.X)){
            
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")>0.0f && detailCoroutine == null){
                menuImage[selected-1].color = defaultCol;
                selected = (int)Mathf.Clamp(selected - 1,1.0f,5.0f);
                menuImage[selected-1].color = Color.blue;
                MusicManager.PlaySelectSE();
            }
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")<0.0f && detailCoroutine == null){
                menuImage[selected-1].color = defaultCol;
                selected = (int)Mathf.Clamp(selected + 1,1.0f,5.0f);
                menuImage[selected-1].color = Color.blue;
                MusicManager.PlaySelectSE();
            }

            //** 確定
            if(Input.GetButtonDown("Submit") && detailCoroutine == null){
                Debug.Log("確定");
                detailCoroutine = createDetailCoroutine();
                StartCoroutine(detailCoroutine);
                Debug.Log("createDetailCoroutine終了");
                finishFlg = false;
            }

            //Debug.Log("OnActionのwhile");   //detailコルーチン中でもこっちのwhileも回り続けている
            if(!finishFlg && detailCoroutine == null){ finishFlg = true;}//詳細メニューから復帰したらフラグ戻す
            yield return null;
        }

        Debug.Log("OnAction終了直前");

        //** Xキー押下でメニュー操作終了
        yield return new WaitUntil( () => Input.GetKeyDown(KeyCode.X));
    }

    private IEnumerator createDetailCoroutine(){
        //Debug.Log("createCoroutine");

        //アクション呼び出し
        yield return OnDetail();

        StopCoroutine(detailCoroutine);
        detailCoroutine = null;
    }

    //** メニュー詳細コルーチン
    private IEnumerator OnDetail(){
        Debug.Log("OnDetail");
        yield return null;
        switch(selected){
            case 1: //アイテム

                //アイテム画面起動
                itemWindow.gameObject.SetActive(true);
                
                yield return OnItemMenu();
                //yield return null;

                //Debug.Log("アイテム画面閉じる");
                //アイテム画面閉じる
                itemWindow.gameObject.SetActive(false);
                
                break;

            case 2: //弾幕設定
                break;
            case 3: //セーブ
                //セーブ画面起動
                saveWindow.gameObject.SetActive(true);

                yield return OnSaveMenu();

                //セーブ画面起動
                saveWindow.gameObject.SetActive(false);
                break;
            case 4: //各種設定
                break;
            case 5: //ゲーム終了
                SceneManager.LoadScene("Title");
                break;
            default:
                break;
        }
    }

    //** アイテムメニュー
    private IEnumerator OnItemMenu(){
        Debug.Log("OnItemMenu");

        ItemMenu itemMenu = new ItemMenu();
        yield return itemMenu.disp();

        //** Xキー押下でアイテム操作終了
        yield return new WaitUntil( () => Input.GetKeyDown(KeyCode.X));
    }

    //** セーブメニュー
    private IEnumerator OnSaveMenu(){

        SaveMenu saveMenu = new SaveMenu();
        yield return saveMenu.disp();
        //** Xキー押下でセーブ操作終了
        yield return new WaitUntil( () => Input.GetKeyDown(KeyCode.X));

    }


}
