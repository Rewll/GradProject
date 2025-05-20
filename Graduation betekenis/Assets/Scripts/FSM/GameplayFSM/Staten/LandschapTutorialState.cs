using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LandschapTutorialState : BaseState
{
    private Agent _agent;
    private GameManager _gameManagerRef;

    public Vector2 startPlayerRotation;
    public float screenFadeTime = 2f;

    private void Awake()
    {
        _agent = GetComponent<Agent>();
        _gameManagerRef = GetComponent<GameManager>();
    }

    public override void OnEnter()
    {
        _gameManagerRef.fadeVlak.gameObject.SetActive(true);
        _gameManagerRef.fadeVlak.DOFade(1, 0.001f);
        _gameManagerRef.SetFreezePlayer(true);
        _gameManagerRef.SetPlayerRotation(startPlayerRotation.x, startPlayerRotation.y);
        StartCoroutine(TutorialRoutine());
    }

    IEnumerator TutorialRoutine()
    {
        yield return new WaitForSeconds(1f);
        Tween fadeTween = _gameManagerRef.fadeVlak.DOFade(0, screenFadeTime);
        yield return fadeTween.WaitForCompletion();
        _gameManagerRef.fadeVlak.gameObject.SetActive(false);
        _gameManagerRef.SetFreezePlayer(false);
        //tutorial dingen
        yield return new WaitForSeconds(2f);
        //owner.SwitchState(typeof(FabriekWerkState));
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