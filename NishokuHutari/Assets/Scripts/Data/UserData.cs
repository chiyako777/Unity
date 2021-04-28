using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public int remainTurn;
    //piblic List<ItemData> itemList;
    public bool mistakeFlg;

    public UserData(){
        remainTurn = 25 + 1;    //初期ターン（最初の遷移で一個削れるのでプラス1）
        mistakeFlg = false;
    }
}
