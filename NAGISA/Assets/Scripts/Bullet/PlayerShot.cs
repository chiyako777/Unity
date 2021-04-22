using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    
    protected float speed = 6.0f;
    [HideInInspector]
    public int power = 2;
    [HideInInspector]
    public string optionType;

    protected void Start()
    {
        
    }

    protected void Update()
    {
        //Debug.Log("optionType = " + optionType);
        if(optionType == "Homing" || optionType == "Reflec"){
            //** 直進ショット
            float angle = 90.0f;
            transform.position += BulletUtility.GetDirection(angle) * speed;
        }else if(optionType == "Warp"){
            //** 拡散ショット
            Quaternion q = transform.rotation;
            float angle = q.z * 100.0f;
            transform.position += BulletUtility.GetDirection(angle + 90.0f) * speed;
        }

        //** ショットの削除
        Destroy(gameObject,2);
    }

}
