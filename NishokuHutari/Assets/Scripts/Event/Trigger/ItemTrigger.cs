using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    [SerializeField]
    private int itemId;
    private bool isContacted = false;

    void Start()
    {
        
    }
    void Update()
    {
        if (isContacted &&
             Input.GetButton("Submit") && Input.anyKeyDown &&
             !(bool)FlagManager.flagDictionary["coroutine"]) {
                 Debug.Log("ItemTrigger実行");
                 EventQueue itemEv = new EventQueue("Item",itemId,5);
                 if(EventManager.IsAdd(itemEv.level)){
                     EventManager.AddEvent(itemEv);
                 }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        isContacted = collider.gameObject.tag.Equals("Player");
    }

    private void OnTriggerExit2D(Collider2D collider) {
        isContacted = !collider.gameObject.tag.Equals("Player");
    }
}
