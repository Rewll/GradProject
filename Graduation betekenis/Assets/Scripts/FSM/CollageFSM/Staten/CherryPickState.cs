using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryPickState : BaseState
{
    private CollageAgent _collageAgentRef;
    private CollageManager _collageManagerRef;
    
    private void Awake()
    {
        _collageAgentRef = GetComponent<CollageAgent>();
        _collageManagerRef = GetComponent<CollageManager>();
    }

    public override void OnEnter()
    {
        _collageAgentRef.huidigeStaat = CollageAgent.Collagestaten.CherryPickState;

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