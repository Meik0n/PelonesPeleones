using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : LevelLoader
{
    private GameFlow instance;
    private AudioManager audioManagerInstance;

    void Awake()
    {
        instance = GameFlow.instance;
        audioManagerInstance = AudioManager.instance;
    }

    void Start()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Inicio"))
        {
            audioManagerInstance.Play("Inicio_Music");
        }
    }

    public void LoadVideo(string num)
    {
        if(num == "0")
        {
            if(instance.naveGame)
            {
                LoadLevel("LevelSelection");
            }
            else 
            {
                LoadLevel("Video" + num);
            }
        }
        LoadLevel("Video" + num);
    }
    
    public void LoadBoss()
    {
        if(instance.envialia)
        {
            LoadLevel("Boss");
            audioManagerInstance.Play("Press_unlockedLvl");
        }

        audioManagerInstance.Play("Press_lockedLvl");
    }
    public void LoadCreditsScene()
    {
        LoadLevel("Credits");
    }

    public void LoadPlatformGame1()
    {
        if(instance.naveGame)
        {
            audioManagerInstance.Play("Press_unlockedLvl");
            LoadLevel("nivel1");
        }  

        audioManagerInstance.Play("Press_lockedLvl");    
    }
    public void LoadPlatformGame2()
    {
        if(instance.plat1)
        {
            audioManagerInstance.Play("Press_unlockedLvl");
            LoadLevel("nivel2");
        }

        audioManagerInstance.Play("Press_lockedLvl");  
    } 
    public void LoadPlatformGame3()
    {
        if(instance.plat2)
        {
            audioManagerInstance.Play("Press_unlockedLvl");
            LoadLevel("nivel 3");
        }

        audioManagerInstance.Play("Press_lockedLvl");  
    } 

    public void LoadNaveGame()
    {
        audioManagerInstance.Play("Press_unlockedLvl");
        LoadLevel("NaveGame01");
    }

    public void LoadLevelSelection()
    {
        LoadLevel("LevelSelection");
        audioManagerInstance.Play("Press_btnJugar");
    }

    public void LoadOptions()
    {
        LoadLevel("Options");
        audioManagerInstance.Play("Press_btnOpciones");
    }

    public void LoadInicio()
    {
        LoadLevel("Inicio");
        audioManagerInstance.Play("Press_btnOpcionesBack");
    }

    public void LoadTopos()
    {
        if(instance.plat3)
        {
            audioManagerInstance.Play("Press_unlockedLvl");
            LoadLevel("Topos");
        }
        audioManagerInstance.Play("Press_lockedLvl");  
    }

    public void LoadEnvialia()
    {
        if(instance.topos)
        {
            audioManagerInstance.Play("Press_unlockedLvl");
            LoadLevel("Envialia");
        }
        audioManagerInstance.Play("Press_lockedLvl");  
    }

    public void BloqTodo()
    {
        instance.BloquearTodo();
    }

    public void DesbloqTodo()
    {
        instance.DesbloquearTodo();
    }
}
