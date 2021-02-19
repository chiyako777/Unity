using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharaTipController : MonoBehaviour
{
    [SerializeField]
    float speed = 1.0f;
    private Rigidbody2D rb;
    private Vector2 inputAxis;

    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        if(!(bool)FlagManager.flagDictionary["coroutine"]){
            inputAxis.x = Input.GetAxis("Horizontal");
            inputAxis.y = Input.GetAxis("Vertical");
        }else{
            //Debug.Log("会話イベント中なので移動不可");
        }
    }

    void FixedUpdate()
    {
        rb.velocity = inputAxis.normalized * speed;
    }

}
