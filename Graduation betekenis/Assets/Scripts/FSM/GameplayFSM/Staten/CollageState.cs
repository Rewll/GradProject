using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CollageState : BaseState
{
    private Agent _agent;
    private GameManager _gameManagerRef;
    
    public int collageSceneIndex;

    public UnityEvent onCollageEnter;

    private void Awake()
    {
        _agent = GetComponent<Agent>();
        _gameManagerRef = GetComponent<GameManager>();
    }

    public override void OnEnter()
    {
        _agent.huidigeStaat = Agent.staten.CollageState;
        onCollageEnter.Invoke();
        SceneManager.LoadScene(collageSceneIndex);
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