using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PackageSpawner : MonoBehaviour
{
    public GameObject package1;
    public GameObject package2;
    public float initialSpawnTime = 2f;
    public float minSpawnTime = 0.1f;
    private float spawnTime;
    public float increaseSpawntimeTime = 5f;

    void Start()
    {
        spawnTime = initialSpawnTime;
        StartCoroutine(Spawn());
        StartCoroutine(ReduceSpawnTime());
    }

    void Update()
    {

    }

    private IEnumerator Spawn()
    {
        while(true)
        {
            var r = Random.Range(1,3);
            if(r%2 == 0)
            {
                Instantiate(package1,transform.position,Quaternion.identity);
            }
            else
            {
                Instantiate(package2,transform.position,Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }

    private IEnumerator ReduceSpawnTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(increaseSpawntimeTime);
            spawnTime /=2;
            if(spawnTime <= minSpawnTime)
            {
                spawnTime = minSpawnTime;
            }
        }
    }
}
