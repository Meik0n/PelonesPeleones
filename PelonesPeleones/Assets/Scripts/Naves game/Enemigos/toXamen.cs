using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toXamen : Enemy
{
    private Vector2 screenBounds;
    private float objectHeight;
    public float Speed = 5f;
    public float spawnSpeed = 10f;
    [Range(0, 10 )] public float xPosition = 0.00f;
    private float Step = 1f;
    private float SpawnStep = 1f;
    private float delta = 0f;
    public Transform shootPosition;
    public float shootCd = 3f;
    public float chargingShootTime = 2f;
    public GameObject chargeParticle;
    //private bool movingUp = true;
    public GameObject bullet;
    private GameObject player;

    private Animator anim;

    protected enum States
    {
        positioning, idleing
    }protected States currentState = States.positioning;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectHeight = GetComponent<SpriteRenderer>().bounds.size.y/2;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        StartCoroutine(goPosition());
    }

    void Update()
    {
        SpawnStep = spawnSpeed  * Time.deltaTime;
        Step = Speed * Time.deltaTime;
        delta += Time.deltaTime;
        if(delta >= shootCd)
        {
            delta = 0;
        }
    }

    protected IEnumerator goPosition()
    {
        currentState = States.positioning;
        Vector2 targetPosition = new Vector2(xPosition, 0);
        while(currentState == States.positioning)
        {
            transform.position = Vector2.MoveTowards(transform.position,targetPosition,SpawnStep);
            if(transform.position.x == targetPosition.x)
            {
                StartCoroutine(Idle());
            }
            yield return null;
        }
    }

    protected IEnumerator Idle()
    {
        delta = 0;
        currentState = States.idleing;

        while(currentState == States.idleing)
        {
            Vector2 pos = new Vector2(xPosition, player.transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position,pos,Step);


            if(delta >= shootCd-0.05)
            { 
                anim.SetTrigger("disparo");
                StartCoroutine(ChargeShoot());
            }   
            yield return null;        
        }
        yield break;
    }

    protected IEnumerator ChargeShoot()
    {      
        Instantiate(chargeParticle, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(chargingShootTime);
        Instantiate(bullet, shootPosition.position, Quaternion.identity);
        delta = 0;
        yield break;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(xPosition,-10,0), new Vector3(xPosition,10,0));
    }
}
