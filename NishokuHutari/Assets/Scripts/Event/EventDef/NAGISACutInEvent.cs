using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

//** シーンの遷移を伴うストーリーイベント：こっち
public class NAGISACutInEvent : MonoBehaviour
{
    private int eventId;
    private GameObject timeline;
    private PlayableDirector playableDirector;

    private bool isPlaying = false;

    public IEnumerator OnAction(){
        //Debug.Log("NAGISACutIn:OnAction()");
        yield return null;

        //** シーン遷移
        SceneManager.LoadScene("NAGISA");
        yield return null;

        //** タイムライン準備
        GameObject t = Instantiate(timeline,new Vector3(0.0f,0.0f,0.0f),Quaternion.identity);
        playableDirector = t.GetComponent<PlayableDirector>();

        List<PlayableBinding> bind = new List<PlayableBinding>();
        foreach(NAGISACutInData nagisa in Manager.generalData.nagisaCutInData){
            if(eventId == nagisa.nagisaCutInId){
                for(int i=0; i < nagisa.trackName.Count; i++){
                    bind.Add(playableDirector.playableAsset.outputs.First(c => c.streamName == nagisa.trackName[i]));
                    switch(nagisa.componentName[i]){
                        case "Animator":
                            Animator anim = GameObject.Find(nagisa.bindObjectName[i]).GetComponent<Animator>();
                            playableDirector.SetGenericBinding(bind[i].sourceObject,anim);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        yield return null;

        //** タイムライン再生
        if(playableDirector.state != PlayState.Playing && !isPlaying){
            Debug.Log("タイムライン再生 playableDirector = " + playableDirector);
            playableDirector.Play();
            isPlaying = true;
            yield return null;
        }

        //** タイムライン実行中
        while(isPlaying){
            Debug.Log("タイムライン再生中 playableDirector.state = " + playableDirector.state);
            if(playableDirector.state != PlayState.Playing){
                isPlaying = false;
            }
            yield return null;
        }

        //** 終了処理
        foreach(NAGISACutInData n in Manager.generalData.nagisaCutInData){
            if(eventId == n.nagisaCutInId){
                Debug.Log("NAGISA:Compフラグ立てる");
                n.compFlg = true;
            }
        }

        Debug.Log("タイムライン終了");

        //** シーン戻る
        SceneManager.sceneLoaded += OnMapLoaded;
        SceneManager.LoadScene("Map");

        //Debug.Log("NAGISACutIn:タイムライン終了");

        yield break;
    }

    public void SetNAGISAData(int id){

        eventId = id;
        foreach(NAGISACutInData nagisa in Manager.generalData.nagisaCutInData){
            if(id == nagisa.nagisaCutInId){
                timeline = nagisa.timeline;
            }
        }

        //Debug.Log("SetNAGISAData : timeline = " + timeline);
    }

    private void OnMapLoaded(Scene nextScene,LoadSceneMode mode){
        //Debug.Log("OnMapLoaded:マップ復帰時処理");
        Manager.returnFlg = true;
    }
}
