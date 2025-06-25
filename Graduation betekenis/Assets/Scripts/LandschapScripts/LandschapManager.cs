using System;
using UnityEngine;
using UnityEngine.UI;

public class LandschapManager : MonoBehaviour
{
    public PlayerAgent playerAgentRef;
    [Space] 
    public Image fadeVlak;
    public AudioLayer audioManRef;
    [Space]
    public KameraPictureDisplay picDisRef;
    [Space]
    [Header("Tutorial:")]
    public GameObject cameraTutorial1;
    public GameObject cameraTutorial2;
    public Button fotoKijkKnop;
    [Space]
    public bool skipTutorial;
    
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