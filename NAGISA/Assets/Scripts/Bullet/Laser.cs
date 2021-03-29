using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//** レーザー：予告線+直線タイプ
public class Laser : MonoBehaviour
{

    [HideInInspector]
    public int status = 0;      //予告線状態、拡大状態、マックス状態、縮小状態
    [HideInInspector]
    public float angle;
    [HideInInspector]
    public float length;
    [HideInInspector]
    public Vector3 startPos;

    //ラインの頂点配列
    private Vector3[] positions = new Vector3[10];

    //** cache
    [HideInInspector]
    public LineRenderer lineRenderer;
    [HideInInspector]
    public BoxCollider2D collider;

    void Start()
    {
        this.lineRenderer = GetComponent<LineRenderer>();
        this.collider = GetComponent<BoxCollider2D>();
        status = 1;     //予告線状態
    }


    void Update()
    {
        //** Length,Angleに応じた頂点再定義
        CalcPositions();

        //** コライダー角度合わせ(コライダー単体で回転できないのでオブジェクトそのものを回転)
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);

        //** コライダー設定
        SetCollider();

        switch(status){
            case 1 :        //予告線状態
                lineRenderer.widthMultiplier = 1.0f;
                break;

            case 2 :        //拡大状態
                if(lineRenderer.widthMultiplier <= 4.0f){
                    lineRenderer.widthMultiplier += 0.2f;
                }else{
                    status = 3;
                }
                break;

            case 3 :        //マックス状態
                break;

            case 4 :        //縮小状態
                if(lineRenderer.widthMultiplier >= 1.0f){
                    lineRenderer.widthMultiplier -= 0.2f;
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
                                positions[i-1].x + ( Mathf.Cos(angle * Mathf.Deg2Rad) * length / 10.0f )
                                ,positions[i-1].y + ( Mathf.Sin(angle * Mathf.Deg2Rad) * length / 10.0f )
                                ,0.0f);
        }

        lineRenderer.SetPositions(positions);

    }

    private void SetCollider(){
        collider.offset = new Vector2(
            Mathf.Abs(positions[0].x - positions[9].x) / 2.0f,collider.offset.y);

    }

}





