using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStartState : BaseState
{
    private LandschapAgent _landschapAgent;
    private LandschapManager _landschapManagerRef;

    private void Awake()
    {
        _landschapAgent = GetComponent<LandschapAgent>();
        _landschapManagerRef = GetComponent<LandschapManager>();
    }

    public override void OnEnter()
    {
        //_landschapAgent.huidigeStaat = LandschapAgent.staten.GameStartState;
        //Dingen intitalizen enzo
        owner.SwitchState(typeof(FabriekInitState));
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