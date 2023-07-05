using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViolentoAeronautico : Enemy
{
    private Vector2 targetPosition;
    public float Speed = 10f;
    public float timeToCharge = 1f;
    private bool charge = false;

    void Start()
    {
       targetPosition =(Vector2)(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position) * 10;
       // targetPosition =(Vector2)(GameObject.FindGameObjectWithTag("Player").transform.position) * 10;
        StartCoroutine(ChargeTimer());
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        if(charge)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Speed/10);
           // transform.position = Vector2.MoveTowards( transform.position,targetPosition , Time.deltaTime * Speed/100);
        }     
    }

    private IEnumerator ChargeTimer()
    {
        yield return new WaitForSeconds(timeToCharge);
        charge = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<NaveController>().LooseLife(1);
            Destroy(gameObject);
        }
    }
}
