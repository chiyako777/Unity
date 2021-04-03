using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float m_updateInterval = 0.5f;      //FPS計算更新間隔(deltaTimeの蓄積が0.5secになったら更新されると考える)

    private float m_accum;
    private int m_frames;
    private float m_timeleft;
    private float m_fps;

    void Start()
    {
        Application.targetFrameRate = 60; //エディタ実行だと早すぎたので、60FPSに設定(ビルド前にコメントアウト & Quality設定のVSyncを垂直同期に戻すこと)
    }

    private void Update()
    {
        m_timeleft -= Time.deltaTime;
        //Time.timeScale = 1 (何もしてなければ基本1)
        //m_accum:「今のレートだと何FPSか」を1フレーム毎に計算して蓄積している。例えばdeltaTimeが0.1666ならm_accumに加算されるのは60(fps)
        m_accum += Time.timeScale / Time.deltaTime;
        m_frames++;

        if ( 0 < m_timeleft ){ 
            //deltaTimeの蓄積がm_updateIntervalに達するまでは、returnする
            return;
        }

        //m_accumは「今のレートだと何FPSか、を毎フレーム(=m_frames回分)計算して加算したもの」
        //m_accumをm_framesで割ることで、「今のレートだと何FPSか」が平均化される
        //平均化されたFPS値⇒m_fps
        m_fps = m_accum / m_frames;

        //各値初期化
        m_timeleft = m_updateInterval;
        m_accum = 0;
        m_frames = 0;
    }

    private void OnGUI(){
        GUILayout.Label( "FPS: " + m_fps.ToString( "f2" ) );
    }

}
