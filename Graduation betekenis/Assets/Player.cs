using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum playerStates {WalkLookMode, CameraMode}

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
