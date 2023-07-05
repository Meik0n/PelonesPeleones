using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Violento : Enemy
{
    public float radioDetection = 1f;
    public float waitTime = 1f;
    public float chaseSpeed = 5f;
    private Transform player;
    public LayerMask Player;
    public Animator anim;
 
    protected enum States
    {
        idleing, chasing
    }protected States currentState = States.idleing;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        StartCoroutine(Idle());
    }

    void Update()
    {
        
    }

    protected IEnumerator Idle()
    {
        currentState = States.idleing;
        anim.SetTrigger("Idleing");

        while(currentState == States.idleing)
        {
            Collider2D detector = Physics2D.OverlapCircle(transform.position, radioDetection, Player);
            if(detector)
            {
                anim.SetTrigger("Detection");
                audioManager.Play("Gruñido");
                yield return new WaitForSeconds(waitTime);
                StartCoroutine(Chase());
            }
            else
            {
                yield return null;
            }
        }

        yield return null;
    }

    protected IEnumerator Chase()
    {
        currentState = States.chasing;
        anim.SetTrigger("Chase");
        var playerPos = new Vector2(player.position.x, player.position.y);

        while(currentState == States.chasing)
        {
            if(player.position.x < transform.position.x)
            {
                if(facingRight)
                {
                    Flip();
                }
            }
            else if(player.position.x > transform.position.x)
            {
                if(!facingRight)
                {
                    Flip();
                }
            }

            if((Vector2)transform.position != playerPos)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos, chaseSpeed*Time.deltaTime);
            }
            else
            {
                StartCoroutine(Idle());
            }
            yield return null;
        }
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioDetection);
    }

}
