using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToEnding : MonoBehaviour
{
    private bool isContacted = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isContacted &&
             Input.GetButton("Submit") && Input.anyKeyDown &&
             !(bool)FlagManager.flagDictionary["coroutine"]) {

            SceneManager.sceneLoaded += OnEndLoaded;
            SceneManager.LoadScene("NAGISA");

        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        isContacted = collider.gameObject.tag.Equals("Player");
    }

    private void OnTriggerExit2D(Collider2D collider) {
        isContacted = !collider.gameObject.tag.Equals("Player");
    }

    //** NAGISAシーン遷移後呼ばれる
    private void OnEndLoaded(Scene nextScene,LoadSceneMode mode){
        Debug.Log("OnEndLoaded:エンディング準備");
        Ending ending = GameObject.Find("Ending").GetComponent<Ending>();
        ending.progress = 1;
        SceneManager.sceneLoaded -= OnEndLoaded;

    }

}
