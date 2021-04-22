using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** レーザー：NoticeLaser (現在は直線レーザーのみ対応)
public class NoticeLaser : MonoBehaviour
{

    [HideInInspector]
    public int status = 0;      //予告線状態、拡大状態、マックス状態、縮小状態
    [HideInInspector]
    public float angle;
    [HideInInspector]
    public float length;
    [HideInInspector]
    public Vector3 startPos;
    [HideInInspector]
    public bool stopFlg = false;

    //ラインの頂点配列
    private Vector3[] positions = new Vector3[10];
    //コライダーの頂点配列
    private Vector2[] colPoints = new Vector2[2];

    //** cache
    [HideInInspector]
    public LineRenderer lineRenderer;
    [HideInInspector]
    public EdgeCollider2D collider;

    void Start()
    {        
        //** コンポーネント取得
        this.lineRenderer = GetComponentInChildren<LineRenderer>();
        this.collider = GetComponentInChildren<EdgeCollider2D>();
        
        //** ステータス = 予告線状態
        status = 1;     

    }


    void Update()
    {
        if(stopFlg){return;}

        //** Length,Angleに応じた頂点再定義
        CalcPositions();

        //** コライダー設定
        SetCollider();

        switch(status){
            case 1 :        //予告線状態
                lineRenderer.widthMultiplier = 1.0f;
                //※予告線状態解除は、各スペル側で
                break;

            case 2 :        //拡大状態
                if(lineRenderer.widthMultiplier <= 4.0f){
                    lineRenderer.widthMultiplier += 0.1f;
                }else{
                    status = 3;
                }
                break;

            case 3 :        //マックス状態
                break;

            case 4 :        //縮小状態
                if(lineRenderer.widthMultiplier >= 1.0f){
                    lineRenderer.widthMultiplier -= 0.1f;
                }
                break;

            default :
                break;
        }
    }

    private void CalcPositions(){

        positions[0] = new Vector3(startPos.x,startPos.y,0.0f);
        for(int i=1; i<10; i++){
            positions[i] = new Vector3(
                                positions[i-1].x + ( Mathf.Cos(angle * Mathf.Deg2Rad) * length / 9.0f )
                                ,positions[i-1].y + ( Mathf.Sin(angle * Mathf.Deg2Rad) * length / 9.0f )
                                ,0.0f);
        }
        lineRenderer.SetPositions(positions);

    }

    private void SetCollider(){
        colPoints[0] = new Vector2(positions[1].x,positions[1].y);
        colPoints[1] = new Vector2(positions[8].x,positions[8].y);
        collider.points = colPoints;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        //Debug.Log("NoticeLaser : Trigger Enter");
        //** ボム・リフレクによる弾消し
        if(collision.gameObject.tag == "Bomb_Shot" || collision.gameObject.tag == "Reflec_Shot"){
            //Debug.Log("ボム・リフレクによるレーザー消し");
            for(int i=0; i<transform.childCount; i++){
                Destroy(transform.GetChild(i).gameObject);
            }
            Destroy(gameObject);
            
        }
    }

}





