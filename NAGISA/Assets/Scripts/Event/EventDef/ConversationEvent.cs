using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ConversationEvent : MonoBehaviour
{

    //** UI
    [SerializeField]
    private Canvas window;
    [SerializeField]
    private Text targetMessage;
    [SerializeField]
    private Text selection1;
    [SerializeField]
    private Text selection2;
    [SerializeField]
    private Text selection3;
    [SerializeField]
    private Image charaGraph;

    //** 選択肢制御
    private const string selectEscape = "SELECT:";
    private List<int> selected = new List<int>();
    private IEnumerator selectCoroutine;

    //** 会話データ
    private List<string> messages;
    private List<Sprite> graphics = new List<Sprite>();

    void Start(){
        window.gameObject.SetActive(false);
    }

    public IEnumerator OnAction(){

        window.gameObject.SetActive(true);
        for(int i=0; i<messages.Count; ++i){
            //** 初期化&1フレーム待機
            setMessage("");
            setSelection(null,null,null);
            yield return null;

            //** メッセージ設定
            if(!messages[i].Contains(selectEscape)){
                
                if(selected.Count == 0){
                    //地の文
                    //Debug.Log("i = " + i + " 地の文");
                    setMessage(messages[i]);
                    charaGraph.sprite = graphics[i];
                }
                else{
                    //選択肢分岐後の地の文
                    //Debug.Log("i = " + i + " 選択肢分岐後 地の文");

                    string s = "";
                    foreach(int data in selected){
                        s += "SELECT" + data.ToString() + ":";
                    }

                    if(messages[i].Length > s.Length && messages[i].Substring(0,s.Length).Equals(s)){                        
                        setMessage(messages[i].Substring(s.Length));
                        charaGraph.sprite = graphics[i];
                    }else{
                        continue;
                    }
                }

            }else{
                //Debug.Log("i = " + i + " 選択肢");

                string s1;
                if(selected.Count == 0){
                    //1回目の選択肢
                    s1 = messages[i].Substring(selectEscape.Length);
                    setMessage(messages[i-1]);
                    charaGraph.sprite = graphics[i];
                }else{
                    //選択肢分岐後の選択肢
                    string s = "";
                    foreach(int data in selected){
                        s += "SELECT" + data.ToString() + ":";
                    }
                    if(messages[i].Length < s.Length || !messages[i].Substring(0,s.Length).Equals(s)){
                        continue;
                    }
                    setMessage(messages[i-1].Substring(s.Length));
                    charaGraph.sprite = graphics[i];
                    s1 = messages[i].Substring(s.Length + selectEscape.Length);
                }
                string s2 = s1.Substring(s1.IndexOf(selectEscape) + selectEscape.Length);
                string s3 = s2.Substring(s2.IndexOf(selectEscape) + selectEscape.Length);
                s1 = s1.Substring(0,s1.IndexOf(selectEscape));
                s2 = s2.Substring(0,s2.IndexOf(selectEscape));
                selected.Add(1);
                setSelection(s1,s2,s3);

                //選択コルーチンの開始
                selectCoroutine = CreateSelectCoroutine(s1,s2,s3);
                StartCoroutine(selectCoroutine);
            }

            //** メッセージ送りのキー入力待機
            yield return new WaitUntil( () => Input.GetButtonDown("Submit"));
        }

        //** 初期状態に戻す
        setMessage("");
        setSelection(null,null,null);
        selected.Clear();
        window.gameObject.SetActive(false);
        yield break;
    }

    //** メッセージ設定
    private void setMessage(string message){
        this.targetMessage.text = message;
    }

    //** 選択肢設定
    private void setSelection(string s1,string s2,string s3){
        if(s1 == null && s2 == null && s3 == null){
                this.selection1.text = s1;
                this.selection2.text = s2;
                this.selection3.text = s3;
                return;
        } 
        switch(selected[selected.Count-1]){
            case 1:
                this.selection1.text = "* " + s1;
                this.selection2.text = "  " + s2;
                this.selection3.text = "  " + s3;
                break;
            case 2:
                this.selection1.text = "  " + s1;
                this.selection2.text = "* " + s2;
                this.selection3.text = "  " + s3;
                break;
            case 3:
                this.selection1.text = "  " + s1;
                this.selection2.text = "  " + s2;
                this.selection3.text = "* " + s3;
                break;
            default:
                break;
        }
        return;
    }

    //** 選択肢コルーチン
    private IEnumerator CreateSelectCoroutine(string s1,string s2,string s3){
        yield return OnSelect(s1,s2,s3);
        StopCoroutine(selectCoroutine);
    }

    //** 選択肢セレクトルーチン処理
    private IEnumerator OnSelect(string s1,string s2,string s3){
        //Debug.Log("OnSelect");
        
        while(!Input.GetButtonDown("Submit")){
            if(Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal")>0.0f){
                //右矢印ボタン
                if(!s3.Equals("")){
                    selected[selected.Count-1] = (int)Mathf.Clamp(selected[selected.Count-1] + 1,1.0f,3.0f);
                }else{
                    selected[selected.Count-1] = (int)Mathf.Clamp(selected[selected.Count-1] + 1,1.0f,2.0f);
                }
            }else if(Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal")<0.0f){
                //左矢印ボタン
                if(!s3.Equals("")){
                    selected[selected.Count-1] = (int)Mathf.Clamp(selected[selected.Count-1] - 1,1.0f,3.0f);
                }else{
                    selected[selected.Count-1] = (int)Mathf.Clamp(selected[selected.Count-1] - 1,1.0f,2.0f);
                }
            }

            setSelection(s1,s2,s3);

            yield return null;
        }

        //選択肢確定
        yield return new WaitUntil( () => Input.GetButtonDown("Submit"));
    }

    //** 会話データ読み込み、セット
    public void SetConvData(int id){

        //** ファイル読み込み、シリアライズ
        string f = Application.dataPath + "/StaticData" + "/data/" + "event_conv.json";
        EventConvData[] data = null;
        if(File.Exists(f)){
            data = JsonHelper.FromJson<EventConvData>(File.ReadAllText(f));
        }

        //** メンバにセット
        foreach(EventConvData ev in data){
            if(ev.id != id){
                continue;
            }
            //メッセージ
            messages = ev.messages;
            //顔グラ（スプライトデータに変換）
            foreach(string graName in ev.graphics){
                graphics.Add(SceneCustomManager.spriteLoader.GetObjectHandle(graName));
            }
        }
    }

}
