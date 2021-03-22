using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 敵情報データ
public struct EnemyInfo
{
    public GameObject enemyObj;     //敵をInstantiateするときのコピー元として使われる意味合いが大きく、インスタンス化された後は特に使用しない？
    public GameObject lifeGage;
    public GameObject bulletController;
    public string spellName;
    public GameObject defeatEffect;
    public int enemyStatus;
    public Vector3 enemyLocation;
    public float life;

    //↓ 以下は現状不使用
    public int graphType,waitTime,bulletPattern;
    public int bulletInterval,bulletType,bulletColor,bulletScriptType;

    public EnemyInfo(GameObject enemyObj,GameObject lifeGage,GameObject bulletController,string spellName,
                        GameObject defeatEffect,int enemyStatus,Vector3 enemyLocation,float life = 1.0f,
                        int graphType = 0,int waitTime = 180,
                        int bulletPattern = 0,int bulletInterval = 60,int bulletType = 0,
                        int bulletColor = 0,int bulletScriptType = 0){
        this.enemyObj = enemyObj;
        this.lifeGage = lifeGage;
        this.bulletController = bulletController;
        this.spellName = spellName;
        this.defeatEffect = defeatEffect;
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
