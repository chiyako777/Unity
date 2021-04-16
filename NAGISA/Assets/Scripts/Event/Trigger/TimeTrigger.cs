using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 時限イベントトリガ
//** 現行：シーン遷移後〇〇秒後にイベント発生
//** 必要に応じて、機能拡張
public class TimeTrigger : MonoBehaviour
{
    public float invokeTime;
    public string eventType;
    public int eventId;
    public int eventLevel;      //イベントレベル基本方針：会話=1、遷移=1 、ストーリーイベント=10

    void Start()
    {
        Invoke("TimeEvent",invokeTime);
    }

    public void TimeEvent(){
        EventQueue ev = new EventQueue(eventType,eventId,eventLevel);
        if(EventManager.IsAdd(eventLevel)){
            EventManager.AddEvent(ev);
        }
    }
}
