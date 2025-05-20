using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Agent : MonoBehaviour
{
    private FSM fsm;
    private System.Type startState;
    public enum staten
    {GameStartState,
        FabriekInitState, FabriekTutorialState, FabriekWerkState, FabriekOntsnapState, 
        LandschapInitState,LandschapTutorialState, LandschapState, 
        CollageState}

    public staten StartStaat;
    public staten huidigeStaat;
    
    void Start()
    {
        switch (StartStaat)
        {
            case staten.GameStartState:
                startState = typeof(GameStartState);
                break;
            case staten.FabriekInitState:
                startState = typeof(FabriekInitState);
                break;
            case staten.FabriekTutorialState:
                startState = typeof(FabriekTutorialState);
                break;
            case staten.FabriekWerkState:
                startState = typeof(FabriekWerkState);
                break;
            case staten.FabriekOntsnapState:
                startState = typeof(FabriekOntsnapState);
                break;
            case staten.LandschapInitState:
                startState = typeof(LandschapInitState);
                break;
            case staten.LandschapTutorialState:
                startState = typeof(LandschapTutorialState);
                break;
            case staten.LandschapState:
                startState = typeof(LandschapState);
                break;
            case staten.CollageState:
                startState = typeof(CollageState);
                break;
        }
        //huidigeStaat = StartStaat;
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