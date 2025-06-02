using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;
using Unity.UI;

public class LandschapState : BaseState
{
    private LandschapAgent _landschapAgent;
    private LandschapManager _landschapManagerRef;
    public float fadeTime;
    [Header("References: ")]
    public Transform startplek;

    private void Awake()
    {
        _landschapAgent = GetComponent<LandschapAgent>();
        _landschapManagerRef = GetComponent<LandschapManager>();
    }

    public override void OnEnter()
    {
        //Debug.Log("LandschapState OnEnter");
        _landschapAgent.huidigeStaat = LandschapAgent.LandschapStaten.LandschapState;

        
        
        _landschapManagerRef.playerAgentRef.TeleportPlayer(startplek.position);
        StartCoroutine(StartRoutine());
    }

    IEnumerator StartRoutine()
    {
        Tween fadeTween = _landschapManagerRef.fadeVlak.DOFade(0, fadeTime);
        yield return fadeTween.WaitForCompletion();
        _landschapManagerRef.fadeVlak.gameObject.SetActive(false);
        _landschapManagerRef.playerAgentRef.SetPlayerState(PlayerStates.WalkLookState);
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