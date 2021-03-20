using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : BulletController
{

    void Start()
    {
        base.Start();        
    }

    void Update()
    {
        base.Update();
        //Debug.Log("BulletController.Update");

        if(frameCount % 150 == 0){
            //Debug.Log("弾幕生成");
            for(float i=0.0f; i<12.0f; i++){
                bulletList.Add(Instantiate(prefabs[0],enemyLocation,Quaternion.identity));
                bulletList[bulletList.Count-1].AddComponent<Bullet>();
                Bullet b = bulletList[bulletList.Count-1].GetComponent<Bullet>();
                b.velocity = BulletUtility.GetDirection(360.0f/12.0f * i);
                //Debug.Log("velocity = " + b.velocity);
                b.gravity = new Vector3(0.0f,0.0f,0.0f);
            }
        }
        frameCount++;

    }
}
