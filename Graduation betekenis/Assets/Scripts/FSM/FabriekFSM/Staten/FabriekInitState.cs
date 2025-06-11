using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabriekInitState : BaseState
{
    private FabriekAgent _fabriekAgentRef;
    private FabriekManager _fabriekManagerRef;
    
    public bool skipTutorial;
    public Transform fabriekStartPos;
    
    private void Awake()
    {
        _fabriekAgentRef = GetComponent<FabriekAgent>();
        _fabriekManagerRef = GetComponent<FabriekManager>();
    }

    public override void OnEnter()
    {
        _fabriekAgentRef.huidigeStaat = FabriekAgent.FabriekStaten.FabriekInitState;
        _fabriekManagerRef.SetDeur(false);
        _fabriekManagerRef.playerAgentRef.TeleportPlayer(fabriekStartPos.position);
        
        if (skipTutorial)
        {
            _fabriekManagerRef.fadeVlak.gameObject.SetActive(false);
            _fabriekManagerRef.machineAnim.SetTrigger("TrEnter");
            _fabriekManagerRef.fabriekAmbianceLayerRef.FadeInSound(0,5f);
            owner.SwitchState(typeof(FabriekWerkState));
        }
        else
        {
            _fabriekManagerRef.fadeVlak.gameObject.SetActive(true);
            owner.SwitchState(typeof(FabriekTutorialState));
        }
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