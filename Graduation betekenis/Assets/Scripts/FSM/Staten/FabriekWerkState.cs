using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabriekWerkState : BaseState
{
    private Agent _agent;
    private GameManager _gameManagerRef;

    private void Start()
    {
        _agent = GetComponent<Agent>();
        _gameManagerRef = GetComponent<GameManager>();
    }

    public override void OnEnter()
    {
        _agent.huidigeStaat = Agent.staten.FabriekWerkState;
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