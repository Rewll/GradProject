using System;
using UnityEngine;
using UnityEngine.UI;

public class LandschapManager : MonoBehaviour
{
    public GameObject playerObjectRef;
    public PlayerAgent playerAgentRef;
    [Space] 
    public Image fadeVlak;
    public AudioLayer audioManRef;
    public bool skipTutorial;
    [Space] 
    [Header("Tutorial References")]
    public GameObject cameraTutorial1;
    public GameObject cameraTutorial2;
    public Button fotoKijkKnop;
    
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