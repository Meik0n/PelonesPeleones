using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public int health;
    public int ContactDamage = 1;
    protected bool facingRight = false;
    protected AudioManager audioManager;
    public GameObject particleDeath;

    protected void Awake()
    {
      audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

     protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          collision.gameObject.GetComponent<PlayerController>().LooseLife(ContactDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {

          if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Nivel1")
          || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Nivel2") 
          || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Nivel 3"))
          {
            audioManager.Play("MuerteBlasto");
                Instantiate(particleDeath, transform.position, Quaternion.identity);
          }

          else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("NaveGame01"))
          {
            audioManager.Play("Destruccion_enemies");
          }        
          Die();
        }
    }

  

     protected void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    protected void Die()
    {
      Destroy(gameObject);
    }
}
