using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Package : MonoBehaviour
{
    public float timeAlive = 10f;
    private float timeToDie;
    public Image hpBar;
    public float initialForce = 5f;
    private Rigidbody2D rb;
    private Vector2 randomDir;
    public EnvialiaInputManager manager;
    private AudioManager audioManager;
    private Animator animator;
    public Canvas barCanvas;
    private bool scaled = false;
    private Pause lvlManager;
    
    void Awake()
    {
        lvlManager = GameObject.FindGameObjectWithTag("Lvlmanager").GetComponent<Pause>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        timeToDie = timeAlive;
        audioManager = AudioManager.instance;

        var v1 = Random.Range(-1f,1f);
        var v2 = Random.Range(0,1f);
        randomDir = new Vector2(v1,v2);

        rb.AddForce(randomDir.normalized * initialForce,ForceMode2D.Force);
    }

    void Update()
    {
        timeToDie -= Time.deltaTime;
        hpBar.fillAmount = timeToDie/timeAlive;

        if(timeToDie <= 0)
        {
            Die();
        }

        if(lvlManager.paused == true)
        {
            this.gameObject.SetActive(false);
        }
        else{this.gameObject.SetActive(true);}
    }

    void OnTriggerEnter2D(Collider2D  col)
    {
        if(col.tag == "RedArea" && this.tag == "RedPackage" || col.tag == "GreenArea" && this.tag == "GreenPackage")
        {
            rb.isKinematic = true;
            gameObject.layer = 0;
            rb.velocity = new Vector2(0,0);
            timeToDie = Mathf.Infinity;
            gameObject.GetComponent<Collider2D>().enabled = false;
            barCanvas.gameObject.SetActive(false);
            audioManager.Play("Paquete_Recogido");
        }

        if(col.CompareTag("RedArea") && this.CompareTag("GreenPackage") ||col.CompareTag("GreenArea") && this.CompareTag("RedPackage"))
        {
            audioManager.Play("Paquete_Roto");
            SceneManager.LoadScene("EnvialiaDeath");
        }
    }

    private void Die()
    {
        animator.SetTrigger("destroy");
        audioManager.Play("Paquete_Roto");
        if(!scaled)
        {
            transform.localScale *= 1.3f;
            scaled = true;
        }
        rb.velocity = Vector2.zero;
        gameObject.layer = 0;
        StartCoroutine(LoseGame());
    }

    private IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("EnvialiaDeath");
    }
}
