using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FabriekOntsnapState : BaseState
{
    private FabriekAgent _fabriekAgentRef;
    private FabriekManager _fabriekManagerRef;

    private void Awake()
    {
        _fabriekAgentRef = GetComponent<FabriekAgent>();
        _fabriekManagerRef = GetComponent<FabriekManager>();
    }

    public override void OnEnter()
    {
        _fabriekAgentRef.huidigeStaat = FabriekAgent.FabriekStaten.FabriekOntsnapState;
        StartCoroutine(OntsnapRoutine());
    }

    IEnumerator OntsnapRoutine()
    {
        _fabriekManagerRef.SetDeur(true);
        yield return new WaitForSeconds(5f);
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

    public void GaNaarLandschap()
    {
        SceneManager.LoadScene(_fabriekManagerRef.volgendeSceneIndex);
    }
}