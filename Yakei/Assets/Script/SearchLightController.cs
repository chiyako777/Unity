using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//灯台のサーチライトコントローラー
public class SearchLightController : MonoBehaviour
{
    private Transform _transform;
    private float rotateSpeed = 1.0f;
    private float offsetY = 0.0f;

    void Start()
    {
        _transform = transform;
    }

    void Update()
    {
        //Y軸を回転
        offsetY += rotateSpeed;
        //Debug.Log("offsetY = " + offsetY);
        if(offsetY >= 360){
            //Debug.Log("kiki");
            offsetY = 0.0f;     
            //y軸のtransform.rotationは基本的には-180~180の値を取っている
            //（もし絶対値がそれ以上になっても自動で丸められる）
            //しかし、そのままにしておくといつか桁あふれするので、360になったら0に戻すと、ちょうど切りがよく数値がリセットされる
        }
        transform.rotation = Quaternion.Euler(140,offsetY, 0);

    }
}
