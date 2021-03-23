using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//※BlendTreeでアニメ制御
public class CharaTipAnimeController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    void Update()
    {
        //会話イベント中アニメーション再生停止
        if((bool)FlagManager.flagDictionary["coroutine"]){
            this.animator.SetBool("isCoroutine",true);
            return;
        }

        if(!(bool)FlagManager.flagDictionary["coroutine"]){
            this.animator.SetBool("isCoroutine",false);
        }

        //方向キー入力があったら向き変更
        if(Input.anyKeyDown){
            Vector2? action = this.ActionKeyDown();
            if(action.HasValue){
                SetStateToAnimator(vector : action.Value);
                return;
            }
        }

        //入力からベクトルを作成
        Vector2 vector = new Vector2(
            (int)Input.GetAxis("Horizontal"),
            (int)Input.GetAxis("Vertical")
        );

        //キー入力 が続いている場合は入力から作成したベクトルを渡す
        //キー入力が無ければnull
        SetStateToAnimator(vector : vector != Vector2.zero ? vector : (Vector2?)null);
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        //Debug.Log("Collision Enter!");
        this.animator.SetBool("isColi",true);
    }

    void OnCollisionExit2D(Collision2D collision){
        //Debug.Log("Collision Exit!");
        this.animator.SetBool("isColi",false);
    }

    private void SetStateToAnimator(Vector2? vector) {

        //** 静止
        if(!vector.HasValue){
            this.animator.speed = 0.0f;
            return;
        }

        //** 歩行
        this.animator.speed = 1.0f;
        this.animator.SetFloat("x",vector.Value.x);
        this.animator.SetFloat("y",vector.Value.y);

    }


    private Vector2? ActionKeyDown(){
        if (Input.GetKeyDown(KeyCode.UpArrow)) return Vector2.up;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return Vector2.left;
        if (Input.GetKeyDown(KeyCode.DownArrow)) return Vector2.down;
        if (Input.GetKeyDown(KeyCode.RightArrow)) return Vector2.right;
        return null;
    }
}
