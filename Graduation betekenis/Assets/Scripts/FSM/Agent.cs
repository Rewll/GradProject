using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private System.Type startState;
    private FSM fsm;
    public enum staten
    {startStaat}

    public staten huidigeStaat;

    void Start()
    {
        switch (huidigeStaat)
        {
            case staten.startStaat:
                startState = typeof(gameStart);
                break;
        }
        fsm = new FSM(startState, GetComponents<BaseState>()); //Starting state, with getcomponentSSS because multiple states are being used
    }

    void Update()
    {
        fsm.OnUpdate();
    }
}