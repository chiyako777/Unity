using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** タイムラインの中から発生するイベントトリガ(タイムラインオブジェクトにアタッチする形)
public class TimelineTrigger : MonoBehaviour
{
    public string eventType;
    public int eventId;
    public int eventLevel;      //イベントレベル基本方針：会話=1、遷移=1 、ストーリーイベント=10

    //** イベントトリガ
    public void OnSignalReceived(){
        //Debug.Log("OnSignalReceived");
        EventQueue ev = new EventQueue(eventType,eventId,eventLevel);
        if(EventManager.IsAdd(eventLevel)){
            EventManager.AddEvent(ev);
        }
    }
}
