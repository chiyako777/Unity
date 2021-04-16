using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayableBehaviourMy : PlayableBehaviour{
    public GameObject objMy;

    public override void OnGraphStart(Playable playable){
        Debug.Log("OnGraphStart");
    }

    //** Timeline全体終了、一時停止時
    public override void OnGraphStop(Playable playable){
        Debug.Log("OnGraphStop");
        Debug.Log("objMy = " + objMy);
        Debug.Log("objMy.transform.position = " + objMy.transform.position);

    }
    public override void OnBehaviourPlay(Playable playable, FrameData info){
        Debug.Log("OnBehaviourPlay");
    }        
    public override void OnBehaviourPause(Playable playable, FrameData info){
        Debug.Log("OnBeaviourPause");
    }
    public override void PrepareFrame(Playable playable, FrameData info){
        //Debug.Log("PrepareFrame");
    }
}
