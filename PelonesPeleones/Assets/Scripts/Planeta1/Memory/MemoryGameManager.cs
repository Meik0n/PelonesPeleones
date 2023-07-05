//**
/*  Cuando escribí esto solo dios y yo sabíamos lo que estaba haciendo
/*  Ahora solo dios lo sabe
/*  Te recomiendo que recurras a los antiguos testamentos y te acojas al mandamiento numero 1:
/*  "Si funciona no lo toques"
/*  Así que mejor cierra este script y haz como si nunca hubieses entrado aquí
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGameManager : MonoBehaviour
{
    
    public GameObject[] colors;
    public Sprite[] iluminatedColors;
    private List<Sprite> colorsToIlluminate = new List<Sprite>();
    public List<GameObject> selectedColors = new List<GameObject>();
    public float timeToInitiateGame = .7f;
    public float timeBetweenColors = .5f;
    private Vector3 touchPosition;
    private AudioManager audioManager;
    private int i = 0;  
    public GameObject memoryCanvas;
    public MemoryManager manager;
    public Platform_Memory_Activable platform;
    public MemoryParticles memoryParticle;

    void Awake()
    {
        audioManager = AudioManager.instance;
    }

    void Start()
    {
        StartCoroutine(ChooseRandomColors());
    }

    protected IEnumerator ChooseRandomColors()
    {
        colorsToIlluminate.Clear();   
        int r =  3;

        int i = 0;
        do
        {
            int randomColor = Random.Range(0,colors.Length);
            selectedColors.Add(colors[randomColor]);
            colorsToIlluminate.Add(iluminatedColors[randomColor]);
            ++i;
        }while(i < r);

        foreach(GameObject c in colors)
        {
            c.GetComponent<Button>().interactable = false;
        }

        yield return new WaitForSeconds(timeToInitiateGame);
        StartCoroutine(IlluminateColors());
        yield break;
    }

    protected IEnumerator IlluminateColors()
    {
        int iterator = 0; 
        foreach(GameObject c in selectedColors)
        {
            Sprite initialSprite = c.gameObject.GetComponent<Image>().sprite;
            c.gameObject.GetComponent<Image>().sprite = colorsToIlluminate[iterator];
            c.gameObject.transform.localScale *= 1.15f;
            yield return new WaitForSeconds(.7f);
            c.gameObject.GetComponent<Image>().sprite = initialSprite;
            c.gameObject.transform.localScale /= 1.15f;
            yield return new WaitForSeconds(timeBetweenColors);
            iterator++;
        }

        foreach(GameObject c in colors)
        {
            c.GetComponent<Button>().interactable = true;
        }

        yield break;
    }

    public void TouchColor(GameObject colourToTouch)
    {
        if(selectedColors[i] != null)
        {
            if(colourToTouch == selectedColors[i])
            {
                audioManager.Play("Memory_Click");
                i++;
                if(i >= selectedColors.Count)
                {
                    //StartCoroutine(ChooseRandomColors());
                    memoryParticle.gameObject.SetActive(true);

                    IEnumerator wait()
                    {
                        memoryCanvas.GetComponent<Canvas>().enabled = false;
                        audioManager.Play("Memory_Acierto");
                        yield return new WaitForSeconds(1f);
                        manager.Desactivate();
                        platform.isActive = true;
                    }
                    StartCoroutine(wait());
                }
            }
            else
            {
                audioManager.Play("Memory_Error");
                i = 0;
                selectedColors.Clear();  
                StartCoroutine(ChooseRandomColors());             
            }
        }      
    }
}
