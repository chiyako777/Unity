
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

    private int frameCount = 0;

    void Start()
    {
        //Debug.Log("BulletController.Start");
        GameObject obj = MainManager.resourcesLoader.GetObjectHandle("test_bullet");
        prefabs.Add(obj);
    }

    void Update()
    {
        if(!activeFlg){ return; }
        //Debug.Log("BulletController.Update");

        if(frameCount % 150 == 0){
            //Debug.Log("弾幕生成");
            bulletList.Add(Instantiate(prefabs[0],enemyLocation,Quaternion.identity));
            bulletList[bulletList.Count-1].AddComponent<Bullet>();
            Bullet b = bulletList[bulletList.Count-1].GetComponent<Bullet>();
            b.velocity = new Vector3(0.1f,-0.1f,0.0f);
            b.gravity = new Vector3(0.0f,0.0f,0.0f);
        }
        frameCount++;
    }

}
