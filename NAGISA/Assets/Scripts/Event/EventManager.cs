using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static List<EventQueue> eventList = new List<EventQueue>();

    void Start(){

    }
    void Update(){
        Exec();
    }

    //** イベントキュー先頭のイベントを実行
    private void Exec(){

        if(!IsExec()){
            return;
        }

        FlagManager.flagDictionary["coroutine"] = true;
        eventList[0].coroutine = CreateCoroutine();
        StartCoroutine(eventList[0].coroutine);

    }

    //** 実行するか判定
    private bool IsExec(){
        if(eventList.Count == 0){
            //イベント発生なし
            return false;
        }
        if(eventList[0].coroutine != null){
            //イベント発生中
            return false;
        }
        return true;
    }

    //** イベントコルーチン
    private IEnumerator CreateCoroutine(){
        //Debug.Log("EventManager:CreateCoroutine");
        switch(eventList[0].eventType){
            case "Conv":
                Debug.Log("EventManager:CreateCoroutine Conv");
                ConversationEvent convEvent = GetComponentInChildren<ConversationEvent>();
                convEvent.SetConvData(eventList[0].eventId);
                yield return convEvent.OnAction();
                break;
            case "Trans":
                Debug.Log("EventManager:CreateCoroutine Trans");
                TransitionEvent transEvent = GetComponentInChildren<TransitionEvent>();
                transEvent.SetTransData(eventList[0].eventId);
                yield return transEvent.OnAction();
                break;
            case "Story":
                Debug.Log("EventManager:CreateCoroutine Story");
                StoryEvent storyEvent = GetComponentInChildren<StoryEvent>();
                storyEvent.SetStoryData(eventList[0].eventId);
                yield return storyEvent.OnAction();
                break;
            case "":
                break;
        }

        //イベント終了、リストから削除
        StopCoroutine(eventList[0].coroutine);
        FlagManager.flagDictionary["coroutine"] = false;
        eventList[0].coroutine = null;
        eventList.RemoveAt(0);
    }

    //** イベントリストに追加するか判定
    public static bool IsAdd(int level){
        if(eventList.Count == 0){
            //キューが空なら無条件で追加
            return true;
        }
        if(eventList[0].coroutine != null && eventList[0].level <= level){
            //イベント中、かつ実行中イベントの方がレベルが上位か同じ
            return false;
        }
        return true;
    }

    //** イベントリストに追加
    public static void AddEvent(EventQueue ev){
        //Debug.Log("EventManager : AddEvent");
        eventList.Add(ev);
    }

}
