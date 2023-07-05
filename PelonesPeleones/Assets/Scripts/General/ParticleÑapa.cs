using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleÑapa : MonoBehaviour
{
    private GameObject position;
    void Start()
    {
        position = GameObject.FindGameObjectWithTag("ShootPoint");
    }
    void Update()
    {
        transform.position = position.transform.position;
    }
}
