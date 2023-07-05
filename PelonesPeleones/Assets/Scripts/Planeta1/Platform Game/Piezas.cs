using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piezas : MonoBehaviour
{
    private LvlManager manager;
    public GameObject piezasParticles;
    private AudioManager audioManager;

    void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("Lvlmanager").GetComponent<LvlManager>();
        audioManager = AudioManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Instantiate(piezasParticles,transform.position,Quaternion.identity);
            audioManager.Play("Coleccionable");
            Debug.Log("Entro");
            manager.piezas++;
            Destroy(gameObject);
            
        }
    }
}
