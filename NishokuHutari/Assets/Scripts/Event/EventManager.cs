using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    private static List<EventQueue> eventList = new List<EventQueue>();
    private int nowIndex = 0;      //現在実行中のイベントのインデックス
    private const int defaultIndex = 99;

    void Start(){
        DontDestroyOnLoad(this);
    }
    void Update(){
        Exec();
    }

    //** イベントを実行
    private void Exec(){

        //** イベント実行指示を受ける
        int index = GetExecEvent();
        if(index == defaultIndex){
            return;     //実行指示なし（=未発生か、発生中で更新の必要なし)
        }else{
            nowIndex = index;
            //Debug.Log("Exec:nowIndex = " + nowIndex);
        }

        FlagManager.flagDictionary["coroutine"] = true;
        eventList[nowIndex].coroutine = CreateCoroutine();
        StartCoroutine(eventList[nowIndex].coroutine);

    }

    private int GetExecEvent(){
        int index = 0;

        //イベントなし
        if(eventList.Count == 0){    
            return defaultIndex;
        }

        //現在実行中イベントの後続に、より優先度の高いイベントが差し込まれたら、
        //対象インデックス更新する
        for(int i=nowIndex; i<eventList.Count; i++){
            if(eventList[i].level <= eventList[nowIndex].level){
                index = i;
            }
        }

        //対象インデックスが未実行なら、実行指示
        if(eventList[index].coroutine == null){
            return index;
        }else{
            return defaultIndex;
        }
    }

    //** イベントコルーチン
    private IEnumerator CreateCoroutine(){
        //Debug.Log("EventManager:CreateCoroutine");
        yield return null;
        switch(eventList[nowIndex].eventType){
            // case "Conv":
            //     //Debug.Log("EventManager:CreateCoroutine Conv");
            //     ConversationEvent convEvent = GetComponentInChildren<ConversationEvent>();
            //     convEvent.SetConvData(eventList[nowIndex].eventId);
            //     yield return convEvent.OnAction();
            //     break;
            case "TransReturn":
                //Debug.Log("EventManager:CreateCoroutine TransReturn");
                TransEvent transReturnEvent = GetComponentInChildren<TransEvent>();
                transReturnEvent.transType = "TransReturn";
                transReturnEvent.SetTransData(eventList[nowIndex].eventId);
                yield return transReturnEvent.OnAction();
                break;
            case "Trans":
                //Debug.Log("EventManager:CreateCoroutine Trans");
                TransEvent transEvent = GetComponentInChildren<TransEvent>();
                transEvent.transType = "Trans";
                transEvent.SetTransData(eventList[nowIndex].eventId);
                yield return transEvent.OnAction();
                break;
            case "Select":
                //Debug.Log("EventManager:CreateCoroutine Select");
                SelectEvent selectEvent = GetComponentInChildren<SelectEvent>();
                selectEvent.SetSelectData(eventList[nowIndex].eventId);
                yield return selectEvent.OnAction();
                break;
            case "NAGISACutIn":
                //Debug.Log("EventManager:CreateCoroutine NAGISACutIn");
                NAGISACutInEvent nagisaEvent = GetComponentInChildren<NAGISACutInEvent>();
                nagisaEvent.SetNAGISAData(eventList[nowIndex].eventId);
                yield return nagisaEvent.OnAction();
                break;
            case "Story":
                //Debug.Log("EventManager:CreateCoroutine Story");
                StoryEvent storyEvent = GetComponentInChildren<StoryEvent>();
                storyEvent.SetStoryData(eventList[nowIndex].eventId);
                yield return storyEvent.OnAction();
                break;
            // case "Fade":
            //     //Debug.Log("EventManager:CreateCoroutine Fade");
            //     FadeEvent fadeEvent = GetComponentInChildren<FadeEvent>();
            //     fadeEvent.SetFadeData(eventList[nowIndex].eventId);
            //     yield return fadeEvent.OnAction();
            //     break;
            case "":
                break;
        }

        //** イベント終了処理

        //リスト削除
        Debug.Log("イベント終了：eventType = " + eventList[nowIndex].eventType + " nowIndex = " + nowIndex);
        StopCoroutine(eventList[nowIndex].coroutine);
        //タイムラインの場合、タイムラインオブジェクトの削除処理をそのうち入れる
        eventList[nowIndex].coroutine = null;
        eventList.RemoveAt(nowIndex);

        //インデックス更新
        for(int i=nowIndex-1; i>=0; i--){
            if(eventList[i].coroutine != null){
                nowIndex = i;
                //戻し後のイベントがタイムラインの場合、再開処理
                // if(eventList[nowIndex].eventType == "Story"){
                //     StoryEvent storyEvent = GetComponentInChildren<StoryEvent>();
                //     storyEvent.Resume();
                // }
            }
        }

        //全体フラグ更新
        bool allFinishFlg = true;
        foreach(EventQueue ev in eventList){
            if(ev.coroutine != null) {allFinishFlg = false;}
        }
        if(allFinishFlg){
            FlagManager.flagDictionary["coroutine"] = false;
        }

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

    //** UI系コンポーネントバインド
    public void BindObject(){
        TransEvent tr = GetComponentInChildren<TransEvent>();
        tr.mistakeFilterImage = GameObject.Find("MistakeFilter_Image").GetComponent<Image>();
        SelectEvent se = GetComponentInChildren<SelectEvent>();
        se.whiteImage = GameObject.Find("Selection_White_Image").GetComponent<Image>();
        se.blackImage = GameObject.Find("Selection_Black_Image").GetComponent<Image>();
        se.whiteImage.gameObject.SetActive(false);
        se.blackImage.gameObject.SetActive(false);
    }

}