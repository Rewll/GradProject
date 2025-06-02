using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabriekWerkState : BaseState
{
    private Agent _agent;
    private GameManager _gameManagerRef;

    public float werkTijd;
    private void Start()
    {
        _agent = GetComponent<Agent>();
        _gameManagerRef = GetComponent<GameManager>();
    }

    public override void OnEnter()
    {
        _agent.huidigeStaat = Agent.staten.FabriekWerkState;
        _gameManagerRef.fadeVlak.gameObject.SetActive(false);

        StartCoroutine(werkRoutine());
    }
    
    public override void OnUpdate()
    {
       
    }

    IEnumerator werkRoutine()
    {
        yield return new WaitForSeconds(werkTijd);
        owner.SwitchState(typeof(FabriekOntsnapState));
    }
    public override void OnFixedUpdate()
    {
        
    }
    
    public override void OnExit()
    {
       
    }
}