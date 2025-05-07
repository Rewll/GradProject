using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandschapState : BaseState
{
    public Transform startplek;
    private void Start()
    {
        
    }

    public override void OnEnter()
    {
        GetComponent<Agent>().huidigeStaat = Agent.staten.LandschapState;
        GetComponent<GameManager>().player.transform.position = startplek.position;
    }
    
    public override void OnUpdate()
    {
       
    }
    
    public override void OnExit()
    {
       
    }
}