using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectData
{
    public int selectId;
    public int whiteTransidionId;
    public int blackTransidionId;
    public string answer;

    public SelectData(int selectId, int whiteTransidionId, int blackTransidionId, string answer){
        this.selectId = selectId;
        this.whiteTransidionId = whiteTransidionId;
        this.blackTransidionId = blackTransidionId;
        this.answer = answer;
    }
}
