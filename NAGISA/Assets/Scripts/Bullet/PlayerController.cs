using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public string optionType;   //デバッグ用にインスペクタで自機オプションタイプ設定(Homing,Reflec,Warp)

    [HideInInspector]
    public int life = 3;    //残機
    [HideInInspector]
    public int bomb = 4;    //ボム数
    [HideInInspector]
    public float power = 1.0f;  //パワー(max:5)

    private Vector2 velocity;   //自機の移動量
    private float recepSqrt2;    // 1 / √2  (ナナメ移動時速度調整)
    [HideInInspector]
    public int mutekiTime = 0;     //被弾後無敵時間
    private int bombTime = 0;       //ボム時間
    private int reflecTime = 0;     //リフレク時間
    private int warpStatus = 0;     //ワープステータス(1:ワープ可能範囲計算中、2:ワープ地点選択中)
    private int warpVH = 0;         //ワープ方向フラグ(1:Horizontal 2:Vertical)
    

    private Dictionary<string,int> InputArray;  //各種入力制御
    [SerializeField]
    private GameObject[] shotObjs;   //自機ショット用オブジェクト（Inspectorでプレハブを指定）

    private float warpLength = 37.0f;   //ワープ可能領域の大きさ(正方形の辺の半分)

    //** caches
    private GameObject enemy;
    private GameObject reflec;
    private InfoController infoController;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private GameObject warpArea;

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
        InputArray["Fire2"] = 0;
        InputArray["WarpH"] = 0;
        InputArray["WarpV"] = 0;

        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.infoController = GameObject.Find("bulletinfo_controller").GetComponent<InfoController>();

        //test
        //shotObjs[0].tag = "Bullet";
    }
    
    void Update()
    {
        //spriteRenderer.color = new Color(1.0f,1.0f,1.0f,1.0f);
        //** 敵機をキャッシュ
        LoadEnemy();
        //** 情報表示
        InfoUpdate();
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
        if(warpStatus != 2){
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + velocity.x,BulletUtility.screenTopLeftJust.x,BulletUtility.screenBottomRightJust.x),
                                            Mathf.Clamp(transform.position.y + velocity.y,BulletUtility.screenBottomRightJust.y,BulletUtility.screenTopLeftJust.y),
                                            0.0f);
        }else{
            //ワープ地点選択中
            float warpX = warpArea.transform.position.x;
            float warpY = warpArea.transform.position.y;
            float limitXLeft = (BulletUtility.screenTopLeftJust.x < (warpX - warpLength)) 
                                    ? warpX - warpLength
                                    : BulletUtility.screenTopLeftJust.x;
            float limitXRight = (BulletUtility.screenBottomRightJust.x > (warpX + warpLength))
                                    ? warpX + warpLength
                                    : BulletUtility.screenBottomRightJust.x;
            float limitYTop = (BulletUtility.screenTopLeftJust.y > (warpY + warpLength))
                                    ? warpY + warpLength
                                    : BulletUtility.screenTopLeftJust.y;
            float limitYBottom = (BulletUtility.screenBottomRightJust.y < (warpY - warpLength))
                                    ? warpY - warpLength
                                    : BulletUtility.screenBottomRightJust.y;

            transform.position = new Vector3(Mathf.Clamp(transform.position.x + velocity.x , limitXLeft , limitXRight),
                                            Mathf.Clamp(transform.position.y + velocity.y , limitYBottom , limitYTop),
                                            0.0f);
        }

        //** ショットを打つ
        if(InputArray["Shot"] > 0 && InputArray["Shot"] % 5 == 0
            && mutekiTime == 0 && bombTime == 0 && warpStatus == 0){
            //Debug.Log("ショットを打つ");
            //通常ショット
            if(power < 3.0f){
                GameObject b = Instantiate(shotObjs[0],new Vector3(transform.position.x,transform.position.y + 5.0f,0.0f),Quaternion.identity);
                b.GetComponent<PlayerShot>().optionType = optionType;
            }else{
                GameObject b1 = Instantiate(shotObjs[0],new Vector3(transform.position.x - 3.0f,transform.position.y + 5.0f,0.0f),Quaternion.identity);
                b1.GetComponent<PlayerShot>().optionType = optionType;
                GameObject b2 = Instantiate(shotObjs[0],new Vector3(transform.position.x + 3.0f,transform.position.y + 5.0f,0.0f),Quaternion.identity);
                b2.GetComponent<PlayerShot>().optionType = optionType;
            }

            //if(optionType.Equals("Homing",StringComparison.CurrentCulture)){
            if(optionType == "Homing"){    
                //ホーミング2Way
                Vector3 leftPos = new Vector3(transform.position.x-5.0f,transform.position.y-7.0f,0.0f);
                GameObject lh = Instantiate(shotObjs[2],leftPos,Quaternion.identity);
                lh.GetComponent<PlayerHomingShot>().lr = true;
                Vector3 rightPos = new Vector3(transform.position.x+5.0f,transform.position.y-7.0f,0.0f);
                GameObject rh = Instantiate(shotObjs[2],rightPos,Quaternion.identity);
                rh.GetComponent<PlayerHomingShot>().lr = false;
            }

            if(optionType == "Warp"){
                //拡散ショット
                for(float i=-36.0f; i<=36.0f; i+=6.0f){
                    GameObject b = Instantiate(shotObjs[0],new Vector3(transform.position.x,transform.position.y + 5.0f,0.0f),Quaternion.AngleAxis(i,Vector3.forward));
                    b.GetComponent<PlayerShot>().optionType = optionType;
                }
            }
        }

        //** ボムを打つ
        if(InputArray["Bomb"] > 0 && mutekiTime == 0 && bombTime == 0 && bomb >= 1 && warpStatus == 0
            && enemy != null
            && enemy.GetComponent<Enemy>().bulletController.activeFlg){
            //Debug.Log("Bomb");
            bombTime = 1;
            bomb -= 1;
        }
        bombTime = (bombTime > 0) ? ++bombTime : 0;
        if(bombTime >= maxBombTime){ bombTime = 0; }
        if(bombTime > 0 && bombTime % 10 == 0){
            GameObject b1 = Instantiate(shotObjs[1],new Vector3(transform.position.x - 3.0f,transform.position.y + 5.0f,0.0f),Quaternion.identity);
            b1.GetComponent<PlayerBombShot>().type = "center";
            GameObject b2 = Instantiate(shotObjs[1],new Vector3(transform.position.x + 3.0f,transform.position.y + 5.0f,0.0f),Quaternion.identity);
            b2.GetComponent<PlayerBombShot>().type = "center";
            GameObject b3 = Instantiate(shotObjs[1],new Vector3(transform.position.x - 3.0f,transform.position.y + 5.0f,0.0f),Quaternion.identity);
            b3.GetComponent<PlayerBombShot>().type = "left";
            GameObject b4 = Instantiate(shotObjs[1],new Vector3(transform.position.x + 3.0f,transform.position.y + 5.0f,0.0f),Quaternion.identity);
            b4.GetComponent<PlayerBombShot>().type = "right";
        }

        //** リフレク
        if(optionType == "Reflec"){
            if(InputArray["Fire2"] > 0 && reflecTime < maxReflecTime){
                if(reflecTime == 0 && enemy != null && enemy.GetComponent<Enemy>().bulletController.activeFlg){
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

        //** ワープ
        if(optionType == "Warp"){

            //ワープモード開始
            if((InputArray["WarpH"] > 0 || InputArray["WarpV"] > 0) && warpStatus == 0 && enemy != null && 
                    enemy.GetComponent<Enemy>().bulletController.activeFlg){

                if(InputArray["WarpV"] > 0){warpVH = 2;}
                if(InputArray["WarpH"] > 0){warpVH = 1;}        //もし同時にWarpH,WarpV同時に押されていたらHorizontalを優先
                
                spriteRenderer.color = new Color(0.166f,0.376f,0.858f,1.0f);
                enemy.GetComponent<Enemy>().bulletController.StopAll();
                warpStatus = 1;

            }

            switch(warpStatus){
                //ワープ範囲計算
                case 1 :    
                    //途中でWarpH,WarpV離したら終了
                    if( (warpVH == 1 && InputArray["WarpH"] == 0) || (warpVH == 2 && InputArray["WarpV"] == 0)){
                        if(warpArea != null){Destroy(warpArea);}
                        spriteRenderer.color = new Color(1.0f,1.0f,1.0f,1.0f);
                        enemy.GetComponent<Enemy>().bulletController.RestartAll();
                        warpStatus = 0;
                        warpVH = 0;
                    }

                    //計算中に敵が死んだら終了
                    if(enemy == null){
                        if(warpArea != null){Destroy(warpArea);}
                        spriteRenderer.color = new Color(1.0f,1.0f,1.0f,1.0f);
                        warpStatus = 0;
                        warpVH = 0;
                    }

                    if(warpArea == null){                
                        //最初にワープ範囲矩形を生成
                        warpArea = Instantiate(BulletMainManager.resourcesLoader.GetObjectHandle("test_warpArea"),CalcWarpArea(warpVH),Quaternion.identity);
                    }
                    //ワープ範囲を大きくしていく
                    Vector3 nowScale = warpArea.transform.localScale;
                    if(nowScale.x <= 60.0f){
                        warpArea.transform.localScale = new Vector3(nowScale.x + 1.5f,nowScale.y + 1.5f,1.0f);
                    }else{
                        //ワープ範囲が所定の大きさに達したら、地点選択へ
                        warpStatus = 2;
                        warpArea.GetComponent<SpriteRenderer>().color = new Color(0.116f,0.915f,0.883f,1.0f);
                        transform.position = new Vector3(warpArea.transform.position.x,warpArea.transform.position.y,0.0f);
                    }
                    break;
                    
                //ワープ地点選択
                case 2 :    
                    //WarpH,WarpVを離したらワープ実行
                    if( (warpVH == 1 && InputArray["WarpH"] == 0) || (warpVH == 2 && InputArray["WarpV"] == 0)){
                        if(warpArea != null){Destroy(warpArea);}
                        spriteRenderer.color = new Color(1.0f,1.0f,1.0f,1.0f);
                        enemy.GetComponent<Enemy>().bulletController.RestartAll();
                        warpStatus = 0;
                        warpVH = 0;
                    }

                    //地点選択中に敵が死んだら終了
                    if(enemy == null){
                        if(warpArea != null){Destroy(warpArea);}
                        spriteRenderer.color = new Color(1.0f,1.0f,1.0f,1.0f);
                        warpStatus = 0;
                        warpVH = 0;
                    }

                    break;
                default : 
                    break;
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
        //Fire2:左Alt:リフレク
        string[] str = { "Shot", "Bomb", "Fire3", "Fire2" ,"WarpH" , "WarpV" };
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
        //Debug.Log("collision.gameObject.tag = " + collision.gameObject.tag + " mutekiTime = " + mutekiTime + " bombTime = " + bombTime);
        if((collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Laser")
                && mutekiTime==0 && bombTime == 0 && warpStatus == 0){

            //レーザー NoticeLaser:max時のみ被弾
            if(collision.gameObject.tag == "Laser"){
                GameObject laserObj = collision.gameObject.transform.parent.gameObject;

                if(laserObj.GetComponent<NoticeLaser>()){
                    NoticeLaser noticeLaser = laserObj.GetComponent<NoticeLaser>();
                    if(noticeLaser.status != 3){
                        //Debug.Log("レーザー：セーフ");
                        return;
                    }
                }
                
            }

            Debug.Log("自機被弾");

            if(life > 0){
                //残機マイナス
                life--;
                //ボム数リセット
                bomb = 2;
                //無敵時間カウント開始
                mutekiTime = 1;
                //弾幕消去
                enemy.GetComponent<Enemy>().bulletController.DeleteBullet();
            }else{
                //** ゲームオーバー(取り急ぎ前シーンへ遷移のみ)
                SceneManager.LoadScene(BulletMainManager.beforeScene);                
            }
        }
    }
    
    //** 無敵時間中自機点滅
    private void Blink(){
        float alpha = (Mathf.Sin(mutekiTime * 0.1f) + 1) / 2;   //正弦波を0~1に正規化
        spriteRenderer.color = new Color(1.0f,1.0f,1.0f,alpha);
    }

    //** ワープ可能区域計算(中心点返却？)
    private Vector3 CalcWarpArea(int vh){
        Vector3 centerArea = new Vector3(0.0f,0.0f,0.0f);
        float x = transform.position.x;
        float y = transform.position.y;
        if(vh == 1){
            //Horizon
            if(x == BulletUtility.centerPos.x){return centerArea;}
            centerArea.x = (x < BulletUtility.centerPos.x)
                                        ? BulletUtility.centerPos.x + Mathf.Abs(BulletUtility.centerPos.x - x)
                                        : BulletUtility.centerPos.x - Mathf.Abs(BulletUtility.centerPos.x - x);
            centerArea.y = y;
        }else if(vh == 2){
            //Vertical
            if(y == BulletUtility.centerPos.y){return centerArea;}
            centerArea.x = x;
            centerArea.y = (y < BulletUtility.centerPos.y)
                                        ? BulletUtility.centerPos.y + Mathf.Abs(BulletUtility.centerPos.y - y)
                                        : BulletUtility.centerPos.y - Mathf.Abs(BulletUtility.centerPos.x - y);
        }
        return centerArea;
    }


    //** 弾幕情報画面に受け渡し
    private void InfoUpdate(){
        infoController.life = life;
        infoController.bomb = bomb;
        infoController.power = power;
    }

}
