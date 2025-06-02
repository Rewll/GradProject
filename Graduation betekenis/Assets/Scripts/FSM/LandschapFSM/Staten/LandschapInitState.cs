using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandschapInitState : BaseState
{
    private LandschapAgent _landschapAgent;
    private LandschapManager _landschapManagerRef;
    public bool skipTutorial1;
    public bool skipTutorial2;
    [Header("References: ")]
    public Transform startplek;

    private void Awake()
    {
        _landschapAgent = GetComponent<LandschapAgent>();
        _landschapManagerRef = GetComponent<LandschapManager>();
    }

    public override void OnEnter()
    {
        //Debug.Log("LandschapState OnEnter");
        _landschapAgent.huidigeStaat = LandschapAgent.LandschapStaten.LandschapInitState;
        
        _landschapManagerRef.audioManRef.FadeInSound(0, 5f);
        if (skipTutorial1)
        {
            if (skipTutorial2)
            {
                _landschapManagerRef.fadeVlak.gameObject.SetActive(false);
                owner.SwitchState(typeof(LandschapState));
            }
            else
            {
                owner.SwitchState(typeof(LandschapTutorial2));
            }

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