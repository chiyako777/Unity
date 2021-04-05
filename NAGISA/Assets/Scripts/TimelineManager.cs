using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

//何らかのキーアクションをトリガとして、タイムラインを止めたり再開したりする
public class TimelineManager : MonoBehaviour
{
    private PlayableDirector playableDirector;
    private int frameCount = 0;

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
        if(frameCount == 0){
            Debug.Log("タイムライン一時停止");
            playableDirector.Pause();
        }

        if(frameCount > 60 && Input.GetKeyDown(KeyCode.Y)){
            Debug.Log("タイムライン起動");
            playableDirector.Resume();
        }

        ++frameCount;

    }
}
