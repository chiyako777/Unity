using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** フィールドマップオブジェクトを調べたときのイベントトリガ
public class MapObjectTrigger : MonoBehaviour
{
    [SerializeField]
    private string eventType;
    [SerializeField]
    private int eventId;
    [SerializeField]
    private int eventLevel;      //イベントレベル基本方針：会話=1、遷移=1 、ストーリーイベント=10
    private bool isContacted = false;
    

    private void OnTriggerEnter2D(Collider2D collider) {
        //Debug.Log("MO Trigger Enter");
        isContacted = collider.gameObject.tag.Equals("Player");
    }

    private void OnTriggerExit2D(Collider2D collider) {
        //Debug.Log("Trigger Exit");
        isContacted = !collider.gameObject.tag.Equals("Player");
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isContacted && Input.GetButton("Submit") && Input.anyKeyDown) {
            EventQueue ev = new EventQueue(eventType,eventId,eventLevel);
            if(EventManager.IsAdd(eventLevel)){
                EventManager.AddEvent(ev);
            }
        }
    }


}
