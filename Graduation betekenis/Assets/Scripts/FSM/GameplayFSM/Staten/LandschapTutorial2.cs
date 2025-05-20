using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;
using Unity.UI;

public class LandschapTutorial2 : BaseState
{
    private Agent _agent;
    private GameManager _gameManagerRef;

    public GameObject tutorial2Scherm;
    public float fadeToBlackTime = 2f;
    [Space] 
    public PictureDisplay picDisRef;
    
    private void Awake()
    {
        _agent = GetComponent<Agent>();
        _gameManagerRef = GetComponent<GameManager>();
        
    }
    public override void OnEnter()
    {
        _agent.huidigeStaat = Agent.staten.LandschapTutorialState;
        _gameManagerRef.SetFreezePlayer(true);
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
        _gameManagerRef.fadeVlak.gameObject.SetActive(true);
        Tween fadeTween = _gameManagerRef.fadeVlak.DOFade(1, fadeToBlackTime);
        yield return fadeTween.WaitForCompletion();
        yield return new WaitForSeconds(1f);
        _gameManagerRef.tutorialArea.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        picDisRef.EmptyCamera();
        owner.SwitchState(typeof(LandschapState));
    }
}