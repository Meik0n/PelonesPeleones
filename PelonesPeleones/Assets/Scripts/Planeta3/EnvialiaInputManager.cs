using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnvialiaInputManager : MonoBehaviour
{
    public LayerMask objectToTouch;
    private Vector3 finger;
    [HideInInspector]public Touch touch;
    private RaycastHit2D hit2;
    private GameObject objectToMove;
    public float gameTime = 90f;
    private float timeFlag = 0f;
    private Camera cam;
    private GameFlow instance;
    private ManagerScene sceneManager;
    private AudioManager audioManager;
    public Image timeBar;

    void Awake()
    {
        instance = GameFlow.instance;
        sceneManager = GetComponent<ManagerScene>();
    }
    void Start()
    {
        audioManager = AudioManager.instance;
        cam = Camera.main;

        audioManager.StopAllSounds();
        audioManager.Play("Music_Envialia");
    }
    void Update()
    {
       if(Input.touchCount > 0)
        {
            finger = cam.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, -20.0f)); //fix z here  
            touch = Input.GetTouch(0);
            bool hit = Physics2D.Raycast(finger,Vector3.forward,Mathf.Infinity,objectToTouch);
            hit2 = Physics2D.Raycast(finger,Vector3.forward,Mathf.Infinity,objectToTouch);

            Vector3 position = new Vector3(finger.x,finger.y,0);

            if (hit)
            {
                if(touch.phase == TouchPhase.Began)
                {
                    objectToMove =  hit2.collider.gameObject;
                    objectToMove.GetComponent<Collider2D>().enabled = false;
                    objectToMove.transform.localScale *= 1.3f;

                    if(objectToMove.CompareTag("RedPackage"))
                    {
                        audioManager.Play("Pick_Paquete");
                    }
                    else if(objectToMove.CompareTag("GreenPackage"))
                    {
                        audioManager.Play("Pick_Paquete_V2");
                    }
                }    
            }

            if(touch.phase == TouchPhase.Ended)
            {                
                if(objectToMove)
                {
                    objectToMove.transform.localScale /= 1.3f;
                    objectToMove.GetComponent<Collider2D>().enabled = true;
                }
                
                objectToMove = null;
            }  

            if(objectToMove != null)
            {
                objectToMove.transform.position = position;                          
            }               
        }

        timeFlag += Time.deltaTime;
        if(timeFlag >= gameTime)
        {
            FinishGame();
        } 
        timeBar.fillAmount = ( timeFlag / gameTime);  
          
    }

    private void FinishGame()
    {
        instance.envialia = true;
        instance.SaveGame();
        sceneManager.LoadLevel("video8");
    }
}