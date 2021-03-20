using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MOTransition : MapObjectBase
{
    public string dist;
    public int typeSelected;    //仮：本実装はユーザー情報から引き渡し
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

        //MainManagerに、ロードするデータファイル名を渡す
        GameObject mainController = GameObject.Find("bullet_main_controller");
        var mainManager = mainController.GetComponent<MainManager>();
        mainManager.loadFileName = Application.persistentDataPath + "/bullet/" + "Room_Test.txt";

        //PlayerControllerに、自機タイプを渡す
        GameObject player = GameObject.Find("bullet_player");
        var playerController = player.GetComponent<PlayerController>();
        switch(typeSelected){
            case 1:
                playerController.optionType = "Homing";
                break;
            case 2:
                playerController.optionType = "Reflec";
                break;
            case 3:
                //warp:未実装
                playerController.optionType = "Reflec";
                break;
            default:
                break;
        }

        SceneManager.sceneLoaded -= OnBulletLoaded;
    }

}
