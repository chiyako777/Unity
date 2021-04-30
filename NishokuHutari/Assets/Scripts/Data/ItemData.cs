using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public int itemId;
    public GameObject itemObj;      //道に落ちてる系アイテムなら使用
    public int transactionId;
    public Vector3 itemPosition;    //道に落ちてる系アイテムなら使用
    public string spriteName;
    public List<string> messageContents;
    public bool compFlg;

    public ItemData(int itemId,GameObject itemObj,int transactionId,Vector3 itemPosition,string spriteName,List<string> messageContents){
        this.itemId = itemId;
        this.itemObj = itemObj;
        this.transactionId = transactionId;
        this.itemPosition = itemPosition;
        this.spriteName = spriteName;
        this.messageContents = messageContents;
        compFlg = false;
    }
}
