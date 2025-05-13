using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum playerStates {WalkLookMode, CameraMode, SleepMode}

public class Player : MonoBehaviour
{
    private FSM fsm;
    private System.Type startState;
    [Header("Player States:")]
    public playerStates StartStaat;
    public playerStates huidigeStaat;
    [Space]
    [Header("Player Wide Variables:")]
    public KeyCode CameraKnop;
    [Space]
    [Header("References")]
    public PlayerLook playerLookRef;
    [Space]
    public GameObject kameraDisabledMesh;
    public GameObject kamerUseMesh;
    public GameObject pictureScreen;
    public List<GameObject> kameraModeGameObjects = new List<GameObject>();
    
    private void Start()
    {
        switch (StartStaat)
        {
            case playerStates.WalkLookMode:
                startState = typeof(PlayerMove);
                break;
            case playerStates.CameraMode:
                startState = typeof(Kamera);
                break;
            case playerStates.SleepMode:
                startState = typeof(PlayerSleepMode);
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
}
