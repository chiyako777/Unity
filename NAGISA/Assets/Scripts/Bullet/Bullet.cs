using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Vector3 velocity = new Vector3(0.0f,0.0f,0.0f);
    public Vector3 gravity = new Vector3(0.0f,1.0f,0.0f);

    void Start()
    {        
    }

    void Update()
    {
        transform.position += velocity + gravity;
    }
}

