using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{

    public string SceneName;
    private VideoPlayer video;
    private float delta = 0f;
    private AudioManager instance;

    void Start()
    {
        instance = AudioManager.instance;
        video = GetComponent<VideoPlayer>();
        instance.StopAllSounds();
    }


    void Update()
    {
        delta+=Time.deltaTime;

        if(delta >= video.length)
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
