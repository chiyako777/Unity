using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//** タイトル画面コントローラー
public class Title : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetButtonDown("Submit")){
            Debug.Log("Mapへ遷移");
            SceneManager.LoadScene("Map");
        } 
    }
}
