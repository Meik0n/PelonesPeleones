using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool naveGame;
    public bool plat1;
    public bool plat2;
    public bool plat3;
    public bool topos;
    public bool envialia;

    public GameData(GameFlow data)
    {
        naveGame = data.naveGame;
        plat1 = data.plat1;
        plat2 = data.plat2;
        plat3 = data.plat3;
        topos = data.topos;
        envialia = data.envialia;
    }
}
