using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** スペカ実装指針
//   BulletController継承必須

public class TestSpell5 : BulletController
{
    private bool flg = false;
    private Laser laser;

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();

        if(!flg){
            bulletList.Add(Instantiate(prefabs[3],enemyLocation,Quaternion.identity));
            bulletList[bulletList.Count-1].AddComponent<Laser>();
            laser = bulletList[bulletList.Count-1].GetComponent<Laser>();
            laser.angle = -45.0f;
            laser.length = 300.0f;
            laser.startPos = new Vector3(0.0f,0.0f,0.0f);
            
            flg = true;
        }

        if(frameCount > 400 && laser.status == 1){
            //Debug.Log("ステータス：拡大へ");
            laser.status = 2;
        }

        if(laser.status == 3 && frameCount % 800 == 0){
            //Debug.Log("ステータス：縮小へ");
            laser.status = 4;
        }

        if(flg){
            frameCount++;
        }

    }
}
