using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSleepState : BaseState
{
    [SerializeField] private PlayerAgent _playerAgentRef;

    void Awake()
    {
       //_playerAgentRef = GetComponent<PlayerAgent>();
    }

    public override void OnEnter()
    {
        _playerAgentRef.huidigeStaat = PlayerStates.SleepState;
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