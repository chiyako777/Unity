using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{
    //** component
    private Text optionText;
    private Text roomText;
    private Text spellText;
    private Text powerText;

    public GameObject[] lifeCount;
    public GameObject[] bombCount;

    [HideInInspector]
    public string option;
    [HideInInspector]
    public string room;
    [HideInInspector]
    public int life;
    [HideInInspector]
    public int bomb;
    [HideInInspector]
    public float power;

    //** caches
    private Enemy enemy;
    private Image[] lifeCountImage = new Image[10];
    private Image[] bombCountImage = new Image[10];

    void Start()
    {
        //** コンポーネント取得
        Text[] text = GameObject.FindObjectsOfType<Text>();
        foreach(Text t in text){
            switch(t.name){
                case "bulletinfo_option_text":
                    optionText = t;
                    break;
                case "bulletinfo_room_text":
                    roomText = t;
                    break;
                case "bulletinfo_spell_text":
                    spellText = t;
                    break;
                case "bulletinfo_powerval_text":
                    powerText = t;
                    break;
                default:
                    break;
            }
        }

        //** 各種キャッシュ
        for(int i=0; i<10; i++){
            lifeCountImage[i] = lifeCount[i].GetComponent<Image>();
            bombCountImage[i] = bombCount[i].GetComponent<Image>();
        }

        //** 自機タイプ、ルーム情報はシーン遷移元から取得したデータを設定
        optionText.text = option;
        roomText.text = room;
    }

    void Update()
    {
        //** 敵機をキャッシュ
        LoadEnemy();

        //** スペル名表示
        if(enemy != null){
            spellText.text = enemy.enemyInfo.spellName;
        }else{
            spellText.text = "・・・";
        }

        //** 残機数表示
        DispLifeCount();
        //** ボム数表示
        DispBombCount();
        //** パワー表示
        DispPower();

    }

    private void LoadEnemy(){
        if(enemy == null){
            GameObject obj = GameObject.FindGameObjectWithTag("Enemy_Bullet");
            if(obj != null){
                enemy = obj.GetComponent<Enemy>();
            }
        }
    }

    //** 残機数アイコン表示
    private void DispLifeCount(){

        for(int i=1; i<=10; i++){
            if(i <= life){
                lifeCountImage[i-1].color = new Color(1.0f,1.0f,1.0f,1.0f);
            }else{
                lifeCountImage[i-1].color = new Color(1.0f,1.0f,1.0f,0.35f);
            }
        }

    }

    //** ボム数アイコン表示
    private void DispBombCount(){

        for(int i=1; i<=10; i++){
            if(i <= bomb){
                bombCountImage[i-1].color = new Color(1.0f,1.0f,1.0f,1.0f);
            }else{
                bombCountImage[i-1].color = new Color(1.0f,1.0f,1.0f,0.2f);
            }
        }

    }

    //** パワー表示
    private void DispPower(){
        powerText.text = power.ToString("F1") + " / 5.0";
    }

}
