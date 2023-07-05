using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimiInterfaz : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Blink());
    }


    void Update()
    {
        
    }

    private IEnumerator Blink()
    {
        while(true)
        {
            int r = Random.Range(0, 9);
            if(r == 5 )
            {
                
                anim.SetTrigger("Blink");
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
