
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 弾幕クラス(基底)
public class BulletController : MonoBehaviour
{
    //** 弾リスト
    protected List<GameObject> bulletList = new List<GameObject>();
    //** 弾プレハブリスト
    protected List<GameObject> prefabs = new List<GameObject>();
    //** 敵機位置
    [HideInInspector]
    public Vector3 enemyLocation = new Vector3(0.0f,0.0f,0.0f);

    [HideInInspector]
    public bool activeFlg = false;

    protected int frameCount = 0;

    //** const
    protected const int vanishMotionCnt = 100; //消滅演出長さ

    protected void Start()
    {
        //Debug.Log("BulletController.Start");
        //GameObject obj = MainManager.resourcesLoader.GetObjectHandle("test_bullet");
        //GameObject obj = MainManager.resourcesLoader.GetObjectHandle("test_bullet2");
        prefabs.Add(MainManager.resourcesLoader.GetObjectHandle("test_bullet"));
        prefabs.Add(MainManager.resourcesLoader.GetObjectHandle("test_bullet2"));
    }

    protected void Update()
    {
        if(!activeFlg){ return; }
        bulletList.RemoveAll(item => item == null);     //ボムなどによる弾消しが起きているとリストが歯抜けになっているので、null要素削除
    }

    //** 弾幕オブジェクト削除
    public void DeleteBullet(){
        foreach(GameObject bullet in bulletList){
            Destroy(bullet);
        }
        bulletList.Clear();
    }

    //** 弾幕消滅演出
    public int Vanish(float motionFrame){
        if(motionFrame < vanishMotionCnt){
            //Debug.Log("Bullet Vanish 1");
            float alpha = 1.0f - (1.0f * motionFrame/(float)vanishMotionCnt);
            foreach(GameObject bullet in bulletList){
                if(bullet != null){
                    bullet.GetComponent<Bullet>().spriteRenderer.color = new Color(1.0f,1.0f,1.0f,alpha);
                }
            }
            return 1;
        }else if(motionFrame == vanishMotionCnt){
            //Debug.Log("Bullet Vanish 0");
            return 0;
        }else{
            //Debug.Log("Bullet Vanish -1");
            return -1;
        }
    }

}
