
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

    protected void Start()
    {
        //Debug.Log("BulletController.Start");
        GameObject obj = MainManager.resourcesLoader.GetObjectHandle("test_bullet");
        prefabs.Add(obj);
    }

    protected void Update()
    {
        if(!activeFlg){ return; }
        bulletList.RemoveAll(item => item == null);     //ボムなどによる弾消しが起きているとリストが歯抜けになっているので、null要素削除
    }

    //** 弾幕消滅
    public void DeleteBullet(){
        foreach(GameObject bullet in bulletList){
            Destroy(bullet);
        }
        bulletList.Clear();
    }

}
