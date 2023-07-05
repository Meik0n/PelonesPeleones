using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopoBullet : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject player;
    [SerializeField] private float Speed = 5f;
    private ToposManager manager;

    void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("ToposManager").GetComponent<ToposManager>();
    }
    void Start()
    {
        int r = Random.Range(0,sprites.Length);

        if(sprites != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = sprites[r];
        }     
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
        if(transform.position == player.transform.position)
        {
            manager.LooseLife();
            Destroy(gameObject);
        }
    }
}
