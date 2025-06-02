using System;
using UnityEngine;

public class FabriekAgent : MonoBehaviour
{
    private FSM _fsm;
    private System.Type _startState;
    public enum FabriekStaten { FabriekInitState, FabriekTutorialState, FabriekWerkState, FabriekOntsnapState}

    public FabriekStaten startStaat;
    public FabriekStaten huidigeStaat;
    
    void Start()
    {
        switch (startStaat)
        {
            case FabriekStaten.FabriekInitState:
                _startState = typeof(FabriekInitState);
                break;
            case FabriekStaten.FabriekTutorialState:
                _startState = typeof(FabriekTutorialState);
                break;
            case FabriekStaten.FabriekWerkState:
                _startState = typeof(FabriekWerkState);
                break;
            case FabriekStaten.FabriekOntsnapState:
                _startState = typeof(FabriekOntsnapState);
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
