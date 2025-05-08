using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandschapState : BaseState
{
    public Transform startplek;
    public override void OnEnter()
    {
        //Debug.Log("LandschapState OnEnter");
        GetComponent<Agent>().huidigeStaat = Agent.staten.LandschapState;
        GetComponent<GameManager>().player.GetComponent<PlayerMove>().Teleport(startplek.position);
    }
    
    public override void OnUpdate()
    {
       
    }
    
    public override void OnExit()
    {
       
    }
}