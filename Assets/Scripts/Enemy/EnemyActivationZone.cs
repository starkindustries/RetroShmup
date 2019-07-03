using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivationZone : MonoBehaviour
{
    //     
    // Notes: Trigger events are only sent if one of the Colliders also has a Rigidbody attached. 
    // Trigger events will be sent to disabled MonoBehaviours, to allow enabling Behaviours in 
    // response to collisions. OnTriggerEnter occurs on the FixedUpdate after a collision. The 
    // Colliders involved are not guaranteed to be at the point of initial contact.    
    // https://docs.unity3d.com/ScriptReference/Collider.OnTriggerEnter.html
    // 
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        Debug.Log("Activation zone: trigger detected with " + collision.name);
        // if object is a wave spawner object, start wave
        Activatable gameObject = collision.GetComponent<Activatable>();
        if (gameObject == null)
        {
            return;
        }
        gameObject.Activate();
    }
}
