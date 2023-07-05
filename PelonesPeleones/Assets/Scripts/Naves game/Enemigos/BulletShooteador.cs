using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooteador : MonoBehaviour
{
    private Vector2 target;
    public float Speed = 5f;
    void Start()
    {
        target =(Vector2)(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position) * 10;   
    }
    
    void Update()
    {
        //transform.position = (Vector2)transform.position + target * Speed/100 * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, Speed/10);
        if((Vector2)transform.position == target)
        {
            Destroy(gameObject);
        }
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
