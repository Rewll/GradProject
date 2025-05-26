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
    public float werkTijd;
    
    public override void OnEnter()
    {
        _fabriekAgentRef.huidigeStaat = FabriekAgent.FabriekStaten.FabriekWerkState;
        //StartCoroutine(werkRoutine());
    }
    
    public override void OnUpdate()
    {
       
    }

    IEnumerator werkRoutine()
    {
        yield return new WaitForSeconds(werkTijd);
        owner.SwitchState(typeof(FabriekOntsnapState));
    }
    public override void OnFixedUpdate()
    {
        
    }
    
    public override void OnExit()
    {
       
    }
}