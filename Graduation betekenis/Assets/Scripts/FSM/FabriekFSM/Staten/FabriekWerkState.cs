using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabriekWerkState : BaseState
{
    private FabriekAgent _fabriekAgentRef;
    private FabriekManager _fabriekManagerRef;
    [Space] 
    public Animator machineAnim;

    private void Awake()
    {
        _fabriekAgentRef = GetComponent<FabriekAgent>();
        _fabriekManagerRef = GetComponent<FabriekManager>();
    }
    public float werkTijd;
    
    public override void OnEnter()
    {
        _fabriekAgentRef.huidigeStaat = FabriekAgent.FabriekStaten.FabriekWerkState;
        machineAnim.SetTrigger("TrEnter");
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