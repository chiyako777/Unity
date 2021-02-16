using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();

    //is TriggerがONで、他のColliderと重なっているときは、このメソッドが常にコールされる
    private void OnTriggerStay(CollisionDetector other){
        onTriggerStay.Invoke(other);
    }

    // [Serializable]
    // public class TriggerEvent : UnityEvent<CollisionDetector>{

    // }

}
