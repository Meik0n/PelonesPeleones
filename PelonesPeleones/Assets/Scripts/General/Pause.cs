using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    AudioManager instance;
    private Scene scene;
    public Canvas pauseCanvas;
    [HideInInspector]public bool paused = false;
    void Start()
    {
        instance = AudioManager.instance;
    }
    public void PausarGame()
    {
        scene = SceneManager.GetActiveScene();
        //parar el tiempo
        Time.timeScale = 0;

        instance.MuteAll();

        pauseCanvas.gameObject.SetActive(true);

        paused = true;
        /*
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Envialia"))
        {
            GameObject[] GPackage = GameObject.FindGameObjectsWithTag("GreenPackage");
            GameObject[] RPackage = GameObject.FindGameObjectsWithTag("RedPackage");

            foreach( GameObject g in GPackage)
            {
                g.SetActive(false);
            }

            foreach( GameObject g in RPackage)
            {
                g.SetActive(false);
            }
        }  
        */     
    }

    public void ReanudarGame()
    {
        Time.timeScale = 1;
        instance.UnMuteAll();
        pauseCanvas.gameObject.SetActive(false);

        paused = false;
        /*
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Envialia"))
        {
            GameObject[] GPackage = GameObject.FindGameObjectsWithTag("GreenPackage");
            GameObject[] RPackage = GameObject.FindGameObjectsWithTag("RedPackage");

            foreach( GameObject g in GPackage)
            {
                g.SetActive(true);
            }

            foreach( GameObject g in RPackage)
            {
                g.SetActive(true);
            }
        }
        */
        
    }
}
