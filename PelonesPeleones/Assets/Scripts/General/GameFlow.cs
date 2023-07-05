using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public static GameFlow instance;
    public bool naveGame = false;
    public bool plat1 = false;
    public bool plat2 = false;
    public bool plat3 = false;
    public bool topos = false;
    public bool envialia = false;

    void Awake()
    {
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
        }
 
        instance = this;

        DontDestroyOnLoad( this.gameObject );
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadGame()
    {
        GameData data = SaveSystem.LoadGame();

        if(data != null)
        {
            naveGame = data.naveGame;
            plat1 = data.plat1;
            plat2 = data.plat2;
            plat3 = data.plat3;
            topos = data.topos;
            envialia = data.envialia;
        }       
    }

    public void DesbloquearTodo()
    {
        //GameData data = SaveSystem.LoadGame();
        naveGame = true;
        plat1 = true;
        plat2 = true;
        plat3 = true;
        topos = true;
        envialia = true;
    }

    public void BloquearTodo()
    {
        //GameData data = SaveSystem.LoadGame();
        naveGame = false;
        plat1 = false;
        plat2 = false;
        plat3 = false;
        topos = false;
        envialia = false;
    }
}
