using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Platform_Memory : MonoBehaviour
{
    public GameObject panel;
    private AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //gameObject.GetComponentInChildren<Button>().interactable = true;
            audioManager.Play("Memory_On");
            panel.SetActive(true);
            panel.GetComponent<Button>().interactable = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //gameObject.GetComponentInChildren<Button>().interactable = false;
            panel.SetActive(false);
            
           // gameObject.GetComponentInChildren<Image>().gameObject.SetActive(true);
           // gameObject.GetComponentInChildren<Image>().GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(true);
        }
    }
}
