using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** forwardの動きにもっと躍動感を付けたい

public class UsagiController : MonoBehaviour
{

    [HideInInspector]
    public float index = 0.0f;         //前方に1進むたびにカウントアップ
    private string moveFlg = "";        //forward,left,right,unset
    private int collisionCount = 0;

    private Vector3 input = new Vector3(0.0f,0.0f,0.0f);
    private Vector3 startPos = new Vector3(0.0f,0.0f,0.0f);
    private Vector3 nowPos = new Vector3(0.0f,0.0f,0.0f);

    private const float speed = 25.0f;
    private const int maxCollisionCount = 40;

    //** chache
    private Rigidbody rigidbody;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        //** 位置ずれ合わせ
        if(moveFlg == ""){
            transform.position = new Vector3(0.0f,-4.0f,index);
            transform.rotation = Quaternion.identity;
        }

        //** 移動方向判定
        if(Input.GetMouseButtonDown(0) && moveFlg == ""){
            //Debug.Log("tap down");
            //タップダウン直後：方向未確定
            startPos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0.0f);
            //Debug.Log("startPos = " + startPos);
            moveFlg = "unset";
        }
        if(Input.GetMouseButton(0)){
            //Debug.Log("tap now");
            //タップ中：方向判定
            nowPos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0.0f);
            if(moveFlg != "left" && moveFlg != "right"){
                if(JudgeSwipeDirection()){
                    float x = (moveFlg == "left") ? -1 : 1;
                    rigidbody.AddForce(new Vector3(0.7f * x,0.3f,0) * speed,ForceMode.Impulse);
                }
            }
        }
        if(Input.GetMouseButtonUp(0) && moveFlg == "unset"){
            //離した時にスワイプでない：通常タップ
            Debug.Log("tap up forward");
            moveFlg = "forward";
            index++;
            rigidbody.AddForce(new Vector3(0,0.3f,0.1f) * speed,ForceMode.Impulse);
        }

        //** 動作制御
        if(transform.position.z >= index && moveFlg == "forward"){
            //前方進行停止(多分ノード終了時も強制的に戻すと思う)
            Debug.Log("forward停止");
            moveFlg = "";
        }
        if(transform.position.x <= -2 && moveFlg == "left"){
            //左方進行停止(多分ノード終了時も強制的に戻すと思う)
            rigidbody.velocity = Vector3.zero;
            if(!Input.GetMouseButton(0)){
                Debug.Log("left停止");
                moveFlg = "";
            }
        }
        if(transform.position.x >= 2 && moveFlg == "right"){
            //右方進行停止(多分ノード終了時も強制的に戻すと思う)
            rigidbody.velocity = Vector3.zero;
            if(!Input.GetMouseButton(0)){
                Debug.Log("right停止");
                moveFlg = "";
            }
        }

    }

    void FixedUpdate(){

        if(moveFlg == "forward"){
            //Debug.Log("力を加える : " + transform.position.z);
            rigidbody.AddForce(new Vector3(0,-3.0f,1.5f) * speed,ForceMode.Force);
        }else if(moveFlg == "left"){

        }else if(moveFlg == "right"){

        }else{
            rigidbody.velocity = Vector3.zero;
        }

    }

    //** スワイプ方向判定
    private bool JudgeSwipeDirection(){
        Debug.Log("JudgeSwipeDirection");
        if(moveFlg == "left" || moveFlg == "right"){ return false; }

        float dX = Mathf.Abs(nowPos.x - startPos.x);

        if(dX > 50 && nowPos.x < startPos.x){
            //Debug.Log("left");
            moveFlg = "left";
            return true;
        }else if(dX > 50 && nowPos.x > startPos.x){
            //Debug.Log("right");
            moveFlg = "right";
            return true;
        }

        return false;

    }

}
