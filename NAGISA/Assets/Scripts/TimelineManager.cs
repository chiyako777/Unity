using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//TimeLineManager プロトタイプ
//何らかのキーアクションをトリガとして、タイムラインを止めたり再開したりする
//メッセージを出したりする
public class TimelineManager : MonoBehaviour
{
    private PlayableDirector playableDirector;

    void Start()
    {
        //** コンポーネント取得
        playableDirector = GetComponent<PlayableDirector>();
        if(playableDirector == null){
            Debug.Log("PlayableDirector 取得失敗");
        }

    }

    void Update()
    {

        //Debug.Log("time = " + playableDirector.time);
        
        //** ある位置まで行ったら一時停止
        if(playableDirector.state == PlayState.Playing && playableDirector.time > 7){
            Debug.Log("タイムライン一時停止");
            playableDirector.Pause();
        }

        if(playableDirector.state == PlayState.Paused && Input.GetKeyDown(KeyCode.Y)){
            Debug.Log("タイムライン起動");
            playableDirector.Resume();
        }

    }
}
