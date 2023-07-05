using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baboso : Enemy
{
    
    public GameObject bullet;
    public Transform ShootPoint; 
    public float ShootRate = 1f;
    private PlayerController player;
    public float distanceToSound = 50f;

    private Animator anim;
    void Start()
    {       
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while(true)
        {
            if(audioManager)
            {
                if(Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position) <= distanceToSound)
                {
                    audioManager.Play("Escupitajo");
                }
            }
            anim.SetTrigger("disparar");
            Instantiate(bullet, ShootPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(ShootRate);
        }
    }
}