using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string optionType;   //デバッグ用にインスペクタで自機オプションタイプ設定(Homing,Reflec,Warp&Power)

    private Vector2 velocity;   //自機の移動量
    private float recepSqrt2;    // 1 / √2  (ナナメ移動時速度調整)
    private int mutekiTime = 0;     //被弾後無敵時間
    private int bombTime = 0;       //ボム時間
    private int reflecTime = 0;     //リフレク時間 

    private Dictionary<string,int> InputArray;  //各種入力制御
    public GameObject[] shotObjs;   //自機ショット用オブジェクト（Inspectorでプレハブを指定）

    //** caches
    private GameObject enemy;
    private GameObject reflec;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //** const
    private const int maxMutekiTime = 150;
    private const int maxBombTime = 300;
    private const int maxReflecTime = 1000;

    void Start()
    {
        velocity = new Vector2(0.0f,0.0f);
        recepSqrt2 = 1.0f / Mathf.Sqrt(2.0f);
        
        InputArray = new Dictionary<string,int>();
        InputArray["Shot"] = 0;
        InputArray["Bomb"] = 0;
        InputArray["Fire3"] = 0;
        InputArray["Fire1"] = 0;

        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        //** 敵機をキャッシュ
        LoadEnemy();
        //** 入力情報
        CalcInput();
        //** 速度設定
        SetVeloc();

        //** 自機を動かす
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if(x < 0.0f){ 
            velocity.x *= -1.0f;
            this.animator.SetInteger("XAxis",-1);
        }else if(x > 0.0f){
            this.animator.SetInteger("XAxis",1);
        }else if(x == 0.0f){
            velocity.x = 0.0f;
            this.animator.SetInteger("XAxis",0);
        }
        if(y < 0.0f) velocity.y *= -1.0f; else if(y == 0.0f) velocity.y = 0.0f;
        if(velocity.x != 0.0f && velocity.y != 0.0f){
            //ナナメ移動速度調整
            velocity.x *= recepSqrt2;
            velocity.y *= recepSqrt2;
        }
        transform.position = new Vector3(transform.position.x + velocity.x,transform.position.y + velocity.y , 0.0f);

        //** ショットを打つ
        if(InputArray["Shot"] > 0 && InputArray["Shot"] % 10 == 0
            && mutekiTime == 0 && bombTime == 0){
            //Debug.Log("ショットを打つ");
            Instantiate(shotObjs[0],transform.position,Quaternion.identity);    //通常ショット
            if(optionType.Equals("Homing",StringComparison.CurrentCulture)){
                //ホーミング2Way
                Vector3 leftPos = new Vector3(transform.position.x-5.0f,transform.position.y-7.0f,0.0f);
                GameObject lh = Instantiate(shotObjs[2],leftPos,Quaternion.identity);
                lh.GetComponent<PlayerHomingShot>().lr = true;
                Vector3 rightPos = new Vector3(transform.position.x+5.0f,transform.position.y-7.0f,0.0f);
                GameObject rh = Instantiate(shotObjs[2],rightPos,Quaternion.identity);
                rh.GetComponent<PlayerHomingShot>().lr = false;
            }
        }

        //** ボムを打つ
        if(InputArray["Bomb"] > 0 && mutekiTime == 0 && bombTime == 0){
            bombTime = 1;
        }
        bombTime = (bombTime > 0) ? ++bombTime : 0;
        if(bombTime >= maxBombTime){ bombTime = 0; }
        if(bombTime > 0 && bombTime % 10 == 0){
            Instantiate(shotObjs[1],transform.position,Quaternion.identity);
        }

        //** リフレク
        if(optionType == "Reflec"){
            if(InputArray["Fire1"] > 0 && reflecTime < maxReflecTime){
                if(reflecTime == 0){
                    //リフレクモード開始(リフレクオブジェクト生成)
                    Vector3 initialPos = new Vector3(transform.position.x,transform.position.y + 15.0f,0.0f);
                    reflec = Instantiate(shotObjs[3],initialPos,Quaternion.identity);
                }
                reflecTime++;
            }else{
                reflecTime = 0;
                if(reflec != null){
                    //Debug.Log("Reflec Destroy");
                    Destroy(reflec);
                }
            }
        }

        //** 無敵時間カウント
        mutekiTime = (mutekiTime > 0) ? ++mutekiTime : 0;
        if(mutekiTime >= maxMutekiTime){ 
            mutekiTime = 0;
            spriteRenderer.color = new Color(1.0f,1.0f,1.0f,1.0f);
        }
        if(mutekiTime > 0){
            Blink();
        }
    }

    void CalcInput(){
        //Shot:Zキー:ショット
        //Bomb:xキー:ボム
        //Fire3:左シフト:低速移動
        //Fire1:左Ctr:リフレク
        string[] str = { "Shot", "Bomb", "Fire3", "Fire1" };
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

    void LoadEnemy(){
        if(enemy == null){
            enemy = GameObject.FindGameObjectWithTag("Enemy_Bullet");
            //Debug.Log("LoadEnemy Enemy Cache: " + enemy);
        }
    }

    //** 速度設定
    void SetVeloc(){
        if(InputArray["Fire3"] > 0){
            //低速移動
            velocity.x = 1.0f;
            velocity.y = 1.0f;
        }else{
            //高速移動
            velocity.x = 2.0f;
            velocity.y = 2.0f;
        }
        if(bombTime > 0){
            //ボム時は自機移動速度低下
            velocity.x = 0.5f;
            velocity.y = 0.5f;
        }
        if(reflecTime > 0){
            //リフレク中は動けない
            velocity.x = 0.0f;
            velocity.y = 0.0f;
        }
    }

    //** 被弾処理
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Bullet" && mutekiTime==0 && bombTime == 0){
            //Debug.Log("自機被弾");
            //無敵時間カウント開始
            mutekiTime = 1;
            //弾幕消去
            enemy.GetComponent<Enemy>().bulletController.DeleteBullet();
        }
    }
    
    //** 無敵時間中自機点滅
    private void Blink(){
        float alpha = (Mathf.Sin(mutekiTime * 0.1f) + 1) / 2;   //正弦波を0~1に正規化
        spriteRenderer.color = new Color(1.0f,1.0f,1.0f,alpha);
    }
}
