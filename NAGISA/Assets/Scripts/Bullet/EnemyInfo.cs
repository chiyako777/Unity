using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 敵情報データ
public struct EnemyInfo
{
    public GameObject enemyObj;
    public float x,y;
    public int life,graphType,waitTime,bulletPattern;
    public int bulletInterval,bulletType,bulletColor,bulletScriptType;

    public EnemyInfo(GameObject enemyObj,float x = 0.0f,float y = 0.0f,int life = 1,int graphType = 0,int waitTime = 180,
                        int bulletPattern = 0,int bulletInterval = 60,int bulletType = 0,
                        int bulletColor = 0,int bulletScriptType = 0){
        this.enemyObj = enemyObj;
        this.x = x;
        this.y = y;
        this.life = life;
        this.graphType = graphType;
        this.waitTime = waitTime;

        //** 弾幕系情報（整理中）
        //こっちに持つのはスペル名情報くらいにして、個別ロジックにガンガン書く方がよさそうだな・・
        this.bulletPattern = bulletPattern;
        this.bulletInterval = bulletInterval;
        this.bulletType = bulletType;
        this.bulletColor = bulletColor;
        this.bulletScriptType = bulletScriptType;
    }


}
