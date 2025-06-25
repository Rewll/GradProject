using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CollageState : BaseState
{
    [SerializeField] private LandschapAgent _landschapAgent;
    [SerializeField] private LandschapManager _landschapManagerRef;
    
    public int collageSceneIndex;

    public UnityEvent onCollageEnter;

    private void Awake()
    {
        //_landschapAgent = GetComponent<LandschapAgent>();
        //_landschapManagerRef = GetComponent<LandschapManager>();
    }

    public override void OnEnter()
    {
        _landschapAgent.huidigeStaat = LandschapAgent.LandschapStaten.CollageState;
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