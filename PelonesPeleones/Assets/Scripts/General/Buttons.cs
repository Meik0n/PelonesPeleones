using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
     public Button m_YourButton, m_YourSecondButton;
     public PlayerController playerController;

    void Start()
    {
        Button btn = m_YourButton.GetComponent<Button>();
        Button btn2 = m_YourSecondButton.GetComponent<Button>();
        playerController =playerController.GetComponent<PlayerController>();
        
       // btn.onClick.AddListener(playerController.Shoot);
        btn2.onClick.AddListener(playerController.Jump);
    }
    
}
