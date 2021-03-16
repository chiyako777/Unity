using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 敵機基底クラス
public class Enemy : MonoBehaviour
{

    protected EnemyInfo enemyInfo;
    protected bool bulletDischarge = false;     //弾幕発射フラグ
    
    void Start()
    {
    }

    void Update()
    {        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        
        if(collision.gameObject.tag == "Shot"){
            Debug.Log("自機ショットに当たった " + "life:" + enemyInfo.life);
            enemyInfo.life -= collision.GetComponent<PlayerShot>().power;
            if(enemyInfo.life < 0){
                Debug.Log("敵機デストロイ");
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }
}
