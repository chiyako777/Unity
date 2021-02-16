using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class CharaTip_Controller : MonoBehaviour
{
    //キャラ名（ゲームオブジェクトのインスペクタ上で設定）
    public string chara_name;

    //テクスチャ
    private List<Texture2D> texture_list = new List<Texture2D>();
    private List<string> file_suffix = new List<string>(){"_front","_front_walk1","_front_walk2",
                                                    "_back","_back_walk1","_back_walk2",
                                                    "_right","_right_walk1","_right_walk2",
                                                    "_left","_left_walk1","_left_walk2"};
    private string path = "Textures/CharaTip/";

    //動き制御
    private List<KeyCode> key_list = new List<KeyCode>();
    private float speed = 2.0f;
    private float cap_x = 0.434f;
    private float cap_y = 0.410f;

    private int frame = 0;

    void Start()
    {
        //Debug.Log("start!!");

        //** テクスチャロード
        for(int i = 0; i<12;i++){
            texture_list.Add(Resources.Load<Texture2D>(path + chara_name + "/" + chara_name + file_suffix[i]));
            if(texture_list[i] == null){Debug.Log("Texture Road Failed. name = " + chara_name + file_suffix[i]);}
        }

        //** 初期位置・テクスチャ
        GetComponent<Renderer>().material.mainTexture = texture_list[0];

    }

    void Update()
    {
        GameObject go = this.gameObject;

        //** キー制御
        if(Input.GetKeyDown(KeyCode.DownArrow)){key_list.Add(KeyCode.DownArrow);}
        if(Input.GetKeyDown(KeyCode.UpArrow)){key_list.Add(KeyCode.UpArrow);}
        if(Input.GetKeyDown(KeyCode.RightArrow)){key_list.Add(KeyCode.RightArrow);}
        if(Input.GetKeyDown(KeyCode.LeftArrow)){key_list.Add(KeyCode.LeftArrow);}
        if(Input.GetKeyUp(KeyCode.DownArrow)){
            key_list.Remove(KeyCode.DownArrow);
            GetComponent<Renderer>().material.mainTexture = texture_list[0];
            frame = 0;
        }
        if(Input.GetKeyUp(KeyCode.UpArrow)){
            key_list.Remove(KeyCode.UpArrow);
            GetComponent<Renderer>().material.mainTexture = texture_list[3];
            frame = 0;
        }
        if(Input.GetKeyUp(KeyCode.RightArrow)){
            key_list.Remove(KeyCode.RightArrow);
            GetComponent<Renderer>().material.mainTexture = texture_list[6];
            frame = 0;
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow)){
            key_list.Remove(KeyCode.LeftArrow);
            GetComponent<Renderer>().material.mainTexture = texture_list[9];
            frame = 0;
        }

        //** 歩行
        if(key_list.Find(x => x == KeyCode.DownArrow) == KeyCode.DownArrow){
            Vector3 temp = go.transform.localPosition - new Vector3(0.0f,0.1f,0.0f) * speed * Time.deltaTime;
            temp.y = Mathf.Clamp(temp.y,-1 * cap_y,cap_y);    //画面端
            go.transform.localPosition = temp;

            if(frame == 0){GetComponent<Renderer>().material.mainTexture = texture_list[1];}
            if(frame%30 == 0){
                GetComponent<Renderer>().material.mainTexture = 
                    (GetComponent<Renderer>().material.mainTexture == texture_list[1]) ? texture_list[2] : texture_list[1];
            }
            frame++;
        }
        if(key_list.Find(x => x == KeyCode.UpArrow) == KeyCode.UpArrow){
            Vector3 temp = go.transform.localPosition + new Vector3(0.0f,0.1f,0.0f) * speed * Time.deltaTime;
            temp.y = Mathf.Clamp(temp.y,-1 * cap_y,cap_y);    //画面端
            go.transform.localPosition = temp;

            if(frame == 0){GetComponent<Renderer>().material.mainTexture = texture_list[4];}
            if(frame%30 == 0){
                GetComponent<Renderer>().material.mainTexture = 
                    (GetComponent<Renderer>().material.mainTexture == texture_list[4]) ? texture_list[5] : texture_list[4];
            }
            frame++;
        }
        if(key_list.Find(x => x == KeyCode.RightArrow) == KeyCode.RightArrow){
            Vector3 temp = go.transform.localPosition + new Vector3(0.1f,0.0f,0.0f) * speed * Time.deltaTime;
            temp.x = Mathf.Clamp(temp.x,-1 * cap_x,cap_x);    //画面端
            go.transform.localPosition = temp;

            //ナナメ移動時、正面or後ろ向き画像が優先される
            if( (key_list.Find(x => x == KeyCode.DownArrow) != KeyCode.DownArrow) && 
                    (key_list.Find(x => x == KeyCode.UpArrow) != KeyCode.UpArrow)){
                if(frame == 0){GetComponent<Renderer>().material.mainTexture = texture_list[7];}
                if(frame%30 == 0){
                    GetComponent<Renderer>().material.mainTexture = 
                        (GetComponent<Renderer>().material.mainTexture == texture_list[7]) ? texture_list[8] : texture_list[7];
                }
                frame++;
            }
        }
        if(key_list.Find(x => x == KeyCode.LeftArrow) == KeyCode.LeftArrow){
            Vector3 temp = go.transform.localPosition - new Vector3(0.1f,0.0f,0.0f) * speed * Time.deltaTime;
            temp.x = Mathf.Clamp(temp.x,-1 * cap_x,cap_x);    //画面端
            go.transform.localPosition = temp;

            //ナナメ移動時、正面or後ろ向き画像が優先される
            if( (key_list.Find(x => x == KeyCode.DownArrow) != KeyCode.DownArrow) && 
                    (key_list.Find(x => x == KeyCode.UpArrow) != KeyCode.UpArrow)){
                if(frame == 0){GetComponent<Renderer>().material.mainTexture = texture_list[10];}
                if(frame%30 == 0){
                    GetComponent<Renderer>().material.mainTexture = 
                        (GetComponent<Renderer>().material.mainTexture == texture_list[10]) ? texture_list[11] : texture_list[10];
                }
                frame++;
            }   
        }

    }
}
