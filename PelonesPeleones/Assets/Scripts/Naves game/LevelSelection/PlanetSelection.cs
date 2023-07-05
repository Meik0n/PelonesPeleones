using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetSelection : MonoBehaviour
{
    [Header("Paneles de selección de nivel del planeta")]
    public GameObject[] SelectionPanels;
    [Header("Paneles de info del planeta")]
    public GameObject[] InfoPanels;

    [Header("Planetas")]
    public GameObject[] Planetas;
    public Sprite[] PlanetasSprites;

    private GameObject infoBut;
    private AudioManager audioManager;
    private GameFlow data;

    void Awake()
    {
        data = GameFlow.instance;
        
        if(infoBut == null)
        {
            infoBut = GameObject.FindGameObjectWithTag("InfoBut");
            if(infoBut.activeSelf)
            {
                infoBut.SetActive(false);
            }
        }
        
    }
    void Start()
    {
        audioManager = AudioManager.instance;
        audioManager.StopAllSounds();
        audioManager.Play("Music_SeleccionNivel");
        audioManager.Play("Ambient");
    }

    void Update()
    {
        if(data.naveGame)
        {
            Planetas[0].GetComponent<Image>().sprite = PlanetasSprites[1];
        }
        else if(!data.naveGame)
        {
            Planetas[0].GetComponent<Image>().sprite = PlanetasSprites[0];
        }

        if(data.plat3)
        {
            Planetas[1].GetComponent<Image>().sprite = PlanetasSprites[3];
        }
        else if(!data.plat3)
        {
            Planetas[1].GetComponent<Image>().sprite = PlanetasSprites[2];
        }

        if(data.topos)
        {
            Planetas[2].GetComponent<Image>().sprite = PlanetasSprites[5];
        }
        else if(!data.topos)
        {
            Planetas[2].GetComponent<Image>().sprite = PlanetasSprites[4];
        }

        if(data.envialia)
        {
            Planetas[3].GetComponent<Image>().sprite = PlanetasSprites[7];
        }
        else if(!data.envialia)
        {
            Planetas[3].GetComponent<Image>().sprite = PlanetasSprites[6];
        }
    }

    public void PlanetSelector(int num)
    {
        if((num == 1 && data.naveGame) || (num == 2 && data.plat3) ||(num == 3 && data.topos) || (num == 4 && data.envialia))
        {
            foreach(GameObject p in SelectionPanels)
            {
                if(p.activeSelf)
                {
                    p.SetActive(false);
                }
            }            
            SelectionPanels[num-1].SetActive(true);
            audioManager.Play("Press_btnPlanet");
        }

        else
        {
            audioManager.Play("Press_lockedLvl");
        }       


    }

    public void DesactivarPlaneta()
    {
        foreach(GameObject p in SelectionPanels)
        {
            if(p.activeSelf)
            {
              p.SetActive(false);
            }
        }
    }

    public void ActivarInfoButton()
    {
        if(infoBut)
        {
            if(infoBut.activeSelf == false)
            {
                infoBut.SetActive(true);
            }  
        }            
    }

    public void DesactivarInfoButton()
    {
        if(infoBut)
        {
            infoBut.SetActive(false);
        }     
    }

    public void ActivarInfo()
    {
        for(int i=0; i<SelectionPanels.Length; i++)
        {
            if(SelectionPanels[i].activeSelf)
            {
                if(InfoPanels[i])
                {
                    InfoPanels[i].SetActive(true);
                    audioManager.Play("Press_btnInfo");
                }
            }
        }
    }

    public void DesactivarInfo()
    {
        foreach(GameObject i in InfoPanels)
        {
            if(i.activeSelf)
            {
                i.SetActive(false);
            }
        }
    }

    public void BotonDiario()
    {
        audioManager.Play("Press_btnDiario");
    }
}
