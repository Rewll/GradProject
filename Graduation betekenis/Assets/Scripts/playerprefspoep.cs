using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerprefspoep : MonoBehaviour
{
    public PlayerLook playerLookRef;
    public float muisGevoel;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SlaGetalOp()
    {
        muisGevoel = playerLookRef.mouseSensitivity;
    }

    public void LaadGetal()
    {
        playerLookRef = FindAnyObjectByType<PlayerLook>();
        playerLookRef.mouseSensitivity = muisGevoel;
    }
}
