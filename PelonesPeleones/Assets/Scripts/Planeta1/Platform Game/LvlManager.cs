using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlManager : MonoBehaviour
{
    public int numeroDePiezasNecesarias;
    [HideInInspector]public int piezas;
    private GameObject exitPortal;
    public GameObject[] piezasImages;
    public Sprite[] pickedPiezasImages;
    void Start()
    {
        exitPortal = GameObject.FindGameObjectWithTag("ExitPortal");
        
        if(exitPortal)
        {
            if(exitPortal.activeSelf)
            {
                exitPortal.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(piezas == 1)
        {
            piezasImages[0].GetComponent<Image>().sprite = pickedPiezasImages[0];
            piezasImages[3].GetComponent<Image>().sprite = pickedPiezasImages[3];
        }
        if(piezas == 2)
        {
            piezasImages[1].GetComponent<Image>().sprite = pickedPiezasImages[1];
            piezasImages[4].GetComponent<Image>().sprite = pickedPiezasImages[4];
        }
        if(piezas == 3)
        {
            piezasImages[2].GetComponent<Image>().sprite = pickedPiezasImages[2];
            piezasImages[5].GetComponent<Image>().sprite = pickedPiezasImages[5];
        }
        
        if(piezas == numeroDePiezasNecesarias)
        {
            exitPortal.SetActive(true);
        }
    }
}
