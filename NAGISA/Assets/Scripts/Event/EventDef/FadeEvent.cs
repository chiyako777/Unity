using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FadeEvent : MonoBehaviour
{
    //** UI
    [SerializeField]
    private Canvas window;
    [SerializeField]
    private Image fadeImage;
    [SerializeField]
    private Text fadeText;

    //** 暗転データ
    private Color fadeColor;
    private string fadeMessage;
    private int fadePattern;
    private float fadeTime;

    private int status;     //1:フェードイン 2:暗転中 3:フェードアウト

    //private const int 
    private const float lerpStep = 0.016f;

    void Start()
    {
        window.gameObject.SetActive(false);
    }

    public IEnumerator OnAction(){

        //** 初期化
        window.gameObject.SetActive(true);
        status = 1;
        yield return null;

        //** 暗転処理(Pattern = 1)
        if(fadePattern == 1){

            if(status == 1){
                float lerpValue = 0.0f;
                while(lerpValue < 1.0f){
                    FadeAlpha(lerpValue);
                    lerpValue += lerpStep;
                    yield return null;
                }
                status = 2;
            }

            if(status == 2){
                fadeText.text = fadeMessage;
                yield return new WaitForSeconds(fadeTime);
            }

            status = 3;
            if(status == 3){
                fadeText.text = "";
                float lerpValue = 1.0f;
                while(lerpValue > 0.0f){
                    FadeAlpha(lerpValue);
                    lerpValue -= lerpStep;
                    yield return null;
                }
            }

        }

        //** 終了処理
        window.gameObject.SetActive(false);
        yield break;
        
    }

    //** 暗転イベント読み込み、セット
    public void SetFadeData(int id){

        //** ファイル読み込み、シリアライズ
        string f = Application.dataPath + "/StaticData" + "/data/" + "event_fade.json";
        EventFadeData[] data = null;
        if(File.Exists(f)){
            data = JsonHelper.FromJson<EventFadeData>(File.ReadAllText(f));
        }

        //** メンバにセット
        foreach(EventFadeData ev in data){
            if(ev.id != id){
                continue;
            }
            fadeColor = new Color(ev.color[0],ev.color[1],ev.color[2],ev.color[3]);
            fadeMessage = ev.text;
            fadePattern = ev.pattern;
            fadeTime = ev.time;
        }

    }

    private void FadeAlpha(float lerpValue){
        fadeImage.color = new Color(
                                fadeColor.r,
                                fadeColor.g,
                                fadeColor.b,
                                Mathf.Lerp(0.0f,fadeColor.a,lerpValue));
    }


}
