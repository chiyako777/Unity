﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Vector2 velocity;   //自機の移動量
    private float recepSqrt2;    // 1 / √2  (ナナメ移動時速度調整)
    private int mutekiTime = 0;     //被弾後無敵時間
    private int bombTime = 0;       //ボム時間    

    private Dictionary<string,int> InputArray;  //各種入力制御
    public GameObject[] shotObjs;   //自機ショット用オブジェクト（Inspectorでプレハブを指定）

    //** caches
    private GameObject enemy;

    //** const
    private const int maxMutekiTime = 60;
    private const int maxBombTime = 180;

    void Start()
    {
        velocity = new Vector2(0.0f,0.0f);
        recepSqrt2 = 1.0f / Mathf.Sqrt(2.0f);
        
        InputArray = new Dictionary<string,int>();
        InputArray["Shot"] = 0;
        InputArray["Bomb"] = 0;
        InputArray["Fire3"] = 0;

    }
    
    void Update()
    {
        //** 敵機をキャッシュ
        LoadEnemy();
        //** 入力情報
        CalcInput();
        //** 速度設定
        SetVeloc();

        //** 自機を動かす
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if(x < 0.0f) velocity.x *= -1.0f; else if(x == 0.0f) velocity.x = 0.0f;
        if(y < 0.0f) velocity.y *= -1.0f; else if(y == 0.0f) velocity.y = 0.0f;
        if(velocity.x != 0.0f && velocity.y != 0.0f){
            //ナナメ移動速度調整
            velocity.x *= recepSqrt2;
            velocity.y *= recepSqrt2;
        }
        transform.position = new Vector3(transform.position.x + velocity.x,transform.position.y + velocity.y , 0.0f);

        //** ショットを打つ
        if(InputArray["Shot"] > 0 && InputArray["Shot"] % 10 == 0
            && mutekiTime == 0 && bombTime == 0){
            //Debug.Log("ショットを打つ");
            Instantiate(shotObjs[0],transform.position,Quaternion.identity);
        }

        //** ボムを打つ
        if(InputArray["Bomb"] > 0 && mutekiTime == 0 && bombTime == 0){
            bombTime = 1;
        }
        bombTime = (bombTime > 0) ? ++bombTime : 0;
        if(bombTime >= maxBombTime){ bombTime = 0; }
        if(bombTime > 0 && bombTime % 10 == 0){
            Instantiate(shotObjs[1],transform.position,Quaternion.identity);
        }

        //** 無敵時間カウント
        mutekiTime = (mutekiTime > 0) ? ++mutekiTime : 0;
        if(mutekiTime >= maxMutekiTime){ mutekiTime = 0; }
    }

    void CalcInput(){
        //Shot:Zキー:ショット
        //Fire3:左シフト:低速移動
        string[] str = { "Shot", "Bomb", "Fire3" };
        for(int i = 0; i < str.Length; ++i)
        {
            if (Input.GetButton(str[i]))
            {
                //Debug.Log("押されてるキー：" + str[i]);
                ++InputArray[str[i]];
            }
            else
            {
                InputArray[str[i]] = 0;
            }
        }
    }

    void LoadEnemy(){
        if(enemy == null){
            enemy = GameObject.FindGameObjectWithTag("Enemy_Bullet");
            //Debug.Log("LoadEnemy Enemy Cache: " + enemy);
        }
    }

    //** 速度設定
    void SetVeloc(){
        if(InputArray["Fire3"] > 0){
            //低速移動
            velocity.x = 1.0f;
            velocity.y = 1.0f;
        }else{
            //高速移動
            velocity.x = 2.0f;
            velocity.y = 2.0f;
        }
        if(bombTime > 0){
            //ボム時は自機移動速度低下
            velocity.x = 0.5f;
            velocity.y = 0.5f;
        }
    }

    //** 被弾処理
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Bullet" && mutekiTime==0 && bombTime == 0){
            Debug.Log("自機被弾");
            //無敵時間カウント開始
            mutekiTime = 1;
            //弾幕消去
            enemy.GetComponent<Enemy>().bulletController.DeleteBullet();
        }
    }
    

}
