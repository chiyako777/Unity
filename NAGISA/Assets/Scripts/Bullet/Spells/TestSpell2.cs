//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** スペカ実装指針
//   BulletController継承必須

//** 全方位+交差
public class TestSpell2 : BulletController
{
    private float speed1 = 1.0f;    //全方位弾のスピード
    private float speed2 = 0.8f;    //交差弾のスピード

    void Start()
    {
        base.Start();        
    }

    void Update()
    {
        base.Update();
        if(!activeFlg){ return; }
        if(stopFlg){return;}

        Random.InitState(frameCount);

        if(frameCount % 150 == 0){
            //Debug.Log("全方位弾生成");
            for(float i=0.0f; i<15.0f; i++){
                bulletList.Add(Instantiate(prefabs[0],enemyLocation,Quaternion.identity));
                bulletList[bulletList.Count-1].AddComponent<Bullet>();
                Bullet b = bulletList[bulletList.Count-1].GetComponent<Bullet>();
                b.velocity = BulletUtility.GetDirection(360.0f/15.0f * i + Random.Range(0.0f,15.0f)) * speed1;
                //b.velocity = BulletUtility.GetDirection(360.0f/15.0f * i + Random.Range(0.0f,15.0f)) * 0.7f;
                //Debug.Log("b.velocity = " + b.velocity);
                b.gravity = new Vector3(0.0f,0.1f,0.0f);
            }
        }


        // if(frameCount % 100 == 0){
        //     //Debug.Log("交差弾生成");
        //     float y = Random.Range(30.0f,40.0f);
        //     float val = ((frameCount/100)%2 == 0) ? 1.0f : -1.0f;
        //     for(float i=0.0f; i<9.0f;i++){
        //         //左から来る弾
        //         bulletList.Add(Instantiate(prefabs[0],new Vector3(-234.0f,Mathf.Clamp(-60.0f + (y*i),-184.0f,184.0f),0.0f),Quaternion.identity));
        //         bulletList[bulletList.Count-1].AddComponent<Bullet>();
        //         Bullet bleft = bulletList[bulletList.Count-1].GetComponent<Bullet>();
        //         bleft.velocity = BulletUtility.GetDirection(-45.0f + (Random.Range(0.0f,15.0f) * val)) * speed2;
        //         bleft.gravity = new Vector3(0.0f,0.0f,0.0f);
        //         //右から来る弾
        //         bulletList.Add(Instantiate(prefabs[0],new Vector3(139.0f,Mathf.Clamp(-60.0f + (y*i),-184.0f,184.0f),0.0f),Quaternion.identity));
        //         bulletList[bulletList.Count-1].AddComponent<Bullet>();
        //         Bullet bright = bulletList[bulletList.Count-1].GetComponent<Bullet>();
        //         bright.velocity = BulletUtility.GetDirection(-135.0f + (Random.Range(0.0f,15.0f) * val)) * speed2;
        //         bright.gravity = new Vector3(0.0f,0.0f,0.0f);
        //     }
        // }

        frameCount++;

    }
}
