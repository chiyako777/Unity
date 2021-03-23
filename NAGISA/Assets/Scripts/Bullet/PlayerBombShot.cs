using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** 要改良
public class PlayerBombShot : PlayerShot
{
    [HideInInspector]
    public string type;     //center,left,right

    void Start()
    {
        base.Start();  
        power = 8; 
    }

    void Update()
    {
        //base.Update();
        float angle = 0.0f;
        switch(type){
            case "center":
                angle = 90.0f;
                break;
            case "left":
                angle = 97.0f;
                break;
            case "right":
                angle = 82.0f;
                break;
        }
        transform.position += BulletUtility.GetDirection(angle) * speed;
        Destroy(gameObject,2);
    }
}
