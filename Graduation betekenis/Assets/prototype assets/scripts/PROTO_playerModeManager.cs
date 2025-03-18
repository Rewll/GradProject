using UnityEngine;
using UnityEngine.Events;

public class PROTO_playerModeManager : MonoBehaviour
{
    PROTO_playerController playercontRef;
    PROTO_Camera cameraRef;
    public KeyCode cameraModeKnop;

    public bool inCameraMode;
    public UnityEvent onCameraMode;
    public UnityEvent onLoopMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playercontRef = GetComponent<PROTO_playerController>();
        cameraRef = GetComponent<PROTO_Camera>();

        if (inCameraMode)
        {
            onCameraMode.Invoke();
        }
        else if (!inCameraMode)
        {
            onLoopMode.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(cameraModeKnop))
        {
            if (!inCameraMode)
            {
                onCameraMode.Invoke();
            }
            else if (inCameraMode)
            {
                onLoopMode.Invoke();
            }
        }
    }

    public void toCameraMode()
    {
        inCameraMode = true;
        Cursor.lockState = CursorLockMode.None;

        playercontRef.magLopen = false;
        playercontRef.magKijken = false;
        playercontRef.cameraModus = true;
    }

    public void toLoopMode()
    {
        inCameraMode = false;
        Cursor.lockState = CursorLockMode.Locked;
        playercontRef.magLopen = true;
        playercontRef.magKijken = true;
        playercontRef.cameraModus = false;
    }
}
