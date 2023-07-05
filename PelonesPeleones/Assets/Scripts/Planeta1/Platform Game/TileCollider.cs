using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileCollider : MonoBehaviour
{
    private BoxCollider2D col;
    private SpriteRenderer spr;
    void Start()
    {
      
    }

    void Update()
    {
        //Vector2 S = gameObject.GetComponent<SpriteRenderer>().size;
        //gameObject.GetComponent<BoxCollider2D>().size = S;
        //gameObject.GetComponent<BoxCollider2D>().offset = new Vector2 ((S.x / 2), 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.layer);
    }
}
