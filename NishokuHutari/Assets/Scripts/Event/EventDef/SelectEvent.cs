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
    [SerializeField]
    private Image whiteImage;
    [SerializeField]
    private Image blackImage;

    private int selected = 0;   //0:white 1:black

    void Start(){
        whiteImage.gameObject.SetActive(false);
        blackImage.gameObject.SetActive(false);
    }

    void Update(){
        //** UIへの紐づけが外れていたら設定しなおす（EventManagerがDontDestroyのため、シーン遷移を挟むと外れる）
        if(whiteImage == null && SceneManager.GetActiveScene().name == "Map"){
            GameObject ui = GameObject.Find("Map_UI");
            whiteImage = ui.transform.Find("Selection/Selection_White_Image").gameObject.GetComponent<Image>();
        }
        if(blackImage == null && SceneManager.GetActiveScene().name == "Map"){
            GameObject ui = GameObject.Find("Map_UI");
            blackImage = ui.transform.Find("Selection/Selection_Black_Image").gameObject.GetComponent<Image>();
        }
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

        //** 確定⇒transイベント起こす
        whiteImage.gameObject.SetActive(false);
        blackImage.gameObject.SetActive(false);
        EventQueue ev = null;
        if(selected == 0){
            ev = new EventQueue("Trans",whiteTransidionId,20);
        }else{
            ev = new EventQueue("Trans",blackTransidionId,20);
        }
        if(EventManager.IsAdd(20)){
            EventManager.AddEvent(ev);
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
