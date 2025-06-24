using System;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity;

    public Transform orientaton;

    public float xRotation;
    public float yRotation;

    private void Awake()
    {
        //xRotation = transform.rotation.x;
        //yRotation = transform.rotation.y;
        //transform.localRotation = Quaternion.Euler(startX, startY, 0f);
    }

    public void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * (mouseSensitivity * 0.1f);
        float mouseY = Input.GetAxis("Mouse Y") * (mouseSensitivity * 0.1f);
        
        yRotation += mouseX;
        xRotation -= mouseY;
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientaton.rotation = Quaternion.Euler(0, yRotation, 0f);
    }
    
    public void VeranderMuisGevoeligheid(float newSens)
    {
        mouseSensitivity = newSens;
    }
}