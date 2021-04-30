using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class EndingPB1 : PlayableBehaviour
{
    private Text messageText;
    public List<string> messageContents;

    private int progress = 0;
    private int charaStep = 0;
    private int frame = 0;
    private const int frameStep = 30;

    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
        messageText = GameObject.Find("UI").GetComponentInChildren<Text>();
        //Debug.Log("messageText" + messageText);
        messageText.text = "";

    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        //** 自動テキスト送り
        if(progress >= messageContents.Count){
            messageText.text = "";
            return;
        }

        int len = messageContents[progress].Length;
        if(charaStep <= len){
            messageText.text = messageContents[progress].Substring(0,charaStep);
        }else if((charaStep - len) == 4){
            charaStep = 0;
            ++progress;
            if(progress < messageContents.Count){
                len = messageContents[progress].Length;
            }
        }
        if(frame%frameStep == 0){
            ++charaStep;
        }

        ++frame;
        
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {
        
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        
    }

}
