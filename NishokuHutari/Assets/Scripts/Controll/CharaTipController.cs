using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharaTipController : MonoBehaviour
{
    private float speed = 2.0f;
    private Rigidbody2D rb;
    private Vector2 inputAxis;

    private Vector2 topLeft = new Vector2(-4.72f,3.2f);
    private Vector2 bottomRight = new Vector2(4.82f,-3.28f);

    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        if(!(bool)FlagManager.flagDictionary["coroutine"]){
            inputAxis.x = Input.GetAxis("Horizontal");
            inputAxis.y = Input.GetAxis("Vertical");            
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
