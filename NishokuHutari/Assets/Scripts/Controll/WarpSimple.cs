using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpSimple : Warp
{
    [SerializeField]
    private int transitionId;

    void Start()
    {
        
    }

    void Update()
    {
        if (isContacted &&
             Input.GetButton("Submit") && Input.anyKeyDown &&
             !(bool)FlagManager.flagDictionary["coroutine"]) {
            //Debug.Log("Trans実行");
            EventQueue ev = new EventQueue("Trans",transitionId,20);
            if(EventManager.IsAdd(20)){
                EventManager.AddEvent(ev);
            }
        }
    }
}
