using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraTransition : MonoBehaviour
{

    public GameObject cameraActive;
    public GameObject cameraToActive;

    void Update()
    {
        //Debug.Log("vcam1:" + cameraActive.activeSelf);
       // Debug.Log("vcam2:" + cameraToActive.activeSelf);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(cameraActive.activeSelf && !cameraToActive.activeSelf)
            {
                cameraActive.SetActive(false);
                cameraToActive.SetActive(true);
            }          
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {          
        if(other.gameObject.tag == "Player")
        {
            if(!cameraActive.activeSelf && cameraToActive.activeSelf)
            {
                cameraActive.SetActive(true);
                cameraToActive.SetActive(false);
            }          
        }
    }
}
