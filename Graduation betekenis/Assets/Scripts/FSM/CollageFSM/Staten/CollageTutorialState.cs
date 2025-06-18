using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollageTutorialState : BaseState
{
    private CollageAgent _collageAgentRef;
    private CollageManager _colManagerRef;
    [Space] 
    public GameObject tutorialScreen;
    private void Awake()
    {
        _collageAgentRef = GetComponent<CollageAgent>();
        _colManagerRef = GetComponent<CollageManager>();
    }

    public override void OnEnter()
    {
        _collageAgentRef.huidigeStaat = CollageAgent.Collagestaten.CollageTutorialState;
        _colManagerRef.collageCreateScreen.SetActive(true);
        _colManagerRef.collageTutorialScreen.SetActive(true);
    }

    public void StartCollageMaken()
    {
        owner.SwitchState(typeof(CollageCreateState));
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