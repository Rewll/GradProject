using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LandschapAgent : MonoBehaviour
{
    private FSM _fsm;
    private System.Type _startState;
    public enum LandschapStaten { LandschapInitState, LandschapTutorialState, LandschapState, CollageState}

    public LandschapStaten StartStaat;
    public LandschapStaten huidigeStaat;
    
    void Start()
    {
        switch (StartStaat)
        {
            case LandschapStaten.LandschapInitState:
                _startState = typeof(LandschapInitState);
                break;
            case LandschapStaten.LandschapTutorialState:
                _startState = typeof(LandschapTutorialState);
                break;
            case LandschapStaten.LandschapState:
                _startState = typeof(LandschapState);
                break;
            case LandschapStaten.CollageState:
                _startState = typeof(CollageState);
                break;
        }
        //huidigeStaat = StartStaat;
        _fsm = new FSM(_startState, GetComponents<BaseState>()); //Starting state, with getcomponentSSS because multiple states are being used
    }

    void Update()
    {
        _fsm.OnUpdate();
    }

    void FixedUpdate()
    {
        _fsm.OnFixedUpdate();
    }
}