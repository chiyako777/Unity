using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public int itemId;
    public GameObject itemObj;
    public int transactionId;
    public Vector3 itemPosition;
    public bool compFlg;

    public ItemData(int itemId,GameObject itemObj,int transactionId,Vector3 itemPosition){
        this.itemId = itemId;
        this.itemObj = itemObj;
        this.transactionId = transactionId;
        this.itemPosition = itemPosition;
        compFlg = false;
    }
}
