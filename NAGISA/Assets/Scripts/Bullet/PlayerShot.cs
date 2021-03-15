using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    private float speed = 3.0f;
    private int power = 1;

    void Start()
    {
        
    }

    void Update()
    {
        //** ショットの移動
        float angle = 90.0f;
        //Debug.Log("単位ベクトル：" + BulletUtility.GetDirection(angle));
        transform.position += BulletUtility.GetDirection(angle) * speed;

        //** ショットの削除
        Destroy(gameObject,2);
    }

}
