using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class FadePB : PlayableBehaviour
{
    public Color fadeColor;
    private Image fadeImage;

    private int status = 1; 
    private int frame = 0;
    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
        fadeImage = GameObject.Find("Fade_Image").GetComponent<Image>();
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        //** 指定の色でフェード(1.色濃くなる 2.待機 3.色薄くなる)
        if(status > 3){
            return;
        }
        
        if(status == 1 && fadeImage.color.a < fadeColor.a){
            fadeImage.color = new Color(fadeColor.r,fadeColor.g,fadeColor.b,fadeImage.color.a + 0.03f);
        }else if(status == 1 && fadeImage.color.a >= fadeColor.a){
            status = 2;
            frame = 0;
        }

        if(status == 2 && frame >= 90){
            status = 3;
        }

        if(status == 3 && fadeImage.color.a >= 0){
            fadeImage.color = new Color(fadeColor.r,fadeColor.g,fadeColor.b,fadeImage.color.a - 0.03f);
        }else if(status == 3 && fadeImage.color.a < 0){
            status = 4;
        }

        if(status == 2){
            ++frame;
        }

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
