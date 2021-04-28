using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueue{
    public IEnumerator coroutine; 
    public string eventType;
    public int eventId;
    public int level;       //イベントレベル：より上位のイベント実行中は下位のイベントのトリガ無効（数字が若いほど上位）

    public EventQueue(string eventType,int eventId,int level){
        coroutine = null;
        this.eventType = eventType;
        this.eventId = eventId;
        this.level = level;
    }
}