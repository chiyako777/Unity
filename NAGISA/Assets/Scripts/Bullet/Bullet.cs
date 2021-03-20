using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Vector3 velocity = new Vector3(0.0f,0.0f,0.0f);
    [HideInInspector]
    public Vector3 gravity = new Vector3(0.0f,1.0f,0.0f);

    //** cache
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    void Start()
    {        
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position += velocity + gravity;

        //** 画面Outによる弾消し
        if(BulletUtility.IsOut(transform.position)){
            //Debug.Log("画面outによる弾消し");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        //** ボム・リフレクによる弾消し
        if(collision.gameObject.tag == "Bomb_Shot" || collision.gameObject.tag == "Reflec_Shot"){
            //Debug.Log("ボムによる弾消し");
            Destroy(gameObject);
        }
    }
}

