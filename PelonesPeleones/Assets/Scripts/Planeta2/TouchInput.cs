using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    private Camera cam;
    public GameObject player;
    public GameObject bullet;
    public float ShootCD = 0.1f;
    private bool isWorking = false;
    private AudioManager audioManager;
    void Start()
    {
        cam = GetComponent<Camera>();
        audioManager = AudioManager.instance;

        audioManager.StopAllSounds();
        audioManager.Play("Music_Topos");
    }

   
    void Update()
    {
//en el editor
//#if UNITY_EDITOR

        if(Input.GetMouseButtonDown(0))
        {
            bullet.GetComponent<PlayerBullet>().target = (cam.ScreenToWorldPoint(Input.mousePosition)- player.transform.position) * 10;
            audioManager.Play("Disparo_Baby");
            Instantiate(bullet,player.transform.position,Quaternion.identity);
        }

//#endif

/*
//dispositivos moviles
#if UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            if(!isWorking)
            {
                
                foreach(Touch touch in Input.touches)
                {
                    bullet.GetComponent<PlayerBullet>().target = (cam.ScreenToWorldPoint(Input.touches[0].position) - player.transform.position ) * 10;
                    Instantiate(bullet,player.transform.position,Quaternion.identity); 
                    audioManager.Play("Disparo_Baby");
                    StartCoroutine(CD());
                }
            }            
        }   
#endif    
*/   
    }

    private IEnumerator CD()
    {
        float init = 0;
        isWorking = true;

        while(true)
        {
            init += Time.deltaTime;
            if(init>=ShootCD)
            {
                isWorking = false;
                yield break;
            }

            yield return null;
        }
    }
}
