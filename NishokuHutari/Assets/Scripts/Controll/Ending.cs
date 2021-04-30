using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField]
    private Image finishImage;
    [HideInInspector]
    public int progress = 0;   //0:待機、1:エンディング処理、2:終了

    void Start()
    {
        
    }

    void Update()
    {

        if(progress == 1 && !Manager.userData.mistakeFlg){
            //** true end
            Debug.Log("True End再生");
            EventQueue endingEv = new EventQueue("Story",3,10);
            if(EventManager.IsAdd(endingEv.level)){
                EventManager.AddEvent(endingEv);
            }
            EventQueue finishEv = new EventQueue("Finish",1,50);
            if(EventManager.IsAdd(finishEv.level,true)){
                EventManager.AddEvent(finishEv);
            }
            ++progress;
        }else if(progress == 1 && Manager.userData.mistakeFlg){
            //** bad end
            Debug.Log("Bad End再生");
            EventQueue endingEv = new EventQueue("Story",2,10);
            if(EventManager.IsAdd(endingEv.level)){
                EventManager.AddEvent(endingEv);
            }
            EventQueue finishEv = new EventQueue("Finish",1,50);
            if(EventManager.IsAdd(finishEv.level,true)){
                EventManager.AddEvent(finishEv);
            }
            ++progress;
        }

    }

}
