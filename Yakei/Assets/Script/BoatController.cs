using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CharactorControllerコンポーネントの自動アタッチ設定
[RequireComponent(typeof(CharacterController))]
public class BoatController : MonoBehaviour
{

    private CharacterController _characterController;   //CharacterControllerのキャッシュ
    private Transform _transform;   //Transformのキャッシュ
    [SerializeField] private float moveSpeed = 1;   //移動速度(Inspectorから設定)
    private Vector3 _moveVelocity;  //移動速度情報

    void Start()
    {
        //各コンポーネントのキャッシュの取得（毎フレーム使用するので、負荷を下げるためにキャッシュする）
       _characterController = GetComponent<CharacterController>(); 
       _transform = transform;
    }

    void Update()
    {

        //縦横方向の入力を取得(-1 ~ 1) * moveSpeed
        _moveVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
        _moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed;

        //Debug.Log("x = " + _moveVelocity.x + " z = " + _moveVelocity.z);
        //ボートの向きを変える
        transform.LookAt(_transform.position + new Vector3(_moveVelocity.x,0,_moveVelocity.z));

        //ボートを動かす
        _characterController.Move(_moveVelocity * Time.deltaTime);

    }
}
