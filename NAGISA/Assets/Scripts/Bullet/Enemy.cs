using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 敵機基底クラス
public class Enemy : MonoBehaviour
{

    protected EnemyInfo enemyInfo;
    protected bool bulletDischarge = false;     //弾幕発射フラグ(登場演出、スペルカットイン終了後にtrueになるイメージ)
    
    protected void Start()
    {
        //Debug.Log("Start : 敵生成");
    }

    protected void Update()
    {
        //Debug.Log("Start : 敵update");
        //** 敵体力表示

        //** スペルカットイン

        //** 弾幕発射フラグ = true

    }

    private void OnTriggerEnter2D(Collider2D collision){
        
        if(collision.gameObject.tag == "Shot"){
            //Debug.Log("自機ショットに当たった " + "life:" + enemyInfo.life);
            enemyInfo.life -= collision.GetComponent<PlayerShot>().power;
            if(enemyInfo.life < 0){
                //Debug.Log("敵機デストロイ");
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }
}
