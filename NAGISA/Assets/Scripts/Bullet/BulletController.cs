using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 弾幕クラス(基底)
public class BulletController : MonoBehaviour
{
    //** 弾リスト
    protected List<Bullet> bulletList = new List<Bullet>();

    protected bool activeFlg = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(!activeFlg){ return; }
        Debug.Log("BulletController.Update");
    }
}
