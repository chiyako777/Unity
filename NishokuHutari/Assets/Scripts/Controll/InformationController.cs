using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationController : MonoBehaviour
{
    [SerializeField]
    private Text remainTurnText;
    [SerializeField]
    private Image kimotiImage;

    private IEnumerator kimotiCoroutine;

    void Start()
    {
        kimotiImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if(!Manager.initFlg){
            return;
        }

        //** 残ターン数
        remainTurnText.text = "Remain : " + Manager.userData.remainTurn;

        //** 気持ち表示
        if(Input.GetButtonDown("Jump") 
            && !(bool)FlagManager.flagDictionary["coroutine"]
            && kimotiCoroutine == null){
                Debug.Log("気持ち表示");

                //** 選択肢正解が白：posi 黒：nega 選択肢なし：posi                
                bool negaPosi = true;
                GameObject wp = GameObject.FindWithTag("WarpPoint");
                WarpSelect ws = wp.GetComponent<WarpSelect>();
                if(ws != null){
                    foreach(SelectData select in Manager.generalData.selectData){
                        if(ws.selectId == select.selectId && select.answer == "black"){
                            Debug.Log("negaPosi:black");
                            negaPosi = false;
                        }
                    }
                }

                kimotiCoroutine = CreateKimotiCoroutine(negaPosi);
                StartCoroutine(kimotiCoroutine);
        }

    }

    private IEnumerator CreateKimotiCoroutine(bool negaPosi){
        yield return OnKimotiCoroutine(negaPosi);
        StopCoroutine(kimotiCoroutine);
        Debug.Log("気持ち表示終了");
        kimotiCoroutine = null;
    }

    private IEnumerator OnKimotiCoroutine(bool negaPosi){

        //** スプライト設定
        int status = 1;
        kimotiImage.gameObject.SetActive(true);
        kimotiImage.sprite = (negaPosi) ? Manager.spriteLoader.GetObjectHandle("test_KimotiOK") 
                                        : Manager.spriteLoader.GetObjectHandle("test_KimotiNG");
        kimotiImage.color = new Color(0.86f,0.48f,0.48f,0.0f);
        yield return null;

        //** スプライトアルファ調整
        while(status < 4){
            if(status == 1){
                while(kimotiImage.color.a <= 0.5f){
                    float alpha = kimotiImage.color.a + 0.003f;
                    kimotiImage.color = new Color(0.86f,0.48f,0.48f,alpha);
                    yield return null;
                }
                status = 2;
            }
            if(status == 2){
                yield return new WaitForSeconds(0.5f);
            }
            status = 3;
            if(status == 3){
                while(kimotiImage.color.a >= 0.0f){
                    float alpha = kimotiImage.color.a - 0.003f;
                    kimotiImage.color = new Color(0.86f,0.48f,0.48f,alpha);
                    yield return null;
                }
            }
            status = 4;
        }

        kimotiImage.gameObject.SetActive(false);
        yield break;

    }
}
