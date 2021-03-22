
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//** 敵機基底クラス
public class Enemy : MonoBehaviour
{

    [HideInInspector]
    public EnemyInfo enemyInfo;

    protected float maxLife;      //敵総体力
    protected float motionFrame = 0.0f;
    protected int bulletVanishFlg = 1;
    protected bool defeatedFlg = false;     //敵機撃破済みフラグ

    //** caches
    protected Image lifeGageImage;  //敵体力ゲージ
    protected SpriteRenderer spriteRenderer;
    protected PlayerController playerController;
    [HideInInspector]
    public BulletController bulletController;    //弾幕コントローラー

    //** const
    protected const int lifeDispCnt = 60; //敵体力ゲージ表示演出長さ
    protected const int defeatMotionCnt = 60; //敵撃破演出長さ
    protected const int enemyStatusInit = 0;        //敵ステータス：初期状態
    protected const int enemyStatusGageDisp = 1;    //敵ステータス：体力ゲージ表示演出
    protected const int enemyStatusCutIn = 2;       //敵ステータス：スペルカットイン演出
    protected const int enemyStatusBullet = 3;      //敵ステータス：戦闘時
    protected const int enemyStatusDefeated = 4;      //敵ステータス：撃破時演出
    protected const int enemyStatusFinish = 5;      //敵ステータス：終了

    protected void Start()
    {
        //Debug.Log("Start : 敵生成");
        maxLife = enemyInfo.life;
        lifeGageImage = enemyInfo.lifeGage.GetComponent<Image>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        bulletController = enemyInfo.bulletController.GetComponent<BulletController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
        //Debug.Log("Start : 敵update");

        //** 情報同期
        bulletController.enemyLocation.x = enemyInfo.enemyLocation.x;
        bulletController.enemyLocation.y = enemyInfo.enemyLocation.y;
        bulletController.enemyLocation.z = enemyInfo.enemyLocation.z;

        switch(enemyInfo.enemyStatus){
            case 0:
                enemyInfo.enemyStatus = 1;
                break;
            case 1:
                //** 敵体力表示
                lifeGageImage.fillAmount = Mathf.Clamp(lifeGageImage.fillAmount + (1.0f/(float)lifeDispCnt),0.0f,1.0f);
                if(lifeGageImage.fillAmount >= 1.0f){
                    enemyInfo.enemyStatus = 2;
                }
                break;
            case 2:
                //** スペルカットイン
                enemyInfo.enemyStatus = 3;
                break;
            case 3:
                //** 弾幕アクティブ化
                bulletController.activeFlg = true;
                break;
            case 4:
                //** 撃破時演出
                //Debug.Log("motionFrame = " + motionFrame);
                motionFrame++;
                //敵機
                if(motionFrame < defeatMotionCnt){
                   Vanish(); 
                }

                //弾幕
                bulletVanishFlg = bulletController.Vanish(motionFrame);
                if(bulletVanishFlg == 0){
                    //Debug.Log("弾幕デストロイ");
                    bulletController.DeleteBullet();
                    Destroy(enemyInfo.bulletController);
                }
                //敵機・弾幕共に消滅演出終わったら、ステータス遷移
                if(motionFrame > defeatMotionCnt && bulletVanishFlg == -1){
                    //Debug.Log("ステータス5へ");
                    enemyInfo.enemyStatus = 5;
                }
                break;
            case 5:
                //Debug.Log("終末");
                Destroy(gameObject);
                CommandManager.nextCommandFlg = true;
                break;
            default:
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(defeatedFlg){
            if(collision.gameObject.tag == "Shot" || collision.gameObject.tag == "Bomb_Shot"){
                Destroy(collision.gameObject);
            }
            return; 
        }

        if(collision.gameObject.tag == "Shot" || collision.gameObject.tag == "Bomb_Shot"){
            //Debug.Log("自機ショットに当たった " + "life:" + enemyInfo.life);
            if(bulletController.activeFlg){
                enemyInfo.life -= collision.GetComponent<PlayerShot>().power + playerController.power;
                lifeGageImage.fillAmount = 1.0f/(float)maxLife * (float)enemyInfo.life;
                if(enemyInfo.life < 0){
                    //※敵機撃破時に一度だけ呼ばれるパス
                    //Debug.Log("敵機撃破");
                    defeatedFlg = true;
                    enemyInfo.enemyStatus = 4;      //撃破時演出開始
                    Instantiate(enemyInfo.defeatEffect,
                                new Vector3(enemyInfo.enemyLocation.x,enemyInfo.enemyLocation.y,0.0f),
                                Quaternion.identity);
                    Destroy(enemyInfo.lifeGage);
                    CreateDefeatedBonus();
                }
            }
            Destroy(collision.gameObject);
        }
        

    }

    //** 敵機・弾幕消滅
    private void Vanish(){
        float alpha = 1.0f - (1.0f * motionFrame/(float)defeatMotionCnt);
        spriteRenderer.color = new Color(1.0f,1.0f,1.0f,alpha);
    }

    //** 撃破時アイテム発生
    protected virtual void CreateDefeatedBonus(){
        //none
    }

}
