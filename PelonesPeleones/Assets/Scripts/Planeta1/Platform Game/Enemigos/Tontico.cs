using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tontico : Enemy
{
    public Transform[] points;
    public float speed = 1f;
    public bool cerrado = true;
    private int currentIndex  = -1;
    private Transform currentPoint;
    private bool movingUp = true;
    void Start()
    {
        currentPoint = points[0];   
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if(transform.position != currentPoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, speed* Time.deltaTime);
            if(transform.position.x < currentPoint.position.x)
            {
                if(!facingRight)
                {
                    Flip();
                }
            }
            else if(transform.position.x > currentPoint.position.x)
            {
                if(facingRight)
                {
                    Flip();
                }
            }
        }
        else
        {
            CalculateNextPoint();
        }


    }

    void CalculateNextPoint()
    {
        if(cerrado)
        {
            currentIndex++;
            if(currentIndex >= points.Length)
            {            
                currentIndex = -1;            
            }
            else if (currentIndex < points.Length)
            {
                currentPoint = points[currentIndex];
            }    
        }

        if(!cerrado)
        {
            if(movingUp)
            {
                currentIndex++;

                if(currentIndex >= points.Length)
                {
                    currentIndex--;            
                    movingUp = false;         
                }
                else if(currentIndex < points.Length)
                {
                    currentPoint = points[currentIndex];
                }
            }

            if(!movingUp)
            {
                currentIndex--;

                if(currentIndex >= 0)
                {
                    currentPoint = points[currentIndex];
                }
                else if(currentIndex < 0)
                {
                    currentIndex++;
                    movingUp = true;
                }
            }
        }
    }
}
