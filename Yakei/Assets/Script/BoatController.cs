using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rigidbodyコンポーネントの自動アタッチ設定
[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{

    private Rigidbody _rigidbody;   //Rigidbodyのキャッシュ
    private Transform _transform;   //Transformのキャッシュ
    
    private float moveSpeed = 1;   //移動速度
    private float moveStopSpeed = 0.005f;   //移動停止速度
    private int moveStopCount = 0;  //移動停止用カウンタ

    private float rotateSpeed = 0.3f;   //回転速度
    private float rotateStopSpeed = 0.005f;   //回転停止速度
    private int rotateStopCount = 0;  //回転停止用カウンタ


    void Start()
    {
        //各コンポーネントのキャッシュの取得（毎フレーム使用するので、負荷を下げるためにキャッシュする）
       _rigidbody = GetComponent<Rigidbody>(); 
       _transform = transform;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        //キー入力を取得(-1 ~ 1)
        float x  = Input.GetAxis("Horizontal") * moveSpeed;
        float z =  Input.GetAxis("Vertical");

        if(x != 0){
            //yを軸として左右に回転
            _rigidbody.AddTorque(new Vector3(0,x * rotateSpeed,0),ForceMode.Force);
            rotateStopCount = 0;
        }else{
            //回転を停止
            if(rotateStopCount <= 1/rotateStopSpeed){
                //Debug.Log("回転停止中");
                rotateStopCount++;
                _rigidbody.angularVelocity = Vector3.Lerp(_rigidbody.angularVelocity,Vector3.zero,rotateStopCount*rotateStopSpeed);
            }
        }

        //移動
        Vector3 moveVelocity = Vector3.zero;
        if(z > 0 && z != 0){
            moveVelocity = _transform.forward;
            _rigidbody.velocity = moveVelocity;
            moveStopCount = 0;
        }else if(z < 0 && z != 0){
            moveVelocity = _transform.forward*-1;
            _rigidbody.velocity = moveVelocity;
            moveStopCount = 0;
        }else{
            //移動停止
            if(moveStopCount <= 1/moveStopSpeed){
                //Debug.Log("移動停止中");
                moveStopCount++;
                _rigidbody.velocity = Vector3.Lerp(moveVelocity,Vector3.zero,moveStopCount*moveStopSpeed);
            }
        }


    }
}
