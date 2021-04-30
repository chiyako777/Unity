using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//まず選択肢からゲット系⇒道に落ちてる系の順で実装していく
public class ItemEvent : MonoBehaviour
{
    private int itemId;
    private List<string> messageContents;
    private Sprite sprite;

    //** UI
    [HideInInspector]
    public Image itemGetImage;
    [HideInInspector]
    public Text messageText;

    void Start()
    {
        itemGetImage.gameObject.SetActive(false);       
    }

    public IEnumerator OnAction(){

        itemGetImage.gameObject.SetActive(true);
        itemGetImage.sprite = sprite;
        yield return null;

        //** ゲットイメージ表示
        int dispStatus = 1;
        while(dispStatus < 4){
            if(dispStatus == 1){
                while(itemGetImage.color.a <= 0.5f){
                    float alpha = itemGetImage.color.a + 0.003f;
                    itemGetImage.color = new Color(1.0f,1.0f,1.0f,alpha);
                    yield return null;
                }
                dispStatus = 2;
            }
            if(dispStatus == 2){
                yield return new WaitForSeconds(0.5f);
            }
            dispStatus = 3;
            if(dispStatus == 3){
                while(itemGetImage.color.a >= 0.0f){
                    float alpha = itemGetImage.color.a - 0.003f;
                    itemGetImage.color = new Color(1.0f,1.0f,1.0f,alpha);
                    yield return null;
                }                
            }
            dispStatus = 4;
        }
        itemGetImage.gameObject.SetActive(false);

        //** キャプション表示
        int progress = 0;
        int charaStep = 0;
        int frame = 0;
        int frameStep = 30;
        int len = messageContents[progress].Length;
        while(progress < messageContents.Count){
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
            yield return null;
        }
        messageText.text = "";

        //** 終了処理
        foreach(ItemData itemData in Manager.generalData.itemData){
            if(itemId == itemData.itemId){
                itemData.compFlg = true;
            }
        }
        //※道に落ちてる系Itemは各マップに一つしかないという前提の下のオブジェクト削除処理
        GameObject obj = GameObject.FindWithTag("Item");
        if(obj != null){
            Destroy(obj);
        }

        yield break;
    }


    public void SetItemData(int id){
        itemId = id;
        foreach(ItemData itemData in Manager.generalData.itemData){
            if(id == itemData.itemId){
                sprite = Manager.spriteLoader.GetObjectHandle(itemData.spriteName);
                messageContents = itemData.messageContents;
            }
        }
    }

}