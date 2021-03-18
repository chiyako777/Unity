using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 敵情報データ
public struct EnemyInfo
{
    public GameObject enemyObj;     //敵をInstantiateするときのコピー元として使われる意味合いが大きく、インスタンス化された後は特に使用しない？
    public GameObject lifeGage;
    public GameObject bulletController;
    public int enemyStatus;
    public Vector3 enemyLocation;
    
    public int life,graphType,waitTime,bulletPattern;
    public int bulletInterval,bulletType,bulletColor,bulletScriptType;

    public EnemyInfo(GameObject enemyObj,GameObject lifeGage,GameObject bulletController,int enemyStatus,
                        Vector3 enemyLocation,int life = 1,int graphType = 0,int waitTime = 180,
                        int bulletPattern = 0,int bulletInterval = 60,int bulletType = 0,
                        int bulletColor = 0,int bulletScriptType = 0){
        this.enemyObj = enemyObj;
        this.lifeGage = lifeGage;
        this.bulletController = bulletController;
        this.enemyStatus = enemyStatus;
        this.enemyLocation = enemyLocation;
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
