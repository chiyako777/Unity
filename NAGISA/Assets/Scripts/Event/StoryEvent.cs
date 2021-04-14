using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public class StoryEvent : MonoBehaviour
{

    private PlayableDirector playableDirector;
    private bool isPlaying = false;

    public IEnumerator OnAction(){
        yield return null;

        //** タイムライン実行
        if(playableDirector.state != PlayState.Playing && !isPlaying){
            Debug.Log("タイムライン実行");
            playableDirector.Play();
            isPlaying = true;
            yield return null;
        }

        //** 実行中
        while(isPlaying){
            //** タイムライン終了検知(PlayableDirector.Playing以外にもファクターは必要になると思うので、適宜追加)
            if(playableDirector.state != PlayState.Playing){
                Debug.Log("タイムライン実行中※※※※");
                isPlaying = false;
            }
            yield return null;
        }

        Debug.Log("タイムライン終了");
        yield break;

    }

    public void SetStoryData(int id){
        //** ファイル読み込み、シリアライズ
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
            GameObject[] obj = GameObject.FindGameObjectsWithTag("Timeline");
            foreach(GameObject ob in obj){
                if(ob == null) { continue;}
                if(ob.name == ev.timeline){
                    playableDirector = ob.GetComponent<PlayableDirector>();
                    //Debug.Log("playableDirector =  " + playableDirector); 
                }
            }
        }

    }
}
