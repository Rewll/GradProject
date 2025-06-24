using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;

public class LandschapState : BaseState
{
    private LandschapAgent _landschapAgent;
    private LandschapManager _landschapManagerRef;
    public float screenFadeTime = 2f;
    [Header("References: ")]
    public AudioLayer landMarkLayer;
    public LandMarkManager landMarkManagerRef;
    public GameObject geenFotosScherm;
    public GameObject geenFotosEindeScherm;
    [Space] 
    public bool spelerHeeftGekeken;
    private void Awake()
    {
        _landschapAgent = GetComponent<LandschapAgent>();
        _landschapManagerRef = GetComponent<LandschapManager>();
        if (!_landschapManagerRef.skipTutorial)
        {
            _landschapManagerRef.fotoKijkKnop.onClick.AddListener(ZetSpelerHeeftGekeken);
        }
    }

    public override void OnEnter()
    {
        _landschapAgent.huidigeStaat = LandschapAgent.LandschapStaten.LandschapState;
        
        _landschapManagerRef.SetCursorMode(0);
        _landschapManagerRef.fadeVlak.gameObject.SetActive(true);
        _landschapManagerRef.fadeVlak.DOFade(1, 0.001f);
    
        _landschapManagerRef.audioManRef.FadeInSound(0, 5f);
        StartCoroutine(StartRoutine());
    }

    IEnumerator StartRoutine()
    {
        yield return new WaitForSeconds(2f);
        _landschapManagerRef.playerAgentRef.SetPlayerLookDirection(-0.525f, -78.85f);
        _landschapManagerRef.playerAgentRef.SetPlayerState(PlayerStates.WalkLookState);
        Tween fadeTween = _landschapManagerRef.fadeVlak.DOFade(0, screenFadeTime);
        yield return fadeTween.WaitForCompletion();
        _landschapManagerRef.fadeVlak.gameObject.SetActive(false);
        landMarkManagerRef.ActivateLandMark(0);
        if (!_landschapManagerRef.skipTutorial)
        {
            StartCoroutine(TutorialRoutine());
        }
    }

    IEnumerator TutorialRoutine()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        _landschapManagerRef.cameraTutorial1.SetActive(true);
        yield return new WaitUntil(() => Input.mouseScrollDelta.y != 0);
        yield return new WaitForSeconds(2f);
        _landschapManagerRef.cameraTutorial1.SetActive(false);
        _landschapManagerRef.cameraTutorial2.SetActive(true);
        yield return new WaitUntil(() => spelerHeeftGekeken);
        _landschapManagerRef.cameraTutorial2.SetActive(false);
        _landschapManagerRef.fotoKijkKnop.onClick.RemoveAllListeners();
    }

    public void ZetSpelerHeeftGekeken()
    {
        spelerHeeftGekeken = true;
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

    public void FotoCheck()
    {
        if (_landschapManagerRef.picDisRef.pictures.Count <= 0)
        {
            Debug.Log("0 foto's gemaakt");
            geenFotosScherm.SetActive(true);
        }
        else
        {
            StartCollageFase();
        }
    }

    public void GeenFotosEindeSpelLaden()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        geenFotosEindeScherm.SetActive(true);
    }

    public void DeurOpen(bool state)
    {
        switch (state)
        {
            case true:
                _landschapManagerRef.playerAgentRef.SetPlayerState(PlayerStates.SleepState);
                break;
            case false:
                _landschapManagerRef.playerAgentRef.SetPlayerState(PlayerStates.WalkLookState);
                break;
        }
        
    }
}