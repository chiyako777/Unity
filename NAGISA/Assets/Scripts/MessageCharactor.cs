using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCharactor : MapObjectBase
{
    [SerializeField]
    private List<string> messages;

    protected override IEnumerator OnAction(){
        for(int i=0; i<messages.Count; ++i){
            //1フレーム分処理を待機
            yield return null;

            //会話をwindowのtextに設定
            setMessage(messages[i]);

            //キー入力を待機
            yield return new WaitUntil( () => Input.GetButtonDown("Submit"));
        }

        yield break;
    }
}
