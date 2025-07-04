using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandschapInitState : BaseState
{
    [SerializeField] private LandschapAgent _landschapAgent;
    [SerializeField] private LandschapManager _landschapManagerRef;
    private playerprefspoep playerPrepRef;
    private void Awake()
    {
        //_landschapAgent = GetComponent<LandschapAgent>();
        //_landschapManagerRef = GetComponent<LandschapManager>();
        
        _landschapManagerRef.cameraTutorial1.SetActive(false);
        _landschapManagerRef.cameraTutorial2.SetActive(false);
        if (FindAnyObjectByType<playerprefspoep>())
        {
            playerPrepRef = FindAnyObjectByType<playerprefspoep>();
        }
        else
        {
            //Debug.Log("Er is geen playerPref poep");
        }

        AudioListener.pause = false;
    }

    public override void OnEnter()
    {
        //Debug.Log("LandschapState OnEnter");
        _landschapAgent.huidigeStaat = LandschapAgent.LandschapStaten.LandschapInitState;
        
        if (_landschapManagerRef.skipTutorial)
        {
            owner.SwitchState(typeof(LandschapState));
        }
        else if(!_landschapManagerRef.skipTutorial)
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