using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandschapTutorialState : BaseState
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
        _agent.huidigeStaat = Agent.staten.LandschapTutorialState;
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