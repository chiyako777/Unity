using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell3 : BulletController
{

    private int status = 1;  //1:射出、2:停止、3:分散
    private float speed = 0.0f;
    private int heartCount = 0;
    private int transTime = 0;

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
        if(playerController.mutekiTime > 0) {
            status = 1;
            return;
        }
        switch(status){
            case 1:
                speed = 0.1f;
                if(frameCount % 15 == 0){
                    for (int i = 0; i < 360; i+=5) {
                        float xFormula = 16.0f * Mathf.Sin(i * Mathf.Deg2Rad) * Mathf.Sin(i * Mathf.Deg2Rad) * Mathf.Sin(i * Mathf.Deg2Rad);
                        float yFormula = 13 * Mathf.Cos(i * Mathf.Deg2Rad) - 5 * Mathf.Cos(2 * i * Mathf.Deg2Rad) - 2 * Mathf.Cos(3 * i * Mathf.Deg2Rad) - Mathf.Cos(4 * i * Mathf.Deg2Rad);
                        float x = 5 * xFormula + enemyLocation.x;
                        float y = 5 * yFormula + enemyLocation.y;
                        bulletList.Add(Instantiate(prefabs[1],new Vector3(x,y,0.0f),Quaternion.identity));
                        bulletList[bulletList.Count-1].AddComponent<Bullet>();
                        Bullet b = bulletList[bulletList.Count-1].GetComponent<Bullet>();
                        b.velocity = new Vector3(xFormula,yFormula,0.0f) * speed;
                        b.gravity = new Vector3(0.0f,0.0f,0.0f);
                    }
                    heartCount++;
                }
                if(heartCount >= 5){
                    status = 2;
                    heartCount = 0;
                    transTime = 0;
                }
                break;
            case 2:
                foreach(GameObject obj in bulletList){
                    if(obj != null){
                        Bullet b = obj.GetComponent<Bullet>();
                        b.velocity = new Vector3(0.0f,0.0f,0.0f);
                    }
                }
                transTime++;
                if(transTime > 120){
                    status = 3;
                    transTime = 0;
                }
                break;
            case 3:
                speed = 0.45f;
                //Random.InitState(frameCount);
                if(transTime == 0){
                    for(int i=0; i<bulletList.Count; i++){
                        Random.InitState(frameCount + i);
                        Bullet b = bulletList[i].GetComponent<Bullet>();
                        float angle = Random.Range(-180.0f,180.0f);
                        b.velocity = BulletUtility.GetDirection(angle) * speed;
                    }
                }
                transTime++;
                if(transTime > 300){
                    status = 1;
                    transTime = 0;
                }
                break;
        }

        frameCount++;

    }
}
