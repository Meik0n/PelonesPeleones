using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryParticles : MonoBehaviour
{

    public GameObject targetPosition;
    public float speed = 5f;
    void Start()
    {
        StartCoroutine(Move(transform.position,targetPosition.transform.position,speed));
    }


    void Update()
    {
        
    }


    public IEnumerator Move(Vector2 a, Vector2 b, float speed)
    {
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; 
            transform.position = Vector3.Lerp(a, b, t); 
            yield return new WaitForFixedUpdate(); 
            transform.position = b;
        }
    }
}
