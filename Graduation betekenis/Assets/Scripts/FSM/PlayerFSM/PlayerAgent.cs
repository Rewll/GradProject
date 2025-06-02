using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum PlayerStates {WalkLookState, KameraState, SleepState}

public class PlayerAgent : MonoBehaviour
{
    private FSM fsm;
    private System.Type startState;
    
    [Header("Player States:")]
    public PlayerStates StartStaat;
    public PlayerStates huidigeStaat;
    [Space]
    [Header("Script References:")]
    public PlayerLook playerLookRef;
    public PlayerMove playerMoveRef;
    [Space]
    [Header("Object References:")]
    public GameObject kameraDisabledMesh;
    public GameObject kamerUseMesh;
    public GameObject pictureScreen;
    public List<GameObject> kameraModeGameObjects = new List<GameObject>();
    public Transform fabriekRaycastPos;
    [Space]
    [Header("Player Wide Variables:")]
    public KeyCode CameraKnop;
    public bool inFabriek;
    [Space] 
    public bool heeftFoto;
    public Texture fabriekFoto;
    
    private void Awake()
    {
        switch (StartStaat)
        {
            case PlayerStates.WalkLookState:
                startState = typeof(PlayerWalkLookState);
                break;
            case PlayerStates.KameraState:
                startState = typeof(PlayerKameraState);
                break;
            case PlayerStates.SleepState:
                startState = typeof(PlayerSleepState);
                break;
        }
        huidigeStaat = StartStaat;
        fsm = new FSM(startState, GetComponents<BaseState>()); //Starting state, with getcomponentSSS because multiple states are being used
    }
    void Update()
    {
        fsm.OnUpdate();
    }

    void FixedUpdate()
    {
        fsm.OnFixedUpdate();
    }

    public void TeleportPlayer(Vector3 destinationPos)
    {
        playerMoveRef.Teleport(destinationPos);
    }

    public void SetPlayerLookDirection(float xRotation, float yRotation)
    {
        playerLookRef.xRotation = xRotation;
        playerLookRef.yRotation = yRotation;
    }
    
    public void SetPlayerState(PlayerStates state)
    {
        switch (state)
        {
            case PlayerStates.WalkLookState:
                fsm.SwitchState(typeof(PlayerWalkLookState));
                break;
            case PlayerStates.KameraState:
                fsm.SwitchState(typeof(PlayerKameraState));
                break;
            case PlayerStates.SleepState:
                fsm.SwitchState(typeof(PlayerSleepState));
                break;
        }
    }
}
