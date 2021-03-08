using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SpellPracController : MonoBehaviour
{

    private Text stageSelectText;
    private List<RoomData> roomData;

    void Start()
    {
        //** コンポーネント取得
        Text[] text = GameObject.FindObjectsOfType<Text>();
        foreach(Text t in text){
            if(t.name == "stageSelect_text"){
                stageSelectText = t;
            }
        }

        //** ルームデータ読み込み
        string f = Application.persistentDataPath + "/data/" + "room.json";
        //roomData = JsonUtility.FromJson<List<RoomData>>(File.ReadAllText(f));
        RoomData rData = JsonUtility.FromJson<RoomData>(File.ReadAllText(f));
        Debug.Log("roonName = " + rData.name);
        for(int i=0; i<rData.spell.Count; i++){
            Debug.Log("spell = " + rData.spell[i]);
        }

    }

    void Update()
    {
        
    }


}
