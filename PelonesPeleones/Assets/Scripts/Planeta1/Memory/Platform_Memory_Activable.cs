using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Memory_Activable : MonoBehaviour
{
    private Vector3 startPos;
    public Transform target;
    public float speed;
    public float waitTime = 1f;
    private bool moveUp;
    private bool movementAllowed = true;
    public bool isActive = false;
    void Start()
    {
        startPos = transform.position;
    }


    void Update()
    {
        if(isActive)
        {
            float step = speed * Time.deltaTime;

            if(movementAllowed)
            {
                if (transform.position == target.position)
                {
                    moveUp = false;
                    StartCoroutine(platformStopper());
                }
                else if (transform.position == startPos)
                {
                    moveUp = true;
                    StartCoroutine(platformStopper());
                }
                if(!moveUp)
                {
                    transform.position = Vector3.MoveTowards (transform.position, startPos, step);
                }
                else if (moveUp)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                }
            }
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }

    IEnumerator platformStopper()
    {
        movementAllowed = false;
        yield return new WaitForSeconds(waitTime);
        movementAllowed = true;
    }
}
