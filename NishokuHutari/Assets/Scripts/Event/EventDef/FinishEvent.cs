using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//** ここからの遷移は現状ないので、簡易的に実装
public class FinishEvent : MonoBehaviour
{
    private Image finishImage;

    public IEnumerator OnAction(){
        finishImage = GameObject.Find("Finish_Image").GetComponent<Image>();
        if(!Manager.userData.mistakeFlg){
            finishImage.sprite = Manager.spriteLoader.GetObjectHandle("test_KimotiOK");
        }else{
            finishImage.sprite = Manager.spriteLoader.GetObjectHandle("test_KimotiNG");
        }
        yield return null;
        
        while(finishImage.color.a < 1.0f){
            finishImage.color = new Color(finishImage.color.r,finishImage.color.g,finishImage.color.b,finishImage.color.a + 0.03f);
            yield return null;
        }

        yield break;

    }

}
