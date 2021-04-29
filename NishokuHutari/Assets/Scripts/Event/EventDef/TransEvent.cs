using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransEvent : MonoBehaviour
{
    [HideInInspector]
    public Image mistakeFilterImage;

    [HideInInspector]
    public string transType;
    private int transactionId;
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

        //** 次のマップデータをロードして構築する（プレイヤー・マップデータ・アイテムなど）
        Instantiate(player,playerPosition,Quaternion.identity);        
        Instantiate(mapDetail,new Vector3(0.0f,0.0f,0.0f),Quaternion.identity);
        foreach(ItemData item in Manager.generalData.itemData){
            if(transactionId == item.transactionId && !item.compFlg){
                Instantiate(item.itemObj,item.itemPosition,Quaternion.identity);
            }
        }
        if(Manager.userData.mistakeFlg){
            mistakeFilterImage.color = new Color(0.8f,0.3f,0.3f,0.5f);
        }else{
            mistakeFilterImage.color = new Color(0.8f,0.3f,0.3f,0.0f);
        }
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

        transactionId = id;

        //誤√判定
        if(id == 6 && !Manager.userData.mistakeFlg){
            Debug.Log("誤√へ");
            Manager.userData.mistakeFlg = true;
        }
        //誤√復帰判定
        if((id == 22 || id == 26) && Manager.userData.mistakeFlg){
            Debug.Log("誤√復帰");
            Manager.userData.mistakeFlg = false;
        }

        if(!Manager.userData.mistakeFlg){
            player = Manager.gameObjectLoader.GetObjectHandle("Player");
        }else{
            player = Manager.gameObjectLoader.GetObjectHandle("Player_Mistake");
        }
        foreach(MapData m in Manager.generalData.mapData){
            if(id == m.transitionId){
                Manager.nowTransitionId = m.transitionId;
                mapDetail = m.mapDetail;
                playerPosition = m.playerPosition;


            }
        }

        //Debug.Log("SetTransData: mapDetail = " + mapDetail + " player = " + player + " playerPosition = " + playerPosition);
    }


}
