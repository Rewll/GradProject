using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FabriekTutorialState : BaseState
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
        _fabriekAgentRef.huidigeStaat = FabriekAgent.FabriekStaten.FabriekTutorialState;
        
        _fabriekManagerRef.fadeVlak.gameObject.SetActive(true);
        _fabriekManagerRef.fadeVlak.DOFade(1, 0.001f);
        //_fabriekManagerRef.SetFreezePlayer(true);
        StartCoroutine(TutorialRoutine());
    }

    IEnumerator TutorialRoutine()
    {
        yield return new WaitForSeconds(1f);
        //_landschapManagerRef.SetPlayerRotation(0, 90);
        Tween fadeTween = _fabriekManagerRef.fadeVlak.DOFade(0, 2f);
        yield return fadeTween.WaitForCompletion();
        _fabriekManagerRef.fadeVlak.gameObject.SetActive(false);
        //_landschapManagerRef.SetFreezePlayer(false);
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