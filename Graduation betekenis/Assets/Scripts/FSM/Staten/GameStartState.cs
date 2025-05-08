using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartState : BaseState
{
    private void Start()
    {
        
    }

    public override void OnEnter()
    {
        GetComponent<Agent>().huidigeStaat = Agent.staten.GameStartState;
        //Dingen intitalizen enzo
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