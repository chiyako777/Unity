using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransEvent : MonoBehaviour
{
    [HideInInspector]
    public string transType;
    private GameObject mapDetail;
    private GameObject player;
    private Vector3 playerPosition;

    public IEnumerator OnAction(){

        //Debug.Log("TransEvent.OnAction: mapDetail = " + mapDetail + " player = " + player + " playerPosition = " + playerPosition);
        yield return null;

        //** 前のマップデータを破棄する（プレイヤー・マップデータ）
        GameObject oldPlayer = GameObject.FindWithTag("Player");
        GameObject oldMap = GameObject.FindWithTag("Map");
        if(oldPlayer != null){
            Destroy(oldPlayer);
        }
        if(oldMap != null){
            Destroy(oldMap);
        }

        //** 次のマップデータをロードして構築する（プレイヤー・マップデータ）
        Instantiate(player,playerPosition,Quaternion.identity);
        Instantiate(mapDetail,new Vector3(0.0f,0.0f,0.0f),Quaternion.identity);
        if(transType == "Trans"){
            //Debug.Log("TransEvent:ターン数デクリメント");
            Manager.userData.remainTurn -= 1;
        }

        //** 特定の残りターン数ならば、NAGISAカットシーン発生
        if(Manager.generalData.nagisaCutInIndex.ContainsKey(Manager.userData.remainTurn)){
            int eventId = Manager.generalData.nagisaCutInIndex[Manager.userData.remainTurn];
            foreach(NAGISACutInData n in Manager.generalData.nagisaCutInData){
                if(eventId == n.nagisaCutInId && !n.compFlg){
                    Debug.Log("TransEvent ⇒ NAGISACutIn発生");
                    EventQueue ev = new EventQueue("NAGISACutIn",eventId,10);
                    if(EventManager.IsAdd(10)){
                        EventManager.AddEvent(ev);
                    }
                }
            }
        }

        


        yield break;
    }

    public void SetTransData(int id){
        player = Manager.gameObjectLoader.GetObjectHandle("Player");
        foreach(MapData m in Manager.generalData.mapData){
            if(id == m.transitionId){
                mapDetail = m.mapDetail;
                playerPosition = m.playerPosition;
            }
        }

        //Debug.Log("SetTransData: mapDetail = " + mapDetail + " player = " + player + " playerPosition = " + playerPosition);
    }


}
