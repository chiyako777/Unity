//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell2 : BulletController
{
    void Start()
    {
        base.Start();        
    }

    void Update()
    {
        base.Update();
        //Debug.Log("BulletController.Update");

        Random.InitState(frameCount);

        if(frameCount % 150 == 0){
            //Debug.Log("全方位弾生成");
            for(float i=0.0f; i<15.0f; i++){
                bulletList.Add(Instantiate(prefabs[1],enemyLocation,Quaternion.identity));
                bulletList[bulletList.Count-1].AddComponent<Bullet>();
                Bullet b = bulletList[bulletList.Count-1].GetComponent<Bullet>();
                b.velocity = BulletUtility.GetDirection(360.0f/12.0f * i + Random.Range(0.0f,15.0f)) * 0.7f;
                b.gravity = new Vector3(0.0f,0.1f,0.0f);
            }
        }


        if(frameCount % 100 == 0){
            //Debug.Log("交差弾生成");
            float y = Random.Range(30.0f,40.0f);
            float val = ((frameCount/100)%2 == 0) ? 1.0f : -1.0f;
            for(float i=0.0f; i<9.0f;i++){
                //左から来る弾
                bulletList.Add(Instantiate(prefabs[1],new Vector3(-234.0f,Mathf.Clamp(-60.0f + (y*i),-184.0f,184.0f),0.0f),Quaternion.identity));
                bulletList[bulletList.Count-1].AddComponent<Bullet>();
                Bullet bleft = bulletList[bulletList.Count-1].GetComponent<Bullet>();
                bleft.velocity = BulletUtility.GetDirection(-45.0f + (Random.Range(0.0f,15.0f) * val)) * 0.4f;
                bleft.gravity = new Vector3(0.0f,0.0f,0.0f);
                //右から来る弾
                bulletList.Add(Instantiate(prefabs[1],new Vector3(139.0f,Mathf.Clamp(-60.0f + (y*i),-184.0f,184.0f),0.0f),Quaternion.identity));
                bulletList[bulletList.Count-1].AddComponent<Bullet>();
                Bullet bright = bulletList[bulletList.Count-1].GetComponent<Bullet>();
                bright.velocity = BulletUtility.GetDirection(-135.0f + (Random.Range(0.0f,15.0f) * val)) * 0.4f;
                bright.gravity = new Vector3(0.0f,0.0f,0.0f);
            }
        }

        frameCount++;

    }
}
