using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pasota : Enemy
{ 
    private Rigidbody2D m_RigidBody2D;   
    public float Speed = 20f;
    protected override void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          collision.gameObject.GetComponent<NaveController>().LooseLife(ContactDamage);
          Destroy(gameObject);
        }
    }
    void Start()
    {
        m_RigidBody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        m_RigidBody2D.velocity = new Vector2(-Speed * Time.fixedDeltaTime, 0);
    }
}
