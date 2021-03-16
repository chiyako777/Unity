using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 3.0f;
    private float angle = 0.0f;

    void Start()
    {        
    }

    public void SetParam(float speed,float angle){
        this.speed = speed;
        this.angle = angle;
    }


    void Update()
    {
        //** 暫定で単純進行弾
        transform.position += BulletUtility.GetDirection(angle) * speed;

    }
}
