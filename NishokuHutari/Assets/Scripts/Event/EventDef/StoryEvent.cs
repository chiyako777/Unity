using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

//** シーン内でのイベント
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
            //Debug.Log("タイムライン実行中");
            if(playableDirector.state != PlayState.Playing){
                isPlaying = false;
            }
            yield return null;
        }

        Debug.Log("TImeline Destroy 直前");
        Destroy(playableDirector.gameObject);
        yield break;

    }

    public void SetStoryData(int id){
        
        foreach(StoryData story in Manager.generalData.storyData){
            if(id != story.storyId){
                continue;
            }
            Debug.Log("SetStoryData : タイムライン準備");
            //** タイムラインオブジェクト生成
            GameObject obj = Instantiate(story.timeline,new Vector3(0.0f,0.0f,0.0f),Quaternion.identity);
            playableDirector = obj.GetComponent<PlayableDirector>();

            //** 動的バインディング
            List<PlayableBinding> bind = new List<PlayableBinding>();
            for(int i=0; i < story.trackName.Count; i++){
                bind.Add(playableDirector.playableAsset.outputs.First(c => c.streamName == story.trackName[i]));
                switch(story.componentName[i]){
                    case "Animator":
                        Animator anim = GameObject.Find(story.bindObjectName[i]).GetComponent<Animator>();
                        playableDirector.SetGenericBinding(bind[i].sourceObject,anim);
                        break;
                }
            }
        }

        Debug.Log(" タイムラインオブジェクト = " + playableDirector.gameObject + " playableDirector = " + playableDirector);
    }

}
