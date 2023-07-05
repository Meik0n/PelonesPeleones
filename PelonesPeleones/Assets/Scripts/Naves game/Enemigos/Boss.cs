using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private GameObject bossDestination;
    public float Speed = 5f;
    private ManagerScene sceneManager;
    [HideInInspector]public int destroyedEyes = 0;
    void Start()
    {
        bossDestination = GameObject.FindGameObjectWithTag("Seta");
        sceneManager = GameObject.FindGameObjectWithTag("Lvlmanager").GetComponent<ManagerScene>();
    }

    void Update()
    {
        if(transform.position != bossDestination.transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, bossDestination.transform.position, Speed * Time.deltaTime);
        }
        
        if(destroyedEyes == 3)
        {
            sceneManager.LoadVideo("9");
        }
    }
}
