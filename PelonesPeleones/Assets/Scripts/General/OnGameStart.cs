using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameStart : MonoBehaviour
{
    GameFlow instance;
    void Start()
    {
        instance = GameFlow.instance;
        instance.LoadGame();
    }

}
