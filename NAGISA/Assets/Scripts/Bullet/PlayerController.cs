using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Vector2 velocity;   //自機の移動量
    private float recepSqrt2;    // 1 / √2  (ナナメ移動時速度調整)
    private Dictionary<string,int> InputArray;  //各種入力制御

    public GameObject[] shotObjs;   //自機ショット用オブジェクト（Inspectorでプレハブを指定）

    void Start()
    {
        velocity = new Vector2(0.0f,0.0f);
        recepSqrt2 = 1.0f / Mathf.Sqrt(2.0f);
        
        InputArray = new Dictionary<string,int>();
        InputArray["Shot"] = 0;
        InputArray["Fire2"] = 0;
        InputArray["Fire3"] = 0;
    }
    
    void Update()
    {
        CalcInput();

        //** 速度設定
        if(InputArray["Fire3"] > 0){
            //低速移動
            velocity.x = 1.0f;
            velocity.y = 1.0f;
        }else{
            //高速移動
            velocity.x = 2.0f;
            velocity.y = 2.0f;
        }

        //** 自機を動かす
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if(x < 0.0f) velocity.x *= -1.0f; else if(x == 0.0f) velocity.x = 0.0f;
        if(y < 0.0f) velocity.y *= -1.0f; else if(y == 0.0f) velocity.y = 0.0f;
        if(velocity.x != 0.0f && velocity.y != 0.0f){
            //ナナメ移動速度調整
            velocity.x *= recepSqrt2;
            velocity.y *= recepSqrt2;
        }
        transform.position = new Vector3(transform.position.x + velocity.x,transform.position.y + velocity.y , 0.0f);

        //** ショットを打つ
        if(InputArray["Shot"] > 0 && InputArray["Shot"] % 10 == 0){
            //Debug.Log("ショットを打つ");
            Instantiate(shotObjs[0],transform.position,Quaternion.identity);
        }
        
    }

    void CalcInput(){
        //Shot:Zキー:ショット
        //Fire3:左シフト:低速移動
        string[] str = { "Shot", "Fire2", "Fire3" };
        for(int i = 0; i < str.Length; ++i)
        {
            if (Input.GetButton(str[i]))
            {
                //Debug.Log("押されてるキー：" + str[i]);
                ++InputArray[str[i]];
            }
            else
            {
                InputArray[str[i]] = 0;
            }
        }

        

    }
}
