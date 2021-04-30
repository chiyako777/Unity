using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectEvent : MonoBehaviour
{
    //** Data
    private int whiteTransidionId;
    private int blackTransidionId;
    private string answer;

    //** UI
    [HideInInspector]
    public Image whiteImage;
    [HideInInspector]
    public Image blackImage;

    private int selected = 0;   //0:white 1:black

    void Start(){
        whiteImage.gameObject.SetActive(false);
        blackImage.gameObject.SetActive(false);
    }

    void Update(){
    }

    public IEnumerator OnAction(){

        //** 初期化(UIアクティブ化)
        whiteImage.gameObject.SetActive(true);
        blackImage.gameObject.SetActive(true);
        yield return null;

        //** 選択コルーチン
        while(!Input.GetButtonDown("Submit")){
            
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")>0.0f){
                //上ボタン
                selected = (int)Mathf.Clamp(selected-1,0.0f,1.0f);
                UpdateDisp();
            }
            if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")<0.0f){
                //下ボタン
                selected = (int)Mathf.Clamp(selected+1,0.0f,1.0f);
                UpdateDisp();
            }
            yield return null;
        }

        //** 終了処理
        whiteImage.gameObject.SetActive(false);
        blackImage.gameObject.SetActive(false);

        //** Itemイベント(あれば)
        foreach(ItemData itemData in Manager.generalData.itemData){
            EventQueue itemEv = null;
            if((whiteTransidionId == itemData.transactionId && selected == 0 && !itemData.compFlg) ||
                (blackTransidionId == itemData.transactionId && selected == 1 && !itemData.compFlg)){
                itemEv = new EventQueue("Item",itemData.itemId,5);
                if(EventManager.IsAdd(itemEv.level)){
                    EventManager.AddEvent(itemEv);
                }
            }
        }

        //** Transイベント
        EventQueue transEv = null;
        if(selected == 0){
            transEv = new EventQueue("Trans",whiteTransidionId,20);
        }else{
            transEv = new EventQueue("Trans",blackTransidionId,20);
        }
        if(EventManager.IsAdd(transEv.level)){
            EventManager.AddEvent(transEv);
        }

        yield break;
    }

    public void SetSelectData(int id){
        foreach(SelectData s in Manager.generalData.selectData){
            if(id == s.selectId){
                whiteTransidionId = s.whiteTransidionId;
                blackTransidionId = s.blackTransidionId;
                answer = s.answer;
            }
        }
    }

    private void UpdateDisp(){
        if(selected == 0){
            whiteImage.color = new Color(1.0f,0.0f,0.0f,1.0f);
            blackImage.color = new Color(0.0f,0.0f,1.0f,1.0f);
        }else{
            whiteImage.color = new Color(0.0f,0.0f,1.0f,1.0f);
            blackImage.color = new Color(1.0f,0.0f,0.0f,1.0f);
        }
    }

}
