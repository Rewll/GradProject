using System;
using UnityEngine;
using UnityEngine.UI;

public class LandschapManager : MonoBehaviour
{
    public Transform playerStartPos;
    public GameObject player;
    public PlayerAgent playerAgentRef;
    [Space] 
    public Image fadeVlak;

    public GameObject tutorialArea;
    public audioManager audioManRef;
    
    public void SetCursorMode(int mode)
    {
        if (mode == 0)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; 
        }
        else if (mode == 1)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; 
        }
    }
}