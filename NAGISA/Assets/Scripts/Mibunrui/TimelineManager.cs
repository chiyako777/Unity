using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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
    }

    //** 一時停止のラッパーメソッド
    public void OnPause(){
        // Debug.Log("TimelineManager : OnPause playableDirector = " + playableDirector);
        // Debug.Log("playableDirector.playableGraph = " + playableDirector.playableGraph);
        // Debug.Log("playableDirector.playableGraph.GetRootPlayable(0) = " + playableDirector.playableGraph.GetRootPlayable(0));
        //Pauseじゃなくてspeedを0にするという形で止めてみる
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0.0f);

    }

}
