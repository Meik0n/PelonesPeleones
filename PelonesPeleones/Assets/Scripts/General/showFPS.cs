using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class showFPS : MonoBehaviour 
{
    public Text fpsText;
    private float deltaTime;

    void Awake()
    {
        //Application.targetFrameRate = 60;
    }
    
    void Update () 
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();
    }
}
