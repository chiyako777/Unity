using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHomingShot : PlayerShot
{

    [HideInInspector]
    public Vector3 enemyPos = new Vector3(0.0f,0.0f,0.0f);

    //** caches
    private GameObject enemy;

    void Start()
    {
        base.Start();
        speed = 2.0f;
        power = 1;
    }

    void Update()
    {
        //** 敵機をキャッシュ
        LoadEnemy();

        //** 自機と敵機の距離計算
        Vector3 dist = CalcDistance();
        float angle = Mathf.Atan2(dist.y,dist.x);

        //** 軌道算出
        transform.position += BulletUtility.GetDirection(angle * Mathf.Rad2Deg) * speed;

        //** (仮)削除
        Destroy(gameObject,2);

    }

    private Vector3 CalcDistance(){
        float x = enemy.transform.position.x - transform.position.x;
        float y = enemy.transform.position.y - transform.position.y;
        return new Vector3(x,y,0.0f);
    }

    void LoadEnemy(){
        if(enemy == null){
            enemy = GameObject.FindGameObjectWithTag("Enemy_Bullet");
        }
    }

}
