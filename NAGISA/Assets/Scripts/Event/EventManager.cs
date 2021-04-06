using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//commit test
public class EventManager : MonoBehaviour
{

    public class EventQueue{
        public IEnumerator coroutine; 
        public string eventType;
        public int eventId;
    }
    private List<EventQueue> eventList = new List<EventQueue>();

    void Start(){

    }
    void Update(){

    }

    // //** イベントキュー先頭のイベントを実行
    private void Exec(){

        if(!IsExec()){
            return;
        }

        eventList[0].coroutine = CreateCoroutine();
        StartCoroutine(eventList[0].coroutine);

    }

    //** 本当に実行していいかの何らかの判定が必要ならここで
    private bool IsExec(){
        return true;
    }

    private IEnumerator CreateCoroutine(){
        switch(eventList[0].eventType){
            case "Conv":
                break;
            case "Trans":
                break;
            case "":
                break;
        }
        yield return null;
    }

}
