using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLvl : MonoBehaviour
{
    private GameFlow instance;
    public ManagerScene sceneManager;
    void Awake()
    {
        instance = GameFlow.instance;
    }
    void OnTriggerEnter2D(Collider2D col)
    {  
        if(col.tag == "Player")
        {
                
            if(SceneManager.GetActiveScene().name == "nivel1")
            {
                instance.plat1 = true;
                sceneManager.LoadLevel("LevelSelection");
            }
            if(SceneManager.GetActiveScene().name == "nivel2")
            {
                Debug.Log("triggering");
                instance.plat2 = true;
                sceneManager.LoadVideo("3");
            }
            if(SceneManager.GetActiveScene().name == "nivel 3")
            {
                instance.plat3 = true;
                sceneManager.LoadVideo("4");
            }
            instance.SaveGame();
            
        }
    }
}
