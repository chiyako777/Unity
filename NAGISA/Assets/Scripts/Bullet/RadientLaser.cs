
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadientLaser : MonoBehaviour
{

    //停止フラグ（主にワープ制御）
    [HideInInspector]
    public bool stopFlg = false;
    //開始位置
    [HideInInspector]
    public Vector3 startPos;
    //スピード（=Lengthを伸ばす係数）
    [HideInInspector]
    public float speed;
    //初期角度
    [HideInInspector]
    public float initAng;
    //先端線の開始頂点インデックス(直線ならば0、曲がる場合は最後に曲がった点)
    private int lastTurnPoint = 0;

    //ラインの頂点配列
    private List<Vector3> positions = new List<Vector3>();
    //コライダーの頂点配列
    private List<Vector2> colPoints = new List<Vector2>();
    //頂点間の角度の配列
    [HideInInspector]
    public List<float> angles = new List<float>();      //index 0 は実質不使用
    //頂点間の長さ配列
    private List<float> lengths = new List<float>();    //index 0 は実質不使用

    //** cache
    [HideInInspector]
    public LineRenderer lineRenderer;
    [HideInInspector]
    public EdgeCollider2D collider;

    void Start()
    {
        //Debug.Log("Radient Laser Start");
        //** コンポーネント取得
        this.lineRenderer = GetComponentInChildren<LineRenderer>();
        this.collider = GetComponentInChildren<EdgeCollider2D>();

        //** 初期Length、Angle設定(index1～9)
        lengths.Add(0.0f);
        angles.Add(0.0f);
        for(int i=1; i<=9; i++){
            lengths.Add(0.0f);
            angles.Add(initAng);
        }

        //** 初期Position設定
        positions.Add(new Vector3(startPos.x,startPos.y,0.0f));
        for(int i=1; i<=9; i++){
            positions.Add(new Vector3(
                            positions[i-1].x + (Mathf.Cos(angles[i] * Mathf.Deg2Rad) * lengths[i]),
                            positions[i-1].y + (Mathf.Sin(angles[i] * Mathf.Deg2Rad) * lengths[i]),
                            0.0f
                            ));
        }

        //Debug
        // foreach(Vector3 pos in positions){
        //     Debug.Log("pos = " + pos);
        // }
        // foreach(float a in angles){
        //     Debug.Log("angle = " + a);
        // }
        // foreach(float l in lengths){
        //     Debug.Log("length = " + l);
        // }
        
    }

    void Update()

    {
        if(stopFlg){return;}
        CalcLength();
        CalcPositions();

        if(lastTurnPoint != 0){
            // for(int i=lastTurnPoint + 1; i<=positions.Count - 1; i++){
            //     Debug.Log("pos = " + positions[i]);
            // }
            // for(int i=lastTurnPoint + 1; i<=angles.Count - 1; i++){
            //     Debug.Log("angle = " + angles[i]);
            // }
            // for(int i=lastTurnPoint + 1; i<=lengths.Count - 1; i++){
            //     Debug.Log("length[" + i + "] = "+ lengths[i]);
            // }
        }

    }

    private void CalcPositions(){

        for(int i=lastTurnPoint + 1; i<=lengths.Count - 1; i++){
            //Debug.Log("CalcPosition : lastTurnPoint = " + lastTurnPoint);
            positions[i] = new Vector3(
                            positions[i-1].x + (Mathf.Cos(angles[i] * Mathf.Deg2Rad) * lengths[i]),
                            positions[i-1].y + (Mathf.Sin(angles[i] * Mathf.Deg2Rad) * lengths[i]),
                            0.0f
                            );
        }
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());

    }

    private void CalcLength(){
        for(int i=lastTurnPoint + 1; i<=lengths.Count - 1; i++){
            //Debug.Log("CalcLength : lastTurnPoint = " + lastTurnPoint);
            lengths[i] += speed;
        }
    }

    //turnAngle:ワールド軸に対する角度で指定
    public void ExecTurn(float turnAngle){
        //Debug.Log("Radient Laser : ExecTurn turnAngle = " + turnAngle);
        //turnPoint ⇒ 曲げる前の先端
        lastTurnPoint = positions.Count - 1;
        //Debug.Log("lastTurnPoint = " + lastTurnPoint);

        //新しい角度で頂点を10個追加
        for(int i=lastTurnPoint + 1; i<=lastTurnPoint + 10; i++){   //lastTurnPointが9なら、positions[10] ~ positions[19]まで追加
            angles.Add(turnAngle);  //angles(10~19)
            lengths.Add(0.0f);      //lengths(10~19) 伸ばした先のlengthは初期で0
            positions.Add(new Vector3(
                            positions[i-1].x + (Mathf.Cos(angles[i] * Mathf.Deg2Rad) * lengths[i]),
                            positions[i-1].y + (Mathf.Sin(angles[i] * Mathf.Deg2Rad) * lengths[i]),
                            0.0f
                            ));
        }
    }

}
