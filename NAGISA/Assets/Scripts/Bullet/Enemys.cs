using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWhite : Enemy
{
    void Start()
    {
        base.Start();
        //Debug.Log("Start : 敵white生成");
    }

    void Update()
    {
        base.Update();
        //Debug.Log("Start : 敵white:update");
    }

    public static void New(EnemyInfo enemyInfo){
        //Debug.Log("EnemyWhite.New");
        //** 敵オブジェクト
        GameObject enemyObj = Instantiate(enemyInfo.enemyObj, enemyInfo.enemyLocation, Quaternion.identity);
        enemyObj.AddComponent<EnemyWhite>().enemyInfo = enemyInfo;

        //** 敵体力ゲージ
        GameObject lifeGage = Instantiate(enemyInfo.lifeGage,new Vector3(0.0f,0.0f,0.0f),Quaternion.identity);
        
        GameObject enemyUIController = GameObject.Find("enemy_UI_controller");      //親Canvas設定
        lifeGage.transform.SetParent(enemyUIController.transform,false);

        var gagePos = RectTransformUtility.WorldToScreenPoint(Camera.main,enemyObj.transform.position);     //位置合わせ
        lifeGage.GetComponent<RectTransform>().anchoredPosition = new Vector2(gagePos.x,gagePos.y);
        
        enemyObj.AddComponent<EnemyLifeGage>();     //紐づけ
        enemyObj.GetComponent<EnemyWhite>().enemyInfo.lifeGage = lifeGage;

        //** 弾幕コントローラー
        GameObject bulletController = Instantiate(enemyInfo.bulletController,new Vector3(0.0f,0.0f,0.0f),Quaternion.identity);
        enemyObj.GetComponent<EnemyWhite>().enemyInfo.bulletController = bulletController;
        
        //⇒ここのタイミングで、Enemy.Start Enemy.Update呼ばれる
    }
}

public class EnemyLavender : Enemy
{
    // void Start()
    // {
        
    // }

    // void Update()
    // {
    //     transform.position += new Vector3(velocity.x,velocity.y,0.0f);
    // }

    // public static void New(EnemyInfo enemyInfo){
    //     GameObject enemyObj = Instantiate(enemyInfo.enemyObj,new Vector3(enemyInfo.x,enemyInfo.y,0.0f),Quaternion.identity);
    //     enemyObj.AddComponent<EnemyLavender>().enemyInfo = enemyInfo;
    // }
}
