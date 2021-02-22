using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageCharactor : MapObjectBase
{
    public Text targetMessage;
    public Text selection1;
    public Text selection2;
    public Text selection3;

    //** 選択肢制御
    private const string selectEscape = "SELECT:";
    private string s1,s2,s3;
    private int selected = 0;
    private IEnumerator selectCoroutine;

    [SerializeField]
    private List<string> messages;

    protected override IEnumerator OnAction(){
        
        for(int i=0; i<messages.Count; ++i){
            //※memo:ループ毎に2回returnする※
            //初期化&1フレーム待機
            setMessage("");
            setSelection(1,"",0);
            setSelection(2,"",0);
            setSelection(3,"",0);
            Debug.Log("i = " + i + " yield return null");
            yield return null;

            //メッセージ設定
            if(!messages[i].Contains(selectEscape)){
                Debug.Log("i = " + i + " 地の文");
                setMessage(messages[i]);
            }else{
                Debug.Log("i = " + i + " 選択肢");
                setMessage(messages[i-1]);

                s1 = messages[i].Substring(selectEscape.Length);
                s2 = s1.Substring(s1.IndexOf(selectEscape) + selectEscape.Length);
                s3 = s2.Substring(s2.IndexOf(selectEscape) + selectEscape.Length);
                s1 = s1.Substring(0,s1.IndexOf(selectEscape));
                s2 = s2.Substring(0,s2.IndexOf(selectEscape));
                selected = 1;
                setSelection(1,s1,selected);
                setSelection(2,s2,selected);
                setSelection(3,s3,selected);

                selectCoroutine = CreateSelectCoroutine();
                StartCoroutine(selectCoroutine);
            }

            //キー入力を待機
            yield return new WaitUntil( () => Input.GetButtonDown("Submit"));
        }

        setMessage("");
        yield break;
    }

    //メッセージ設定
    private void setMessage(string message){
        this.targetMessage.text = message;
    }

    //選択肢設定
    private void setSelection(int no, string message, int selected){
        string setMessage;
        if(no==selected){
            setMessage = "* " + message;
        }else{
            setMessage = "  " + message;
        }
        switch(no){
            case 1:
                this.selection1.text = setMessage;
                break;
            case 2:
                this.selection2.text =  setMessage;
                break;
            case 3:
                this.selection3.text =  setMessage;
                break;
            default:
                break;
        }
        
    }

    //** 選択肢コルーチン
    private IEnumerator CreateSelectCoroutine(){
        yield return OnSelect();
        StopCoroutine(selectCoroutine);
    }

    private IEnumerator OnSelect(){
        Debug.Log("OnSelect");
        int count = 0;
        while(!Input.GetButtonDown("Submit") || count > 2000){
            if(Input.GetAxis("Horizontal")>0.0f){
                Debug.Log("right");
                selected = (int)Mathf.Clamp(selected + 1,1.0f,3.0f);
            }else if(Input.GetAxis("Horizontal")<0.0f){
                Debug.Log("left");
                selected = (int)Mathf.Clamp(selected - 1,1.0f,3.0f);
            }

            setSelection(1,s1,selected);
            setSelection(2,s2,selected);
            setSelection(3,s3,selected);

            count++;
        }

        yield return new WaitUntil( () => Input.GetButtonDown("Submit"));
    }

}
