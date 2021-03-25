using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** スペカ実装指針
//   BulletController継承必須

public class TestSpell4 : BulletController
{

    private float speed = 0.0f;
    private int status = 1;  //1:位置決め、2:発射、3:分散
    private int term = 0;
    private int maxTerm = 8;
    private int dCount = 0;

    private Vector3 playerPos = new Vector3(0.0f,0.0f,0.0f);    //自機位置のキャッシュ（大玉発射用）
    private GameObject[] initialBullet = new GameObject[2];
    private bool[] initialBulletStopFlg = new bool[2];

    //** cache
    private PlayerController playerController;

    void Start()
    {
        base.Start();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
    }

    void Update()
    {
        base.Update(); 
        if(!activeFlg){ return; }
        if(stopFlg){return;}

        if(playerController.mutekiTime > 0) {
            status = 1;
            return;
        }

        switch(status){
            case 1 :
                //** 自機位置を判定
                if(frameCount % 60 == 0){
                    playerPos.x = playerController.transform.position.x;
                    playerPos.y = playerController.transform.position.y;
                    status = 2;
                }
                break;
            case 2 :
                //** 大玉を発射
                speed = 4.0f;
                if(initialBullet[0] == null && initialBullet[1] == null){
                    //term = (term==1) ? 2 : 1;
                    term++;
                    if(term > maxTerm){ term = 1; }
                    initialBulletStopFlg[0] = false; initialBulletStopFlg[1] = false;
                    if(term % 2 == 1){
                        //1way
                        initialBullet[0] = Instantiate(prefabs[2],enemyLocation,Quaternion.identity);
                        bulletList.Add(initialBullet[0]);
                        initialBullet[0].transform.localScale = new Vector3(13.0f,13.0f,1.0f);
                        initialBullet[0].AddComponent<Bullet>();
                        Bullet b = initialBullet[0].GetComponent<Bullet>();
                        b.velocity = BulletUtility.GetDirection(BulletUtility.CalcBetween(enemyLocation,playerPos) + (float)term) * speed;
                        b.gravity = new Vector3(0.0f,0.0f,0.0f);
                    }else if(term % 2 == 0){
                        //2way
                        for(int i=0; i<2; i++){
                            initialBullet[i] = Instantiate(prefabs[2],enemyLocation,Quaternion.identity);
                            bulletList.Add(initialBullet[i]);
                            initialBullet[i].transform.localScale = new Vector3(13.0f,13.0f,1.0f);
                            initialBullet[i].AddComponent<Bullet>();
                            Bullet b = initialBullet[i].GetComponent<Bullet>();
                            b.velocity = BulletUtility.GetDirection(BulletUtility.CalcBetween(enemyLocation,playerPos) + ((i*2-1) * 10.0f) + (float)term) * speed;
                            b.gravity = new Vector3(0.0f,0.0f,0.0f);
                        }
                    }
                }else{
                    //画面端まで行ったら停止する
                    float left,right,top,bottom;
                    if(term%2 == 0){
                        left = BulletUtility.screenTopLeftJust.x;
                        right = BulletUtility.screenBottomRightJust.x;
                        top = BulletUtility.screenTopLeftJust.y;
                        bottom = BulletUtility.screenBottomRightJust.y;
                    }else{
                        left = BulletUtility.screenTopLeftJust.x + 50.0f;
                        right = BulletUtility.screenBottomRightJust.x - 50.0f;
                        top = BulletUtility.screenTopLeftJust.y - 50.0f;
                        bottom = BulletUtility.screenBottomRightJust.y + 50.0f;
                    }

                    for(int i=0; i<initialBullet.Length; i++){    
                        if(initialBullet[i] != null){
                            if(initialBullet[i].transform.position.x < left 
                            || initialBullet[i].transform.position.x > right
                            || initialBullet[i].transform.position.y < bottom
                            || initialBullet[i].transform.position.y > top){

                                Bullet b = initialBullet[i].GetComponent<Bullet>();
                                b.velocity = new Vector3(0.0f,0.0f,0.0f);
                                initialBulletStopFlg[i] = true;
                            }
                        }else{
                            initialBulletStopFlg[i] = true;
                        }
                    }

                    if(initialBulletStopFlg[0] && initialBulletStopFlg[1]){
                        status = 3;
                        dCount = 0;
                    }

                }
                break;
            case 3 :
                //** 着地した大玉から分散
                foreach(GameObject initBullet in initialBullet){

                    if(frameCount % 4 == 0){
                        if(initBullet != null){
                            for(float i = 0.0f; i <= 6.28f; i += 0.5f){
                                bulletList.Add(Instantiate(prefabs[2],initBullet.transform.position,Quaternion.identity));
                                bulletList[bulletList.Count-1].AddComponent<Bullet>();
                                Bullet b = bulletList[bulletList.Count-1].GetComponent<Bullet>();
                                b.velocity = BulletUtility.GetDirection(BulletUtility.CalcBetween(initBullet.transform.position,playerController.transform.position) + Mathf.Sin(i)) * speed;
                                b.gravity = new Vector3(0.0f,0.0f,0.0f);
                            }
                        }
                    }

                    if(frameCount % 5 == 0){
                        if(initBullet != null){
                            for(float i = -30.0f; i <= 35.0f; i += 5.0f){
                                bulletList.Add(Instantiate(prefabs[2],initBullet.transform.position,Quaternion.identity));
                                bulletList[bulletList.Count-1].AddComponent<Bullet>();
                                Bullet b = bulletList[bulletList.Count-1].GetComponent<Bullet>();
                                b.velocity = BulletUtility.GetDirection(BulletUtility.CalcBetween(initBullet.transform.position,enemyLocation) + i) * speed;
                                b.gravity = new Vector3(0.0f,-0.3f * (float)term,0.0f);
                            }
                        }
                        dCount++;
                    }


                }
                if(dCount > 10){
                    status = 1;
                    foreach(GameObject initBullet in initialBullet){
                        if(initBullet != null) {Destroy(initBullet);}
                    }
                }

                break;

            default :
                break;
        }

        frameCount++;
    }

}
