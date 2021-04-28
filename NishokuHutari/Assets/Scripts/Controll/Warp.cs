using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    protected bool isContacted = false;

    void Start()
    {
        
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        //Debug.Log("TriggerEnter");
        isContacted = collider.gameObject.tag.Equals("Player");
    }

    private void OnTriggerExit2D(Collider2D collider) {
        //Debug.Log("TriggerExit");
        isContacted = !collider.gameObject.tag.Equals("Player");
    }
}
