using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletUtility
{

    //** angleの方向に進む単位ベクトル
    public static Vector3 GetDirection(float angle){
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(
            Mathf.Cos(rad),
            Mathf.Sin(rad),
            0.0f
        );
    }

}
