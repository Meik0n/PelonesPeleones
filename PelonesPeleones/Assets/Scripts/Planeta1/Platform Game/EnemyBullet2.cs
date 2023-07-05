using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet2 : MonoBehaviour {

    public float speed = 20f;
    private Rigidbody2D rb;
    public float DestroyTime = 1f;
    public GameObject burstParticle;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();    
        rb.velocity = -transform.right * speed;       
        Destroy(gameObject, DestroyTime);     
    }

    private void OnCollisionEnter2D(Collision2D hitInfo)
    {         
        if(hitInfo.gameObject.tag != "Enemy")
        {          
            Instantiate(burstParticle,transform.position,Quaternion.identity);      
            Destroy(gameObject);                  
        }    
    }
}
