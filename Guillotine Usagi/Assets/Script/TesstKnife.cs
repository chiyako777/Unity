using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesstKnife : MonoBehaviour
{
    public string DIR;  //center,left,right
    private static ResourcesLoader<GameObject> resourcesLoader = new ResourcesLoader<GameObject>();
    private bool flg = false;

    //** cache
    private Rigidbody rigidbody;

    void Start()
    {
        resourcesLoader.LoadAllObjects("Prefabs");

        switch(DIR){
            case "center" :
                Instantiate(resourcesLoader.GetObjectHandle("Knife"),new Vector3(-3.5f,3.0f,0.0f),Quaternion.AngleAxis(90.0f,Vector3.up));
                break;
            case "left" :
                break;
            case "right" :
                break;
            default :
                break;
        }

        GameObject knife = GameObject.Find("Knife(Clone)");
        //Debug.Log("knife = " + knife);
        if(knife != null){rigidbody = knife.GetComponent<Rigidbody>();}

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            flg = true;
        }     
    }

    void FixedUpdate(){
        if(flg){
            switch(DIR){
                case "center" :
                    rigidbody.AddForce(new Vector3(0.0f,-1.0f,0.0f),ForceMode.Impulse);
                    break;
                case "left" :
                    break;
                case "right" :
                    break;
                default :
                    break;
            }
        }
    }

}
