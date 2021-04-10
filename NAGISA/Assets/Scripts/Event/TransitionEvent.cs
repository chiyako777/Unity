using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionEvent : MonoBehaviour
{
    private string dist = "Bullet";
    private int typeSelected = 1;    //仮：本実装はユーザー情報から引き渡し
    private string roomName = "テストルーム";     //仮：本実装はユーザー情報から引き渡し

    public IEnumerator OnAction(){

        yield return null;

        if(dist == "Bullet"){
            SceneManager.sceneLoaded += OnBulletLoaded;
        }
        SceneManager.LoadScene(dist);

        yield return new WaitForSeconds(1);

        //※暗転等の演出は後で入れる

    }

    //** Bulletシーン遷移前に呼ばれる
    private void OnBulletLoaded(Scene nextScene,LoadSceneMode mode){
        //Debug.Log("OnSceneLoaded:scene.name =  " + nextScene.name);
        //Debug.Log("OnSceneLoaded:mode =  " + mode);

        //BulletMainManagerに、ロードするデータファイル名、シーン名を渡す
        GameObject mainController = GameObject.Find("bullet_main_controller");
        var bulletMainManager = mainController.GetComponent<BulletMainManager>();
        bulletMainManager.loadFileName = Application.dataPath + "/StaticData" + "/bullet/" + "Room_Test.txt";
        BulletMainManager.beforeScene = "Room_White";

        //自機タイプ
        string type = "";
        switch(typeSelected){
            case 1:
                type = "Homing";
                break;
            case 2:
                type = "Reflec";
                break;
            case 3:
                //warp:未実装
                type = "Reflec";
                break;
            default:
                break;
        }

        //PlayerControllerに、自機タイプを渡す
        GameObject player = GameObject.Find("bullet_player");
        var playerController = player.GetComponent<PlayerController>();
        playerController.optionType = type;

        playerController.life = 5;      //※残機・ボム⇒暫定で決め打ち（実際はユーザー情報から渡す）
        playerController.bomb = 5;
        playerController.power = 1.0f;

        //InfoControllerに、自機タイプ、ルーム名を渡す
        GameObject bulletInfo = GameObject.Find("bulletinfo_controller");
        var infoController = bulletInfo.GetComponent<InfoController>();
        infoController.option = type;
        infoController.room = roomName;

        SceneManager.sceneLoaded -= OnBulletLoaded;
    }

    //** 遷移データ読み込み、セット
    public void SetTransData(int id){

        //** ファイル読み込み、シリアライズ
        string f = Application.dataPath + "/StaticData" + "/data/" + "event_trans.json";
        EventTransData[] data = null;
        if(File.Exists(f)){
            data = JsonHelper.FromJson<EventTransData>(File.ReadAllText(f));
        }

        //** メンバにセット
        foreach(EventTransData tr in data){
            if(tr.id == id){
                dist = tr.dist;
            }
        }

    }

}
