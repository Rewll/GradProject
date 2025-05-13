using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandschapState : BaseState
{
    private Agent _agent;
    private GameManager _gameManagerRef;
    
    public Transform startplek;

    private void Awake()
    {
        _agent = GetComponent<Agent>();
        _gameManagerRef = GetComponent<GameManager>();
    }

    public override void OnEnter()
    {
        //Debug.Log("LandschapState OnEnter");
        _agent.huidigeStaat = Agent.staten.LandschapState;
        _gameManagerRef.TeleportPlayer(startplek.position);
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

    public void StartCollageFase()
    {
        owner.SwitchState(typeof(CollageState));
    }
    
}