using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHomingShot : PlayerShot
{

    [HideInInspector]
    public bool lr;    //true:Left false:Right

    //** caches
    private GameObject enemy;

    //** const
    private const float turnValue = 5.0f;

    void Start()
    {
        base.Start();
        speed = 4.0f;
        power = 1;
    }

    void Update()
    {
        //** 敵機をキャッシュ
        LoadEnemy();

        //** 自機と敵機の距離計算
        Vector3 dist = CalcDistance();
        //Debug.Log("敵機-自機距離：" + dist.magnitude);
        float angle = Mathf.Atan2(dist.y,dist.x);
        float turnAngle = dist.magnitude / turnValue * Mathf.Deg2Rad;

        //** 軌道算出
        if(lr){
            transform.position += BulletUtility.GetDirection( (angle + turnAngle) * Mathf.Rad2Deg) * speed; //左旋回
        }else{
            transform.position += BulletUtility.GetDirection( (angle - turnAngle) * Mathf.Rad2Deg) * speed; //右旋回
        }

        //** 画面外に出た場合削除
        if(BulletUtility.IsOut(transform.position)){
          Destroy(gameObject);  
        }

    }

    private Vector3 CalcDistance(){
        if(enemy != null){
            float x = enemy.transform.position.x - transform.position.x;
            float y = enemy.transform.position.y - transform.position.y;
            return new Vector3(x,y,0.0f);
        }else{
            //敵がいなくなってたら、決め打ちで画面左上に向けて打っとく
            float x = -235.0f - transform.position.x;
            float y = 185 - transform.position.y;
            return new Vector3(x,y,0.0f);
        }
    }

    void LoadEnemy(){
        if(enemy == null){
            enemy = GameObject.FindGameObjectWithTag("Enemy_Bullet");
        }
    }

}
