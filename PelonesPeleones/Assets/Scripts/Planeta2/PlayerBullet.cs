using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [HideInInspector]public Vector3 target;
    public float Speed = 5f;
    private float step;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
    }

    void Update()
    {
        step = Speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position,target,step * 2.5f);

        if((Vector2)transform.position == (Vector2)target)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Topo")
        {
            audioManager.Play("Muerte_Blasto");
            col.gameObject.GetComponent<Topo>().Die();
            Destroy(gameObject);
        }
        
        if(col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<GloboAerostatico>().Die();
            Destroy(gameObject);
        }

        if(col.gameObject.tag == "TopoBullet")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
