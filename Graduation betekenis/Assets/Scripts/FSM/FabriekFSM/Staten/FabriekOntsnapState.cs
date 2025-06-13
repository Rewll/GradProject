using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

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
        StartCoroutine(FabriekOutroRoutine());
    }

    IEnumerator FabriekOutroRoutine()
    {
        yield return new WaitForSeconds(1f);
        _fabriekManagerRef.fabriekAmbianceLayerRef.FadeOutSound(0,3);
        _fabriekManagerRef.fadeVlak.gameObject.SetActive(true);
        Tween fadeTween = _fabriekManagerRef.fadeVlak.DOFade(1, 1f);
        yield return fadeTween.WaitForCompletion();
        SceneManager.LoadScene(_fabriekManagerRef.volgendeSceneIndex);
    }
}