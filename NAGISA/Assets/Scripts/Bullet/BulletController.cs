
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
    protected bool stopFlg = false;     //弾幕全停止フラグ

    //** const
    protected const int vanishMotionCnt = 100; //消滅演出長さ

    protected void Start()
    {
        //Debug.Log("BulletController.Start");
        prefabs.Add(BulletMainManager.resourcesLoader.GetObjectHandle("glossy_bubbles_marine"));
        prefabs.Add(BulletMainManager.resourcesLoader.GetObjectHandle("glossy_bubbles_pink"));
        prefabs.Add(BulletMainManager.resourcesLoader.GetObjectHandle("glossy_bubbles_lemon"));
        prefabs.Add(BulletMainManager.resourcesLoader.GetObjectHandle("Notice_Laser"));

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

    //** 弾幕全停止
    public void StopAll(){
        //Debug.Log("BulletController : StopAll");
        this.stopFlg = true;
        foreach(GameObject bullet in bulletList){
            if(bullet != null){
                if(bullet.tag == "Bullet"){
                    Bullet b = bullet.GetComponent<Bullet>();
                    b.stopFlg = true;
                }else if(bullet.tag == "Laser"){
                    //Debug.Log("レーザー停止");
                    Laser l = bullet.GetComponent<Laser>();
                    l.stopFlg = true;
                }
            }
        }
    }

    //** 弾幕全停止解除
    public void RestartAll(){
        //Debug.Log("BulletController : StopAll");
        this.stopFlg = false;
        foreach(GameObject bullet in bulletList){
            if(bullet != null){
                if(bullet.tag == "Bullet"){
                    Bullet b = bullet.GetComponent<Bullet>();
                    b.stopFlg = false;
                }else if(bullet.tag == "Laser"){
                    //Debug.Log("レーザーリスタート");
                    Laser l = bullet.GetComponent<Laser>();
                    l.stopFlg = false;
                }
            }
        }
    }

}
