using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** タイムラインの中から発生するイベントトリガ(タイムラインオブジェクトにアタッチする形)
public class TimelineTrigger : MonoBehaviour
{
    public List<string> eventType = new List<string>();
    public List<int> eventId = new List<int>();
    public List<int> eventLevel = new List<int>();      //イベントレベル基本方針：会話=1、遷移=1 、ストーリーイベント=10

    private int progress = 0;

    //** イベントトリガ
    public void OnSignalReceived(){
        //Debug.Log("OnSignalReceived");
        EventQueue ev = new EventQueue(eventType[progress],eventId[progress],eventLevel[progress]);
        if(EventManager.IsAdd(eventLevel[progress])){
            EventManager.AddEvent(ev);
        }
        ++progress;
    }
}
