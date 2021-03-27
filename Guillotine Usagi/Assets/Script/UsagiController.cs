using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsagiController : MonoBehaviour
{

    private float index = 0.0f;
    private bool moveFlg = false;
    private float speed = 25.0f;
    private Vector3 input = new Vector3(0.0f,0.0f,0.0f);

    //** chache
    private Rigidbody rigidbody;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        //** 位置ずれ合わせ
        if(!moveFlg){
            transform.position = new Vector3(transform.position.x,transform.position.y,index);
        }

        //** タップでZ方向に 1 進む + ジャンプ
        if(Input.GetMouseButtonDown(0) && !moveFlg){
            index++;
            moveFlg = true;
            rigidbody.AddForce(new Vector3(0,5,1) * speed);
            Debug.Log("タップ or 左クリック : index = " + index);
        }

        if(transform.position.z >= index && moveFlg){
            Debug.Log("停止");
            moveFlg = false;
        }

    }

    void FixedUpdate(){

        if(moveFlg){
            //Debug.Log("力を加える : " + transform.position.z);
            rigidbody.AddForce(new Vector3(0,-0.2f,1) * speed);
        }else{
            rigidbody.velocity = Vector3.zero;
        }

    }

}
