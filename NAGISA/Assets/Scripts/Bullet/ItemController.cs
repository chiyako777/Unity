using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    private float angle = -90.0f;
    private float speed = 0.1f;

    //** caches
    private GameObject player;
    private PlayerController playerController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        //** 落下
        transform.position += BulletUtility.GetDirection(angle * Mathf.Rad2Deg) * speed;

        //** 自機との距離
        float dist = Vector3.Distance(transform.position,player.transform.position);
        //Debug.Log("dist = " + dist);

        //** 自機へ引き寄せ
        if(dist <= 50.0f){
            Vector3 vec = player.transform.position - transform.position;
            angle = Mathf.Atan2(vec.y , vec.x);
            speed = 0.7f;
        }

        //** アイテムゲット
        if(dist <= 1.0f){
            GetItem();
        }

    }

    private void GetItem(){
        switch(tag){
            case "Power_Item":
                //Debug.Log("Power Item Get");
                playerController.power = Mathf.Clamp(playerController.power + 0.1f , 1.0f, 5.0f);
                Destroy(gameObject);
                break;
            case null:
                break;
        }
    }


}
