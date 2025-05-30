using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;
using Unity.UI;

public class LandschapTutorial2 : BaseState
{
    private LandschapAgent _landschapAgent;
    private LandschapManager _landschapManagerRef;

    public GameObject tutorial2Scherm;
    public float fadeToBlackTime = 2f;
    [Space] 
    public PictureDisplay picDisRef;
    
    private void Awake()
    {
        _landschapAgent = GetComponent<LandschapAgent>();
        _landschapManagerRef = GetComponent<LandschapManager>();
    }
    public override void OnEnter()
    {
        _landschapAgent.huidigeStaat = LandschapAgent.LandschapStaten.LandschapTutorialState;

        _landschapManagerRef.playerAgentRef.SetPlayerState(PlayerStates.SleepState);
        tutorial2Scherm.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public override void OnUpdate()
    {
       
    }
    
    public override void OnFixedUpdate()
    {
        
    }
    
    public override void OnExit()
    {
        tutorial2Scherm.SetActive(false);
    }

    public void BeginSpel()
    {
        StartCoroutine(EndRoutine());
    }

    IEnumerator EndRoutine()
    {
        _landschapManagerRef.fadeVlak.gameObject.SetActive(true);
        Tween fadeTween = _landschapManagerRef.fadeVlak.DOFade(1, fadeToBlackTime);
        yield return fadeTween.WaitForCompletion();
        yield return new WaitForSeconds(1f);
        _landschapManagerRef.tutorialArea.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        picDisRef.EmptyCamera();
        owner.SwitchState(typeof(LandschapState));
    }
}