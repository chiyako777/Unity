using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaTipAnimeController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    void Update()
    {

        if((bool)FlagManager.flagDictionary["coroutine"]){
            return;
        }

        //方向キー入力があった最初のフレームは向き変更
        if(Input.anyKeyDown){
            Vector2? action = this.ActionKeyDown();
            if(action.HasValue){
                SetStateToAnimator(vector : action.Value);
                return;
            }
        }

        //入力ベクトル
        Vector2 vector = new Vector2(
            (int)Input.GetAxis("Horizontal"),
            (int)Input.GetAxis("Vertical")
        );

        //アニメーション設定
        // ( キー入力 が続いている場合は入力から作成したベクトルを渡す )
        // ( キー入力が無ければnull )
        SetStateToAnimator(vector : vector != Vector2.zero ? vector : (Vector2?)null);              

    }


    //** 各方向の単位ベクトル返却
    private Vector2? ActionKeyDown(){
        if (Input.GetKeyDown(KeyCode.UpArrow)) return Vector2.up;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) return Vector2.left;
        if (Input.GetKeyDown(KeyCode.DownArrow)) return Vector2.down;
        if (Input.GetKeyDown(KeyCode.RightArrow)) return Vector2.right;
        return null;
    }

    //** アニメーションの状態を設定
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

}
