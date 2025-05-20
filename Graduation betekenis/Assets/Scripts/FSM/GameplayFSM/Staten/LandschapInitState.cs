using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandschapInitState : BaseState
{
    private Agent _agent;
    private GameManager _gameManagerRef;
    public bool skipTutorial;
    [Header("References: ")]
    public Transform startplek;

    private void Awake()
    {
        _agent = GetComponent<Agent>();
        _gameManagerRef = GetComponent<GameManager>();
    }

    public override void OnEnter()
    {
        //Debug.Log("LandschapState OnEnter");
        _agent.huidigeStaat = Agent.staten.LandschapInitState;
        _gameManagerRef.audioManRef.PlaySound(0);
        if (skipTutorial)
        {
            _gameManagerRef.fadeVlak.gameObject.SetActive(false);
            owner.SwitchState(typeof(LandschapState));
        }
        else
        {
            _gameManagerRef.fadeVlak.gameObject.SetActive(true);
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