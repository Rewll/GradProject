using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;
using Unity.UI;

public class LandschapState : BaseState
{
    private Agent _agent;
    private GameManager _gameManagerRef;
    public float fadeTime;
    [Header("References: ")]
    public Transform startplek;

    private void Awake()
    {
        _agent = GetComponent<Agent>();
        _gameManagerRef = GetComponent<GameManager>();
    }

    public override void OnEnter()
    {
        //Debug.Log("LandschapState OnEnter");
        _agent.huidigeStaat = Agent.staten.LandschapState;

        
        
        _gameManagerRef.TeleportPlayer(startplek.position);
        StartCoroutine(StartRoutine());
    }

    IEnumerator StartRoutine()
    {
        Tween fadeTween = _gameManagerRef.fadeVlak.DOFade(0, fadeTime);
        yield return fadeTween.WaitForCompletion();
        _gameManagerRef.fadeVlak.gameObject.SetActive(false);
        _gameManagerRef.SetFreezePlayer(false);
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

    public void StartCollageFase()
    {
        owner.SwitchState(typeof(CollageState));
    }
    
}