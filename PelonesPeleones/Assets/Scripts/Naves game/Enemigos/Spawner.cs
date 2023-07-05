using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public GameObject Pasota;
    public GameObject Shooteador;
    public GameObject Violento;
    public GameObject ToXamen;
    private float Y;
    private float deltaPas = 0;
    private float deltaVio = 0;   
    private float deltaShoot = 0;
    private float deltaTox = 0;
    private float spawnTimePas;
    private float spawnTimeVio;
    private float spawnTimeShoot;
    //private float spawnTimeTox;
    public float[] ToxSpawnsTime;
    private int i = 0;
    void Start()
    {
        Y = GetComponent<SpriteRenderer>().bounds.size.y;
        //StartCoroutine(instantiateTox());
    }

    void Update()
    {
        InstantiatePasota();
        InstantiateViolento();
        InstantiateShooteador();
        InstantiateTox();
        deltaPas += Time.deltaTime;
        deltaVio += Time.deltaTime;
        deltaShoot += Time.deltaTime;
        deltaTox += Time.deltaTime;
    }

    private void InstantiatePasota()
    {
        if(deltaPas <= 0)
        {
            spawnTimePas = Random.Range(5f,7.1f);
        }
        else if(deltaPas >= spawnTimePas)
        {
            Instantiate
            (
                Pasota,
                new Vector3
                    (
                    transform.position.x, 
                    Random.Range(-Y,Y),
                    0
                    ),
                    Quaternion.identity
            );

            deltaPas = -0.1f;
        }
    }

    private void InstantiateViolento()
    {
        if(deltaVio <= 0)
        {
            spawnTimeVio = Random.Range(10f,13.1f);
        }
        else if(deltaVio >= spawnTimeVio)
        {
            Instantiate
            (
                Violento,
                new Vector3
                    (
                    transform.position.x, 
                    Random.Range(-(Y+2),Y+2),
                    0
                    ),
                    Quaternion.identity
            );

            deltaVio = -0.1f;
        }
    }

    private void InstantiateShooteador()
    {
        if(deltaShoot <= 0)
        {
            spawnTimeShoot = Random.Range(7,10.1f);
        }
        else if(deltaShoot >= spawnTimeShoot)
        {
            Instantiate
            (
                Shooteador,
                new Vector3
                    (
                    transform.position.x, 
                    Random.Range(-Y,Y),
                    0
                    ),
                    Quaternion.identity
            );

            deltaShoot = -0.1f;
        }
    }

    private void InstantiateTox()
    {
        try
        {
            if((int)deltaTox == (int)ToxSpawnsTime[i])
            {
                Instantiate
                (ToXamen,
                transform.position,
                Quaternion.identity);

                i++; 
            }
        }catch
        {
            return;
        }
       

    }

/*
    internal IEnumerator instantiateTox()
    {
        yield return new WaitForSeconds(timeToTox);
        Instantiate(ToXamen,transform.position,Quaternion.identity);
        yield break;
    }
    */
}
