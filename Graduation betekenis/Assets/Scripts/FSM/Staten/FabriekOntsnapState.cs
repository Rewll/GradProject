using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabriekOntsnapState : BaseState
{
    private void Start()
    {
        
    }

    public override void OnEnter()
    {
        GetComponent<Agent>().huidigeStaat = Agent.staten.FabriekOntsnapState;
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