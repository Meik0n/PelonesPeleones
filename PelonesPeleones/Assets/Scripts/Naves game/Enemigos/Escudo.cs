using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{
    public float Speed;
    private bool goingUp = true;
    private Vector2 screenBounds;
    private float objectHeight;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectHeight = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y/2;
    }

    void Update()
    {
        if(goingUp)
        {
            transform.position += new Vector3(0, Speed * Time.deltaTime,0);
        }
        else if(!goingUp)
        {
            transform.position += new Vector3(0,-Speed * Time.deltaTime,0);
        }

        if(transform.position.y + objectHeight > screenBounds.y)
        {
            goingUp = false;
        }
        else if(transform.position.y - objectHeight < -screenBounds.y)
        {
            goingUp = true;
        }
    }
}
