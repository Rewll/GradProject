using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabriekWerkState : BaseState
{
    private FabriekAgent _fabriekAgentRef;
    private FabriekManager _fabriekManagerRef;

    private void Awake()
    {
        _fabriekAgentRef = GetComponent<FabriekAgent>();
        _fabriekManagerRef = GetComponent<FabriekManager>();
    }
    
    public override void OnEnter()
    {
        _fabriekAgentRef.huidigeStaat = FabriekAgent.FabriekStaten.FabriekWerkState;
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

    public void SpelerOntsnapFase()
    {
        owner.SwitchState(typeof(FabriekOntsnapState));
    }
}