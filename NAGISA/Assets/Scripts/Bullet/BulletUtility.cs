using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletUtility
{

    [HideInInspector]
    public static Vector2 screenTopLeft = new Vector2(-230.0f,180.0f);       //弾幕画面範囲
    public static Vector2 screenBottomRight = new Vector2(135.0f,-180.0f);   //弾幕画面範囲

    //** angleの方向に進む単位ベクトル
    public static Vector3 GetDirection(float angle){
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(
            Mathf.Cos(rad),
            Mathf.Sin(rad),
            0.0f
        );
    }

    //** 画面外に出たかの判定
    public static bool IsOut(Vector3 position){
        if(position.x < screenTopLeft.x || position.x > screenBottomRight.x){
            return true;
        }
        if(position.y < screenBottomRight.y || position.y > screenTopLeft.y){
            return true;
        }
        return false;
    }

}
