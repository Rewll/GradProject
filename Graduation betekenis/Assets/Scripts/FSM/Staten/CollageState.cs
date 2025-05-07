using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollageState : BaseState
{
    private void Start()
    {
        
    }

    public override void OnEnter()
    {
        GetComponent<Agent>().huidigeStaat = Agent.staten.CollageState;
    }
    
    public override void OnUpdate()
    {
       
    }
    
    public override void OnExit()
    {
       
    }
}