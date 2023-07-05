using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topo : MonoBehaviour
{
    [SerializeField] private float throwTime;
    [SerializeField] private float waitTime;
    [SerializeField] private GameObject bullet;
    private ToposManager manager;
    private float timeAlive = 0;
    public float timeToBeAlive = 3f;
    private bool shooted = false;
    private AudioManager audioManager;

    private Animator animator;
    void Awake()
    {
        audioManager = AudioManager.instance;
        animator = GetComponentInChildren<Animator>();
        manager = GameObject.FindGameObjectWithTag("ToposManager").GetComponent<ToposManager>();
    }

    void Update()
    {
        timeAlive += Time.deltaTime;
        if(timeAlive >= timeToBeAlive)
        {
            Die();
        }
        if(timeAlive >= 2f && shooted == false)
        {
            animator.SetTrigger("Throw");
            Throw();
            shooted = true;
        }
    }

    public void Throw()
    {
        Instantiate(bullet,transform.position,Quaternion.identity);
        audioManager.Play("Lanzamiento_Topos");
    }

    public void Die()
    {
        audioManager.Play("MuerteBlasto");
        Destroy(gameObject);
        manager.numTopos--;
        manager.numToposKilled++;
    }
}
