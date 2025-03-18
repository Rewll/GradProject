using UnityEngine;
using UnityEngine.Events;

public class protoCamera : MonoBehaviour
{
    public KeyCode cameraKnop;
    public bool inCameraMode;
    public UnityEvent onCameraMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(cameraKnop) && !inCameraMode)
        {
            inCameraMode = true;
            GetComponent<playerMovement>().magLopen = false;
            onCameraMode.Invoke();
        }
    }
}