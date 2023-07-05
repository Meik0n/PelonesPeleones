using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    public float bounciness; // Determines jump height
 
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.rigidbody.AddForce(/*new Vector2(-other.relativeVelocity.x, other.relativeVelocity.y) */Vector2.up * bounciness, ForceMode2D.Impulse);
        }          
    }
}
