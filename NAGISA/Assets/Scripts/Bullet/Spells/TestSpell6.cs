    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** スペカ実装指針
//   BulletController継承必須

public class TestSpell6 : BulletController
{
    private RadientLaser laser;

    private int[] statusCount = new int[3];

    void Start()
    {
        base.Start();
        statusCount[0]++;
    }

    void Update()
    {
        base.Update();
        if(!activeFlg){ return; }
        if(stopFlg){return;}

        //ステータスカウント（基本、値が入っているインデックスをカウントアップする）
        if(statusCount[0] > 0){
            statusCount[0]++;
        }else if(statusCount[1] > 0){
            statusCount[1]++;
        }else if(statusCount[2] > 0){
            statusCount[2]++;
        }

        //生成
        if(statusCount[0] > 0){    
            if(statusCount[0] == 50){
                //Debug.Log("生成");
                statusCount[0] = 0;
                statusCount[1]++;
                for(int i=0; i<12; i++){
                    bulletList.Add(Instantiate(prefabs[4],enemyLocation,Quaternion.identity));
                    bulletList[bulletList.Count-1].AddComponent<RadientLaser>();
                    laser = bulletList[bulletList.Count-1].GetComponent<RadientLaser>();
                    laser.initAng = 360.0f / 12.0f * i;
                    laser.speed = 0.4f;
                    laser.startPos = new Vector3(0.0f,0.0f,0.0f);
                }
            }
        }

        //曲げる
        if(statusCount[1] > 0){
            if(statusCount[1] % 20 == 0){
                //Debug.Log("曲げる");
                foreach(GameObject bullet in bulletList){
                    if(bullet != null){
                        RadientLaser laser = bullet.GetComponent<RadientLaser>();
                        laser.ExecTurn(laser.angles[laser.angles.Count-1] + 40.0f);
                    }
                }
            }
            if(statusCount[1] == 60){
                statusCount[1] = 0;
                statusCount[2]++;
            }
        }

        //消去
        if(statusCount[2] > 0){
            if(statusCount[2] == 50){
                //Debug.Log("消去");
                DeleteBullet();
                statusCount[2] = 0;
                statusCount[0]++;
            }
        }

        frameCount++;

    }
}
