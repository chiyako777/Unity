using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class StoryEvent : MonoBehaviour
{

    private PlayableDirector playableDirector;
    private float finishTime;

    private bool finishFlg = false;
    private bool isPlaying = false;

    public IEnumerator OnAction(){
        yield return null;

        //** タイムライン実行
        if(playableDirector.state != PlayState.Playing && !isPlaying){
            //Debug.Log("タイムライン実行");
            playableDirector.Play();
            isPlaying = true;
            yield return null;
        }

        //** 実行中
        while(isPlaying){
            //** タイムライン終了検知
            // if(playableDirector.time >= finishTime){
            //     finishFlg = true;       //タイムラインの最後まで行ってPausedになると、timeが0に戻るみたいなので別途フラグ管理
            // }
            // if(playableDirector.state != PlayState.Playing 
            //     && finishFlg){
            //     //Debug.Log("タイムライン idPlaying falseに更新");
            //     //Debug.Log("Current:" + playableDirector.time);
            //     isPlaying = false;
            // }
            //Debug.Log("PlayableDirector : duration = " + playableDirector.duration);

            //一旦、タイムラインを途中でPausedにすることはないという前提の元下記で終了判定（一時停止のときは再生速度でとめるから）
            if(playableDirector.state != PlayState.Playing){
                isPlaying = false;
            }
            //Debug.Log("タイムラインOnAction()実行中 state = " + playableDirector.state + " time = " + playableDirector.time + " finishTime = " + finishTime);
            yield return null;
        }

        Debug.Log("タイムライン終了");
        yield break;

    }

    public void SetStoryData(int id){
        //** ファイル読み込み、シリアライズ
        //Debug.Log("StoryEvent:SetStoryData");
        string f = Application.dataPath + "/StaticData" + "/data/" + "event_story.json";
        EventStoryData[] data = null;
        if(File.Exists(f)){
            data = JsonHelper.FromJson<EventStoryData>(File.ReadAllText(f));
        }

        //** メンバにセット
        foreach(EventStoryData ev in data){
            if(ev.id != id){
                continue;
            }
            //** タイムラインオブジェクト生成
            GameObject obj = Instantiate(
                                SceneCustomManager.resourcesLoader.GetObjectHandle(ev.timeline),
                                new Vector3(0.0f,0.0f,0.0f),
                                Quaternion.identity);
            playableDirector = obj.GetComponent<PlayableDirector>();
            
            //** 動的バインディング
            List<PlayableBinding> bind = new List<PlayableBinding>();
            for(int i=0; i < ev.track.Count; i++){
                bind.Add(playableDirector.playableAsset.outputs.First(c => c.streamName == ev.track[i]));
                switch(ev.bindComponent[i]){
                    case "Animator":
                        Animator anim = GameObject.Find(ev.bindObject[i]).GetComponent<Animator>();
                        playableDirector.SetGenericBinding(bind[i].sourceObject,anim);
                        break;
                    default:
                        break;
                }
            }

            //** 終了時間
            finishTime = ev.finishtime;  
            //Debug.Log("finishTime = " + finishTime);
        }

    }

    public void Resume(){
        //playableDirector.Resume();
        //Pause,Resumeだと、直前のフレームの状態が維持できないので、再生速度を変更する形で停止再開する
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1.0f);
    }

}
