using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsagiControllerOld : MonoBehaviour
{

    private Rigidbody rigidbody;
    private Animator animator;

    private Vector2 inputAxis;
    private float speed = 100.0f;

    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.animator = GetComponent<Animator>();
        inputAxis = new Vector2(0.0f,0.0f);
    }

    void Update()
    {
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");            

    }

    void FixedUpdate(){

        //** 移動
        rigidbody.AddForce(new Vector3(inputAxis.x,0.0f,inputAxis.y) * speed,ForceMode.Force);

        //** 回転(瞬時に向き回転、Vertical優先)
        if(inputAxis.x > 0){
            rigidbody.rotation = Quaternion.AngleAxis(90.0f,Vector3.up);
        }else if(inputAxis.x < 0){
            rigidbody.rotation = Quaternion.AngleAxis(-90.0f,Vector3.up);
        }

        if(inputAxis.y > 0){
            rigidbody.rotation = Quaternion.AngleAxis(0.0f,Vector3.up);
        }else if(inputAxis.y < 0){
            rigidbody.rotation = Quaternion.AngleAxis(180.0f,Vector3.up);
        }

        //** アニメーション
        if(inputAxis.x != 0.0f || inputAxis.y != 0.0f){
            //Debug.Log("走れ！");
            this.animator.SetInteger("AnimIndex",1);
            this.animator.SetTrigger("Next");
        }else{
            //Debug.Log("止まれ！");
            this.animator.SetInteger("AnimIndex",0);
            this.animator.SetTrigger("Next");
        }
        

    }

    void OnCollisionEnter(Collision collision){
    }
}
