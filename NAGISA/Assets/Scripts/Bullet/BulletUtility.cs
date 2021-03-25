using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletUtility
{

    [HideInInspector]
    public static Vector2 screenTopLeftJust = new Vector2(-229.0f,175.0f);
    [HideInInspector]
    public static Vector2 screenBottomRightJust = new Vector2(129.0f,-168.0f);
    [HideInInspector]
    public static Vector3 centerPos = new Vector3(-50.0f,3.5f,0.0f);

    //弾幕画面範囲(実際の見た目より広め)
    [HideInInspector]
    public static Vector2 screenTopLeft = new Vector2(-235.0f,185.0f);
    [HideInInspector]
    public static Vector2 screenBottomRight = new Vector2(140.0f,-185.0f);

    //** angleの方向に進む単位ベクトル
    // angle ⇒ degree (-180 ~ 180)
    public static Vector3 GetDirection(float angle){
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(
            Mathf.Cos(rad),
            Mathf.Sin(rad),
            0.0f
        );
    }

    //** 2点間の角度(degree)
    public static float CalcBetween(Vector3 from,Vector3 to){
        Vector3 v = to - from;
        float rad = Mathf.Atan2(v.y,v.x);
        return rad * Mathf.Rad2Deg;

    }

    //** 画面外に出たかの判定
    public static bool IsOut(Vector3 position){
        if(position.x < screenTopLeft.x || position.x > screenBottomRight.x){
            //Debug.Log("X Out");
            return true;
        }
        if(position.y < screenBottomRight.y || position.y > screenTopLeft.y){
            //Debug.Log("Y Out");
            return true;
        }
        return false;
    }

    
}
