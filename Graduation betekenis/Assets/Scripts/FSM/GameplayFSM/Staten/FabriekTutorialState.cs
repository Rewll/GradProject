using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FabriekTutorialState : BaseState
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
        _agent.huidigeStaat = Agent.staten.FabriekTutorialState;
        
        _gameManagerRef.fadeVlak.gameObject.SetActive(true);
        _gameManagerRef.fadeVlak.DOFade(1, 0.001f);
        _gameManagerRef.SetFreezePlayer(true);
        StartCoroutine(TutorialRoutine());
    }

    IEnumerator TutorialRoutine()
    {
        yield return new WaitForSeconds(1f);
        _gameManagerRef.SetPlayerRotation(0, 90);
        Tween fadeTween = _gameManagerRef.fadeVlak.DOFade(0, 2f);
        yield return fadeTween.WaitForCompletion();
        _gameManagerRef.fadeVlak.gameObject.SetActive(false);
        _gameManagerRef.SetFreezePlayer(false);
        //tutorial dingen
        yield return new WaitForSeconds(2f);
        owner.SwitchState(typeof(FabriekWerkState));
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