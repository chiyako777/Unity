using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharaTipController : MonoBehaviour
{
    private float speed = 1.0f;
    private Rigidbody2D rb;
    private Vector2 inputAxis;

    private Vector2 topLeft = new Vector2(-4.72f,1.96f);
    private Vector2 bottomRight = new Vector2(4.82f,-2.09f);

    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        if(!(bool)FlagManager.flagDictionary["coroutine"]){
            //Debug.Log("歩けるはずだよ");
            inputAxis.x = Input.GetAxis("Horizontal");
            inputAxis.y = Input.GetAxis("Vertical");            
        }else{
            //Debug.Log("会話イベント中なので移動不可");
        }
    }

    void FixedUpdate()
    {
        rb.velocity = inputAxis.normalized * speed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,topLeft.x,bottomRight.x),
                                            Mathf.Clamp(transform.position.y,bottomRight.y,topLeft.y),
                                            0.0f);
    }

}
