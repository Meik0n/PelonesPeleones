using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloboSpawner : MonoBehaviour
{
    public GameObject globoPrefab; 
    private ToposManager manager;   

    void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("ToposManager").GetComponent<ToposManager>();
    }

    void Start()
    {
        StartCoroutine(Spawn());
    }
    void Update()
    {
        
    }

    private IEnumerator Spawn()
    {
        while(true)
        {
            if(manager.numGlobos<1)
            {
                Instantiate(globoPrefab,transform.position,Quaternion.identity);
                manager.numGlobos++;
            }

            yield return new WaitForSeconds(Random.Range(5,10));
        }

    }
}
