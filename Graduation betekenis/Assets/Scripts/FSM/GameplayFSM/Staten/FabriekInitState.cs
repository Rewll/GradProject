using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabriekInitState : BaseState
{
    private Agent _agent;
    private GameManager _gameManagerRef;
    
    public bool skipTutorial;
    
    private void Awake()
    {
        _agent = GetComponent<Agent>();
        _gameManagerRef = GetComponent<GameManager>();
    }

    public override void OnEnter()
    {
        _agent.huidigeStaat = Agent.staten.FabriekInitState;
        
        _gameManagerRef.SetDeurBeginRuimte(0);
        if (skipTutorial)
        {
            _gameManagerRef.fadeVlak.gameObject.SetActive(false);
            owner.SwitchState(typeof(FabriekWerkState));
        }
        else
        {
            _gameManagerRef.fadeVlak.gameObject.SetActive(true);
            owner.SwitchState(typeof(FabriekTutorialState));
        }
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