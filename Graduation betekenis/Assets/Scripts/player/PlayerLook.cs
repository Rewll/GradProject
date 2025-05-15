using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity;

    public Transform orientaton;

    public float xRotation;
    public float yRotation;
    
    // Update is called once per frame
    public void OnUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        yRotation += mouseX;
        xRotation -= mouseY;
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientaton.rotation = Quaternion.Euler(0, yRotation, 0f);
    }
}