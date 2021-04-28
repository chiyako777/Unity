using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpSelect : Warp
{
    [SerializeField]
    public int selectId;

    void Start()
    {
        
    }

    void Update()
    {
        if (isContacted &&
             Input.GetButton("Submit") && Input.anyKeyDown &&
             !(bool)FlagManager.flagDictionary["coroutine"]) {
            Debug.Log("Select実行");
            EventQueue ev = new EventQueue("Select",selectId,40);
            if(EventManager.IsAdd(40)){
                EventManager.AddEvent(ev);
            }
        }
        
    }
}
