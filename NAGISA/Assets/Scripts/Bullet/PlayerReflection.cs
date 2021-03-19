using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReflection : MonoBehaviour
{

    //** cache
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //上
        if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")>0.0f){
            transform.position = new Vector3(player.transform.position.x,player.transform.position.y + 15.0f,0.0f);
            transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
        }

        //下
        if(Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical")<0.0f){
            transform.position = new Vector3(player.transform.position.x,player.transform.position.y - 15.0f,0.0f);
            transform.rotation = Quaternion.Euler(0.0f,0.0f,180.0f);
        }

        //右
        if(Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal")>0.0f){
            transform.position = new Vector3(player.transform.position.x + 15.0f,player.transform.position.y,0.0f);
            transform.rotation = Quaternion.Euler(0.0f,0.0f,-90.0f);
        }

        //左
        if(Input.GetButtonDown("Horizontal") && Input.GetAxis("Horizontal")<0.0f){
            transform.position = new Vector3(player.transform.position.x - 15.0f,player.transform.position.y,0.0f);
            transform.rotation = Quaternion.Euler(0.0f,0.0f,90.0f);
        }

    }
}
