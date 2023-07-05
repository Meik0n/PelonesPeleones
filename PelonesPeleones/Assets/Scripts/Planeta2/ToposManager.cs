using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Anima2D;

public class ToposManager : MonoBehaviour
{
    [SerializeField]public Transform[] casillas = new Transform[32]; 
    [HideInInspector]public int numTopos; 
    [HideInInspector]public int numGlobos;
   // public float spaceBetweenCellsAumentX = 1.4f;
   // public float spaceBetweenCellsAumentY = 1.2f;
   // public GameObject camera1;
    public GameObject topoPrefab;
    public int maxLifes = 5;
    public Image hp;
    private int currentLife;
    public float initialSpawnTime = 1;
    public float finalSpawnTime = 6;
    public int numToposToKill = 10;
    [HideInInspector]public int numToposKilled = 0;
    private Scene gameScene;
    private GameFlow instance;
    private ManagerScene sceneManager;
    private AudioManager audioManager;
    float a = 0f;
    private float temp = 0.1f;
    private bool b = false;
    public Text toposToKillText;
   // void Awake()
    //{    
       
        
       // audioManager.StopAllSounds();
/*
        for(int i = 0; i<casillas.GetLength(0); i-=-1)
        {
            for(int j =0; j < casillas.GetLength(1); j-=-1)
            {
                casillas[i,j] = new Cell(i*spaceBetweenCellsAumentX + 0.5f, j*spaceBetweenCellsAumentY);
            }
        }   
        */ 
    //}

    void Start()
    {
        numTopos = 0;
        numGlobos = 0;
        currentLife = maxLifes;
        instance = GameFlow.instance;
        audioManager = AudioManager.instance;


        StartCoroutine(InstantiateTopo());
        sceneManager = GetComponent<ManagerScene>();
        gameScene = SceneManager.GetActiveScene();

        audioManager.StopAllSounds();
        //camera1.transform.position = (Vector3) casillas[3,1].center + new Vector3(2*casillas[3,1].Tam,casillas[3,1].Tam*4,-10);  
    }

    void Update()
    {

        a += Time.deltaTime;
        if(b ==false && a >= temp )
        {
            b = true;
            audioManager.Play("MusicTopos");
        }
        if(currentLife <= 0)
        {
            SceneManager.LoadScene("ToposDeath");
        }

        if(numToposKilled >= numToposToKill)
        {
            instance.topos = true;
            instance.SaveGame();
            sceneManager.LoadLevel("video6");
        }

        hp.fillAmount = (float)currentLife/maxLifes;
        toposToKillText.text = (numToposToKill - numToposKilled).ToString();
    }

    private IEnumerator InstantiateTopo()
    {
        while(true)
        {
            if(numTopos == 5)
            {
                yield break;
            }
            else
            {
                var pos = (int)Random.Range(0,casillas.Length);
                var go = topoPrefab;
                SpriteMeshInstance[] goComponents;

                Vector3 position = casillas[pos].position;

                numTopos++;
                
                if(pos >= 0 && pos <8)
                {
                    goComponents =  go.GetComponentsInChildren<SpriteMeshInstance>();
                    
                    foreach(SpriteMeshInstance sp in goComponents)
                    {
                        sp.sortingOrder = 1;
                    }
                }
                else if( pos >= 8 && pos < 16)
                {
                    goComponents =  go.GetComponentsInChildren<SpriteMeshInstance>();
                    
                    foreach(SpriteMeshInstance sp in goComponents)
                    {
                        sp.sortingOrder = 3;
                    }
                }   
                else if(pos >= 16 && pos < 24)
                {
                    goComponents =  go.GetComponentsInChildren<SpriteMeshInstance>();
                    
                    foreach(SpriteMeshInstance sp in goComponents)
                    {
                        sp.sortingOrder = 5;
                    }
                }        
                else if(pos >= 24 && pos < 32)
                {
                    goComponents =  go.GetComponentsInChildren<SpriteMeshInstance>();
                    
                    foreach(SpriteMeshInstance sp in goComponents)
                    {
                        sp.sortingOrder = 7;
                    }
                } 

                Instantiate(go,position,Quaternion.identity);
            
                yield return new WaitForSeconds((int)Random.Range(initialSpawnTime,finalSpawnTime));
            }       
        }
    }

    public void LooseLife()
    {
        currentLife--;
        audioManager.Play("RecibirDaño");
    }
}
