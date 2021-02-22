using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MapObjectBase : MonoBehaviour
{
    public Canvas window;

    //接触判定
    private bool isContacted = false;
    private IEnumerator coroutine;

    private void OnTriggerEnter2D(Collider2D collider) {
        //Debug.Log("Trigger Enter");
        isContacted = collider.gameObject.tag.Equals("Player");
    }

    private void OnTriggerExit2D(Collider2D collider) {
        //Debug.Log("Trigger Exit");
        isContacted = !collider.gameObject.tag.Equals("Player");
    }

    void Start(){
        //Debug.Log("coroutine false initial");
        FlagManager.flagDictionary["coroutine"] = false;
    }

    void FixedUpdate(){
        //オブジェクトに接触している状態でEnterを押したら、コルーチンを開始
        if (isContacted && coroutine == null && Input.GetButton("Submit") && Input.anyKeyDown) {
            //Debug.Log("start coroutine");
            FlagManager.flagDictionary["coroutine"] = true;
            coroutine = CreateCoroutine();
            StartCoroutine(coroutine);
        }
    }

    //リアクション用コルーチン
    private IEnumerator CreateCoroutine(){
        //window起動
        window.gameObject.SetActive(true);

        //アクション呼び出し
        yield return OnAction();

        //window終了
        this.window.gameObject.SetActive(false);

        StopCoroutine(coroutine);
        FlagManager.flagDictionary["coroutine"] = false;
        coroutine = null;
    }

    protected abstract IEnumerator OnAction();

}
