using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandschapInitState : BaseState
{
    private LandschapAgent _landschapAgent;
    private LandschapManager _landschapManagerRef;
    [Header("References: ")]
    public Transform startplek;

    private void Awake()
    {
        _landschapAgent = GetComponent<LandschapAgent>();
        _landschapManagerRef = GetComponent<LandschapManager>();
        _landschapManagerRef.cameraTutorial1.SetActive(false);
        _landschapManagerRef.cameraTutorial2.SetActive(false);
    }

    public override void OnEnter()
    {
        //Debug.Log("LandschapState OnEnter");
        _landschapAgent.huidigeStaat = LandschapAgent.LandschapStaten.LandschapInitState;
        
        
        if (_landschapManagerRef.skipTutorial)
        {
            owner.SwitchState(typeof(LandschapState));
        }
        else
        {
            _landschapManagerRef.fadeVlak.gameObject.SetActive(true);
            owner.SwitchState(typeof(LandschapTutorialState));
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