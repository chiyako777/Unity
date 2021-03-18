
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//** 敵機基底クラス
public class Enemy : MonoBehaviour
{

    protected EnemyInfo enemyInfo;
    protected bool bulletDischarge = false;     //弾幕発射フラグ(登場演出、スペルカットイン終了後にtrueになるイメージ)
    protected int maxLife;      //敵総体力

    protected void Start()
    {
        //Debug.Log("Start : 敵生成");
        maxLife = enemyInfo.life;
        Debug.Log("maxLife = " + maxLife);
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
            enemyInfo.lifeGage.GetComponent<Image>().fillAmount = 1.0f/(float)maxLife * (float)enemyInfo.life;
            if(enemyInfo.life < 0){
                //Debug.Log("敵機デストロイ");
                Destroy(gameObject);
                Destroy(enemyInfo.lifeGage);
            }
            Destroy(collision.gameObject);
        }

    }
}
