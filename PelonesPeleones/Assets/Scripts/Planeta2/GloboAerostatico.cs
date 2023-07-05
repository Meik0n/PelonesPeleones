using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloboAerostatico : MonoBehaviour
{

    public float Speed = 2f;
    public float shootTime = 3f;
    public GameObject bulletPrefab;
    private GameObject player;
    private bool isPlayerRight;
    private Vector2 screenBounds;
    private ToposManager manager;   
    private Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        transform.position = new Vector3(gameObject.GetComponentInParent<Transform>().position.x,gameObject.GetComponentInParent<Transform>().position.y,10);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        player = GameObject.FindGameObjectWithTag("Player");
        manager = GameObject.FindGameObjectWithTag("ToposManager").GetComponent<ToposManager>();
        if(player.transform.position.x < transform.position.x)
        {
            isPlayerRight = false;
        }
        else{isPlayerRight = true;}

        StartCoroutine(Shoot());
    }

    void Update()
    {
        if(isPlayerRight)
        {
            transform.position += new Vector3(Speed * Time.deltaTime,0,0);
            if(transform.position.x > screenBounds.x)
            {
                Die();
            }
        }
        else if(!isPlayerRight)
        {
            transform.position -= new Vector3(Speed * Time.deltaTime,0,0);

            if(transform.position.x < -3)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        manager.numGlobos--;
    }

    private IEnumerator Shoot()
    {
        while(true)
        {
            animator.SetTrigger("Throw");
            Instantiate(bulletPrefab,transform.position,Quaternion.identity);
            yield return new WaitForSeconds(shootTime);
        }
    }
}
