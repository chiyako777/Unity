using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float Speed = 3.0f;
    private float Angle = 0.0f;

    void Start()
    {        
    }

    public void SetParam(float speed,float angle){
        Speed = speed;
        Angle = angle;
    }


    void Update()
    {
        
    }
}
