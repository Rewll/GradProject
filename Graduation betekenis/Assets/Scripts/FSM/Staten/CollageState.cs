using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollageState : BaseState
{
    private Agent _agent;
    private GameManager _gameManagerRef;

    private void Awake()
    {
        _agent = GetComponent<Agent>();
        _gameManagerRef = GetComponent<GameManager>();
    }

    public override void OnEnter()
    {
        _agent.huidigeStaat = Agent.staten.CollageState;
        SceneManager.LoadScene("CollageScene");
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