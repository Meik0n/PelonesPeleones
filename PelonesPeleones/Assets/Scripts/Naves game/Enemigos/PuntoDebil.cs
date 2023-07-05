using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoDebil : MonoBehaviour
{
    public int Life = 10;
    public Escudo escudo;
    public float escudoSpeedAument = 3f;
    public GameObject destroyedSprite;
    public Boss boss;
    void Start()
    {
        
    }

    void Update()
    {
        if(Life <= 0)
        {
            escudo.Speed += escudoSpeedAument;
            Die();
        }
    }

    private void Die()
    {
        boss.destroyedEyes ++;
        destroyedSprite.SetActive(true);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "NaveBullet")
        {
            Life--;
        }
    }
}
