using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MOTransition : MapObjectBase
{
    public string dist;
    public int typeSelected;    //仮：本実装はユーザー情報から引き渡し
    public string roomName;     //仮：本実装はユーザー情報から引き渡し
    protected override IEnumerator OnAction(){

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

        //MainManagerに、ロードするデータファイル名、シーン名を渡す
        GameObject mainController = GameObject.Find("bullet_main_controller");
        var mainManager = mainController.GetComponent<MainManager>();
        mainManager.loadFileName = Application.persistentDataPath + "/bullet/" + "Room_Test.txt";
        MainManager.beforeScene = "Room_White";

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

        //InfoControllerに、自機タイプ、ルーム名を渡す
        GameObject bulletInfo = GameObject.Find("bulletinfo_controller");
        var infoController = bulletInfo.GetComponent<InfoController>();
        infoController.option = type;
        infoController.room = roomName;

        SceneManager.sceneLoaded -= OnBulletLoaded;
    }

}
