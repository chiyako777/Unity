using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharaTip_Controller : MonoBehaviour
{

    private float speed = 2.0f;

    //ゲームオブジェクト生成時に呼ばれる
    void Start()
    {
        //初期位置
        //this.gameObject.transform.position = new Vector3(8.7f,-4.6f,0.0f);   
    }

    // 毎フレーム呼ばれる
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow)){
            Vector3 temp = this.gameObject.transform.localPosition + new Vector3(0.0f,0.1f,0.0f) * speed * Time.deltaTime;
            temp.y = Mathf.Clamp(temp.y,-0.457f,0.457f);
            this.gameObject.transform.localPosition = temp;
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            Vector3 temp = this.gameObject.transform.localPosition - new Vector3(0.0f,0.1f,0.0f) * speed * Time.deltaTime;
            temp.y = Mathf.Clamp(temp.y,-0.457f,0.457f);
            this.gameObject.transform.localPosition = temp;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            Vector3 temp = this.gameObject.transform.localPosition + new Vector3(0.1f,0.0f,0.0f) * speed * Time.deltaTime;
            temp.x = Mathf.Clamp(temp.x,-0.468f,0.468f);
            this.gameObject.transform.localPosition = temp;
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            Vector3 temp = this.gameObject.transform.localPosition - new Vector3(0.1f,0.0f,0.0f) * speed * Time.deltaTime;
            temp.x = Mathf.Clamp(temp.x,-0.468f,0.468f);
            this.gameObject.transform.localPosition = temp;
        }
    }
}
