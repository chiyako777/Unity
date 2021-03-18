﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//** 敵機基底クラス
public class Enemy : MonoBehaviour
{

    protected EnemyInfo enemyInfo;

    protected int maxLife;      //敵総体力

    //** caches
    protected Image lifeGageImage;  //敵体力ゲージ
    protected BulletController bulletController;    //弾幕コントローラー

    //** const
    protected const int lifeDispCnt = 60; //敵体力ゲージ表示演出時のフレームカウント
    protected const int enemyStatusInit = 0;        //敵ステータス：初期状態
    protected const int enemyStatusGageDisp = 1;    //敵ステータス：体力ゲージ表示演出
    protected const int enemyStatusCutIn = 2;       //敵ステータス：スペルカットイン演出
    protected const int enemyStatusBullet = 3;      //敵ステータス：戦闘時

    protected void Start()
    {
        //Debug.Log("Start : 敵生成");
        maxLife = enemyInfo.life;
        lifeGageImage = enemyInfo.lifeGage.GetComponent<Image>();
        bulletController = enemyInfo.bulletController.GetComponent<BulletController>();
    }

    protected void Update()
    {
        //Debug.Log("Start : 敵update");

        //** 情報同期
        bulletController.enemyLocation.x = enemyInfo.enemyLocation.x;
        bulletController.enemyLocation.y = enemyInfo.enemyLocation.y;
        bulletController.enemyLocation.z = enemyInfo.enemyLocation.z;

        switch(enemyInfo.enemyStatus){
            case 0:
                enemyInfo.enemyStatus = 1;
                break;
            case 1:
                //** 敵体力表示
                lifeGageImage.fillAmount = Mathf.Clamp(lifeGageImage.fillAmount + (1.0f/(float)lifeDispCnt),0.0f,1.0f);
                if(lifeGageImage.fillAmount >= 1.0f){
                    enemyInfo.enemyStatus = 2;
                }
                break;
            case 2:
                //** スペルカットイン
                enemyInfo.enemyStatus = 3;
                break;
            case 3:
                //** 弾幕アクティブ化
                bulletController.activeFlg = true;
                break;
            default:
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision){
        
        if(collision.gameObject.tag == "Shot" && bulletController.activeFlg){
            //Debug.Log("自機ショットに当たった " + "life:" + enemyInfo.life);
            enemyInfo.life -= collision.GetComponent<PlayerShot>().power;
            lifeGageImage.fillAmount = 1.0f/(float)maxLife * (float)enemyInfo.life;
            if(enemyInfo.life < 0){
                //Debug.Log("敵機デストロイ");
                Destroy(gameObject);
                Destroy(enemyInfo.lifeGage);
                Destroy(enemyInfo.bulletController);

            }
            Destroy(collision.gameObject);
        }

    }
}
