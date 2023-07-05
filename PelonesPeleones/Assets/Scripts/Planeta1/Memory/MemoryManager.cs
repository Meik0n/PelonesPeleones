using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour
{
    public GameObject memoryCanvas;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Activate()
    {
        if(memoryCanvas.activeSelf == false)
        {
            memoryCanvas.SetActive(true);
        }
    }

    public void Desactivate()
    {
        if(memoryCanvas.activeSelf == true)
        {
            memoryCanvas.SetActive(false);
        }
    }
}
