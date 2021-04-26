using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FadeEvent : MonoBehaviour
{
    //** UI
    private Canvas window;
    private Image fadeImage;
    private Text fadeText;

    //** 暗転データ
    private Color fadeColor;
    private string fadeMessage;
    private int fadePattern;
    private float fadeTime;

    //** const
    private const float lerpStep = 0.016f;      //pattern=1:透明度変化係数

    void Start()
    {
    }

    public IEnumerator OnAction(){

        //** 初期化
        window.gameObject.SetActive(true);
        yield return null;

        //** 暗転処理(Pattern = 1):透明度変化による演出
        if(fadePattern == 1){

            int status = 1;

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
        
        //** 暗転処理(Pattern = 2):ルール画像によるフェード演出
        if(fadePattern == 2){
            Fade fade = window.gameObject.GetComponentInChildren<Fade>();
            yield return fade.FadeOut(fadeTime,null);   //UIがだんだんかぶさる
            yield return fade.FadeIn(fadeTime,null);    //UIがだんだん掃ける
            Debug.Log("Fade終了");
        }

        //** 終了処理
        window.gameObject.SetActive(false);
        Destroy(window.gameObject);
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
            GameObject fadeUI = Instantiate(SceneCustomManager.gameObjectLoader.GetObjectHandle(ev.windowUI),new Vector3(0.0f,0.0f,0.0f),Quaternion.identity);
            window = fadeUI.GetComponent<Canvas>();
            fadeImage = fadeUI.GetComponentInChildren<Image>();
            fadeText = fadeUI.GetComponentInChildren<Text>();
            //Debug.Log("window = " + window + " fadeImage = " + fadeImage + " fadeText = " + fadeText);
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

