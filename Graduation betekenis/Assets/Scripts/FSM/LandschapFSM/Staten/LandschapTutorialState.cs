using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;

public class LandschapTutorialState : BaseState
{
    private LandschapAgent _landschapAgent;
    private LandschapManager _landschapManagerRef;

    public float screenFadeTime = 2f;

    [Space] [Header("Objects Refs")]
    public GameObject tutorialScherm1;
 
    private void Awake()
    {
        _landschapAgent = GetComponent<LandschapAgent>();
        _landschapManagerRef = GetComponent<LandschapManager>();
        
        tutorialScherm1.SetActive(false);
    }

    public override void OnEnter()
    {
        _landschapAgent.huidigeStaat = LandschapAgent.LandschapStaten.LandschapTutorialState;
        
        tutorialScherm1.SetActive(true);
        _landschapManagerRef.fadeVlak.gameObject.SetActive(true);
        _landschapManagerRef.fadeVlak.DOFade(1, 0.001f);
        _landschapManagerRef.playerAgentRef.SetPlayerState(PlayerStates.SleepState);
        StartCoroutine(TutorialRoutine());
    }

    public IEnumerator TutorialRoutine()
    {
        yield return new WaitForSeconds(2f);
        Tween fadeTween = _landschapManagerRef.fadeVlak.DOFade(0, screenFadeTime);
        _landschapManagerRef.SetCursorMode(1);
        yield return fadeTween.WaitForCompletion();
        _landschapManagerRef.fadeVlak.gameObject.SetActive(false);
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

    public void StartSpel()
    {
        StartCoroutine(EindeTutorialRoutine());
    }

    IEnumerator EindeTutorialRoutine()
    {
        _landschapManagerRef.fadeVlak.gameObject.SetActive(true);
        Tween endFade = _landschapManagerRef.fadeVlak.DOFade(1, screenFadeTime);
        yield return endFade.WaitForCompletion();
        tutorialScherm1.SetActive(false);
        owner.SwitchState(typeof(LandschapState));
    }
}