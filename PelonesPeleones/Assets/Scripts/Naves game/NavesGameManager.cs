using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavesGameManager : MonoBehaviour
{
    public float gameTime;
    public NaveController nave;
    private float deltaTime = 0f;
    private GameObject[] enemy;
    private GameObject spawner;
    private GameFlow instance;
    private ManagerScene sceneManager;
    private AudioManager audioManager;
    public GameObject boss;
    public GameObject bossSpawnPosition;
    private bool instantiated = false;
    private bool desactivated = false;
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        instance = GameFlow.instance;
        audioManager = AudioManager.instance;
        sceneManager = GetComponent<ManagerScene>();
    }


    void Update()
    {
        deltaTime += Time.deltaTime;

        if(deltaTime >= gameTime)
        {
            if(!desactivated)
            {
                spawner.SetActive(false);
                desactivated = true;
            }
            
            enemy = GameObject.FindGameObjectsWithTag("Enemy");
            
            if(enemy.Length == 0)
            {
                if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Boss") && !instantiated)
                {
                    Instantiate(boss,bossSpawnPosition.transform.position,Quaternion.identity);
                    StartCoroutine(reactivateSpawn());
                    instantiated = true;
                }
                else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("NaveGame01"))
                {
                    instance.naveGame = true;
                    audioManager.Play("Despegue_Final");
                    instance.SaveGame();
                    sceneManager.LoadLevel("Video1");
                }
            }
        }

        if(nave.currentHealth == 0)
        {
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Boss") && !instantiated)
            {
                sceneManager.LoadLevel("BossDeath");
            }
            else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("NaveGame01"))
            {
                sceneManager.LoadLevel("NaveDeath");
            }
            
        }
    }

    private IEnumerator reactivateSpawn()
    {
        yield return new WaitForSeconds(2f);
        spawner.SetActive(true);
    }
}
