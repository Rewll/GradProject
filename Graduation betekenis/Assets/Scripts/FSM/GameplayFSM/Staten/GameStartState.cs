using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStartState : BaseState
{
    private Agent _agent;
    private GameManager _gameManagerRef;

    private void Awake()
    {
        _agent = GetComponent<Agent>();
        _gameManagerRef = GetComponent<GameManager>();
    }

    public override void OnEnter()
    {
        _agent.huidigeStaat = Agent.staten.GameStartState;
        //Dingen intitalizen enzo
        owner.SwitchState(typeof(FabriekInitState));
    }
    
    public override void OnUpdate()
    {
        
    }
    
    public override void OnFixedUpdate()
    {
        
    }
    
    public override void OnExit()
    {
        
    }
}