using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabriekWerkState : BaseState
{
    private void Start()
    {
        
    }

    public override void OnEnter()
    {
        GetComponent<Agent>().huidigeStaat = Agent.staten.FabriekWerkState;
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