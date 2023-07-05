using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooteador : Enemy
{

    public float speed = 10f;
    private Vector2 targetPosition;
    private float randomX = 0;
    private float randomY = 0;
    public Transform shootPosition;
    public float shootRate = 1f;
    public GameObject bullet;
    public Sprite bossSprite;
    private Scene currentScene;

    private Animator anim;

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<NaveController>().LooseLife(ContactDamage);
        }
    }

    protected IEnumerator Shoot()
    {
        while(true)
        {
            if((Vector2)transform.position == targetPosition)
            {
                Instantiate(bullet, shootPosition.position, Quaternion.identity);
                anim.SetBool("disparo", true);
                yield return new WaitForSeconds(1f);
                anim.SetBool("disparo", false);
                yield return new WaitForSeconds(shootRate - 1);
            }
            else
            {
                yield return null;
            }
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        currentScene = SceneManager.GetActiveScene();

        if(currentScene.name != "Boss")
        {
            randomX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - (Screen.width/3), 0)).x, 
                Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

            randomY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        }
        else if(currentScene.name == "Boss")
        {
            randomX = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - (Screen.width/3), 0)).x, 
                Camera.main.ScreenToWorldPoint(new Vector2((Screen.width - bossSprite.rect.width) + Screen.width/9 , 0)).x);

            randomY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        }

 
        targetPosition = new Vector2(randomX,randomY);
        StartCoroutine(Shoot());
    }


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed*Time.deltaTime);
    }
}
