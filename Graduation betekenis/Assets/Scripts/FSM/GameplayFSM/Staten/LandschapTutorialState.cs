using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JetBrains.Annotations;
using Unity.UI;

public class LandschapTutorialState : BaseState
{
    private Agent _agent;
    private GameManager _gameManagerRef;

    public Vector2 startPlayerRotation;
    public float screenFadeTime = 2f;
    public Transform playerStartPosition;

    [Space] [Header("Tutorial Objects Refs")]
    public GameObject tutorialScherm;
    
    public GameObject cameraMesh;
    public GameObject fotoMaakKnop;
    public GameObject fotoBekijkKnop;
    public GameObject laatsteFotoVisual;
    public GameObject muur;
    public GameObject uitgang;
    public List <GameObject> tutorialObjects = new List<GameObject>();
    [Header("Tutorial Variables:")] 
    public bool playerHasTakenPicture;
    public bool playerHasLookedAtPictures;
    public bool playerWalkedThrough;

    private void Awake()
    {
        _agent = GetComponent<Agent>();
        _gameManagerRef = GetComponent<GameManager>();
        tutorialScherm.SetActive(false);
        _gameManagerRef.tutorialArea.SetActive(false);
        foreach (var obj in tutorialObjects)
        {
            obj.SetActive(false);
        }
    }

    public override void OnEnter()
    {
        _agent.huidigeStaat = Agent.staten.LandschapTutorialState;

        tutorialScherm.SetActive(true);
        _gameManagerRef.tutorialArea.SetActive(true);
        _gameManagerRef.fadeVlak.gameObject.SetActive(true);
        _gameManagerRef.fadeVlak.DOFade(1, 0.001f);
        _gameManagerRef.SetFreezePlayer(true);
        _gameManagerRef.SetPlayerRotation(startPlayerRotation.x, startPlayerRotation.y);
        _gameManagerRef.TeleportPlayer(playerStartPosition.position);
        cameraMesh.SetActive(false);
        fotoMaakKnop.SetActive(false);
        fotoBekijkKnop.SetActive(false);
        laatsteFotoVisual.SetActive(false);
        StartCoroutine(TutorialRoutine());
    }

    IEnumerator TutorialRoutine()
    {
        yield return new WaitForSeconds(1f);
        Tween fadeTween = _gameManagerRef.fadeVlak.DOFade(0, screenFadeTime);
        yield return fadeTween.WaitForCompletion();
        _gameManagerRef.fadeVlak.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        _gameManagerRef.SetFreezePlayer(false);
        tutorialObjects[0].SetActive(true);
        yield return new WaitUntil(() => CheckForWalkInput());
        yield return new WaitUntil(() => CheckForLookInput());
        yield return new WaitForSeconds(2f);
        tutorialObjects[0].SetActive(false);
        yield return new WaitForSeconds(2f);
        tutorialObjects[1].SetActive(true);
        cameraMesh.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        yield return new WaitForSeconds(.5f);
        tutorialObjects[1].SetActive(false);
        tutorialObjects[2].SetActive(true);
        yield return new WaitForSeconds(4f);
        tutorialObjects[2].SetActive(false);
        tutorialObjects[3].SetActive(true);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(1));
        yield return new WaitForSeconds(3f);
        tutorialObjects[4].SetActive(true);
        fotoMaakKnop.SetActive(true);
        yield return new WaitUntil(() => playerHasTakenPicture);
        yield return new WaitForSeconds(.5f);
        tutorialObjects[4].SetActive(false);
        tutorialObjects[3].SetActive(false);
        laatsteFotoVisual.SetActive(true);
        yield return new WaitForSeconds(1f);
        tutorialObjects[5].SetActive(true);
        fotoBekijkKnop.SetActive(true);
        yield return new WaitUntil(() => playerHasLookedAtPictures);
        tutorialObjects[5].SetActive(false);
        
        tutorialObjects[6].SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        tutorialObjects[6].SetActive(false);
        tutorialObjects[7].SetActive(true);
        yield return new WaitForSeconds(1f);
        tutorialObjects[8].SetActive(true);
        muur.SetActive(false);
        uitgang.SetActive(true);
        yield return new WaitUntil(() => playerWalkedThrough);
        tutorialObjects[7].SetActive(false);
        tutorialObjects[8].SetActive(false);
        owner.SwitchState(typeof(LandschapTutorial2));
    }

    bool CheckForWalkInput()
    {
        if (Input.GetKeyDown(KeyCode.A)||
            Input.GetKeyDown(KeyCode.S)||
            Input.GetKeyDown(KeyCode.D)||
            Input.GetKeyDown(KeyCode.W))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CheckForLookInput()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        if (mouseX != 0 || mouseY != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetPlayerHasTakenPicture()
    {
        playerHasTakenPicture = true;
    }
    
    public void SetPlayerHasLookedAtPicture()
    {
        playerHasLookedAtPictures = true;
    }

    public void SetPlayerWalkedThrough()
    {
        playerWalkedThrough = true;
    }
    
    public override void OnUpdate()
    {
       
    }
    
    public override void OnFixedUpdate()
    {
        
    }
    
    public override void OnExit()
    {
        tutorialScherm.SetActive(false);
    }
}