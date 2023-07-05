using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sonidos;

    [HideInInspector]public float[] sonidosVolume;

    public static AudioManager instance;
    void Awake() //maybe awake
    {
        
       if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
        }
 
        instance = this;

        DontDestroyOnLoad( this.gameObject );

        sonidosVolume = new float[sonidos.Length];

        foreach(Sound s in sonidos)
        {
            s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.spatialBlend = s.spatialBlend;
                s.source.rolloffMode = AudioRolloffMode.Linear;
                s.source.loop = s.loop;
                s.source.maxDistance = s.maxDistanceToSound;
        }
    }

    void Update()
    {
    }

    public void SaveVolume()
    {
        for(int i = 0; i < sonidosVolume.Length; ++i)
        {
            sonidosVolume[i] = sonidos[i].volume;
        }
    }

    public void MuteAll()
    {
        for(int i=0; i<sonidos.Length; ++i)
        {
            sonidos[i].source.mute = true;
        }
    }

    public void UnMuteAll()
    {
        for(int i = 0; i < sonidos.Length; ++i)
        {
            sonidos[i].source.mute = false;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sonidos, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sonidos, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.Stop();
    }

    public void StopAllSounds()
    {
        foreach(Sound s in sonidos)
        {
            if(s.source.isPlaying)
            {
                s.source.Stop();
            }
        }    
    }

    public bool RunningAudioPlaying(string soundName)
    {
        Sound s = Array.Find(sonidos, sound => sound.name == soundName);
        if(s == null)
        {
            return false;
        }

        return s.source.isPlaying;
    }   
}
