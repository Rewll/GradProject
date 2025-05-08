using System;
using UnityEngine;

public class Kamera : BaseState
{
    private Player playerRef;
    private void Start()
    {
        playerRef = GetComponent<Player>();
    }

    public override void OnEnter()
    {
        Debug.Log("kamera");
        playerRef.huidigeStaat = playerStates.CameraMode;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void OnUpdate()
    {
        Debug.Log("kamera");

        if (Input.GetKeyDown(playerRef.CameraKnop))
        {
            owner.SwitchState(typeof(PlayerMove));
        }
    }
    
    public override void OnFixedUpdate()
    {
        
    }
    
    public override void OnExit()
    {
        
    }
}
