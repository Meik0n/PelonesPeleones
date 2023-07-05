using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 20f;
    private Rigidbody2D rb;
    public float DestroyTime = 1f;
    public GameObject burstParticle;
    private AudioManager audioManager;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        gameObject.transform.SetParent(null);
        Destroy(gameObject, DestroyTime);
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D hitInfo)
    {         
        if(hitInfo.gameObject.tag != "Player" && hitInfo.gameObject.tag != "Seta")
        {          
            Instantiate(burstParticle,transform.position,Quaternion.identity);      
            Destroy(gameObject);                  
        }    

        if(hitInfo.gameObject.tag == "Enemy")
        {
            hitInfo.gameObject.GetComponent<Enemy>().TakeDamage(1);
            if(audioManager)
            {
                audioManager.Play("BalaGolpeaEnemigo");
            }        
        }
        if(hitInfo.gameObject.tag == "Ground")
        {
            if(audioManager)
            {
                audioManager.Play("BalaGolpeaPared");
            }
        }
    }
}
