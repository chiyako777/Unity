using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AddressableAssets;
public class aaa : MonoBehaviour
{

    void Start()
    {
        // var settings = AddressableAssetSettingsDefaultObject.Settings;
        // for(int i=1; i <=25; i++){
        //   settings.RemoveLabel("Map_" + i);  
        // }
        //settings.RemoveLabel("Map_34");
        //settings.RemoveLabel("Map_35");

        Invoke("testTrans",3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void testTrans(){
        Debug.Log("TestTranss");
        EventQueue ev = new EventQueue("Trans",9999,1);
        if(EventManager.IsAdd(ev.level)){
            EventManager.AddEvent(ev);
        }
        
    }
}
