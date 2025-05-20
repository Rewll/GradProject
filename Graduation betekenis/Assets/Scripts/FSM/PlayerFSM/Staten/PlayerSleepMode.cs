using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSleepMode : BaseState
{
    private Player _playerRef;

    void Awake()
    {
        _playerRef = GetComponent<Player>();
    }

    public override void OnEnter()
    {
       
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