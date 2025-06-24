using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FabriekTutorialState : BaseState
{
    private FabriekAgent _fabriekAgentRef;
    private FabriekManager _fabriekManagerRef;

    private bool _spelerHeeftfotoGemaakt;
    private bool _spelerHeeftFotoOpgehangen;
    
    [Header("Tutorial references")] 
    public Button fotoMaakKnop;

    public FotoOphangManager ophangRef;
    [Space]
    public List <GameObject> tutorialObjects = new List<GameObject>();

    [SerializeField] private Vector2 spelerStartRotatie;
    private PlayerWalkLookState _walkLookRef; 

    private void Awake()
    {
        _fabriekAgentRef = GetComponent<FabriekAgent>();
        _fabriekManagerRef = GetComponent<FabriekManager>();
        foreach (var obj in tutorialObjects)
        {
            obj.SetActive(false);
        }
        _walkLookRef = _fabriekManagerRef.playerAgentRef.gameObject.GetComponent<PlayerWalkLookState>();
    }

    void SetTutorialEventsListeners(bool state)
    {
        if (state)
        {
            fotoMaakKnop.onClick.AddListener(ZetFotoGemaaktOpTrue);
            ophangRef.onOphang.AddListener(ZetOpgehangenOpTrue);
        }
        else
        {
            fotoMaakKnop.onClick.RemoveAllListeners();
            ophangRef.onOphang.RemoveAllListeners();
        }
    }
    
    public override void OnEnter()
    {
        _fabriekAgentRef.huidigeStaat = FabriekAgent.FabriekStaten.FabriekTutorialState;
    

        SetTutorialEventsListeners(true);
        _fabriekManagerRef.fadeVlak.gameObject.SetActive(true);
        _fabriekManagerRef.fadeVlak.DOFade(1, 0.001f);
        _fabriekManagerRef.playerAgentRef.SetPlayerState(PlayerStates.SleepState);
        _walkLookRef.spelerMagKijken = false;
        _walkLookRef.spelerMagLopen = false;

        StartCoroutine(TutorialRoutine());
    }
    
    IEnumerator TutorialRoutine()
    {

        yield return new WaitForSeconds(4f);
        _fabriekManagerRef.playerAgentRef.SetPlayerLookDirection(0,90);
        _fabriekManagerRef.fabriekAmbianceLayerRef.FadeInSound(0,5f);
        Tween fadeTween = _fabriekManagerRef.fadeVlak.DOFade(0, 3f);
        yield return fadeTween.WaitForCompletion();
        _fabriekManagerRef.fadeVlak.gameObject.SetActive(false);
        _fabriekManagerRef.playerAgentRef.SetPlayerState(PlayerStates.WalkLookState);

        _walkLookRef.spelerMagKijken = true;
        tutorialObjects[0].SetActive(true);
        yield return new WaitUntil(CheckForLookInput);
        
        yield return new WaitForSeconds(2f);
        tutorialObjects[0].SetActive(false);
        tutorialObjects[1].SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(_fabriekManagerRef.playerAgentRef.CameraKnop));
        
        yield return new WaitForSeconds(2f);
        tutorialObjects[1].SetActive(false);
        tutorialObjects[2].SetActive(true);
        yield return new WaitForSeconds(3f);
        
        tutorialObjects[2].SetActive(false);
        tutorialObjects[3].SetActive(true);
        yield return new WaitUntil(() => CheckForLookInput() && Input.GetMouseButton(1));
        yield return new WaitForSeconds(2f);
        
        tutorialObjects[3].SetActive(false);
        tutorialObjects[4].SetActive(true);
        yield return new WaitUntil(() => _spelerHeeftfotoGemaakt);
        _spelerHeeftfotoGemaakt = false;
        
        tutorialObjects[4].SetActive(false);
        yield return new WaitForSeconds(1f);
        tutorialObjects[5].SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(_fabriekManagerRef.playerAgentRef.CameraKnop));
        
        tutorialObjects[5].SetActive(false);
        yield return new WaitForSeconds(1f);
        tutorialObjects[6].SetActive(true);
        _walkLookRef.spelerMagLopen = true;
        yield return new WaitUntil(CheckForWalkInput);
        yield return new WaitForSeconds(1f);
        tutorialObjects[6].SetActive(false);
        yield return new WaitForSeconds(1f);
        tutorialObjects[7].SetActive(true);
        yield return new WaitForSeconds(2f);
        tutorialObjects[7].SetActive(false);
        
        yield return new WaitForSeconds(2f);
        tutorialObjects[8].SetActive(true);
        yield return new WaitForSeconds(2f);
        tutorialObjects[9].SetActive(true);
        yield return new WaitForSeconds(1f);
        _fabriekManagerRef.machineAnim.SetTrigger("TrEnter");
        Debug.Log("Wachten op fotomaken");
        yield return new WaitUntil(() => _spelerHeeftfotoGemaakt);
        Debug.Log("foto gemaakt");

        yield return new WaitForSeconds(1f);
        tutorialObjects[10].SetActive(true);
        yield return new WaitUntil(() => _spelerHeeftFotoOpgehangen);
        yield return new WaitForSeconds(1f);
        tutorialObjects[11].SetActive(true);
        yield return new WaitForSeconds(1f);
        owner.SwitchState(typeof(FabriekWerkState));
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

    void ZetFotoGemaaktOpTrue()
    {
        _spelerHeeftfotoGemaakt = true;
    }

    void ZetOpgehangenOpTrue()
    {
        _spelerHeeftFotoOpgehangen = true;
    }
    public override void OnUpdate()
    {
       
    }
    
    public override void OnFixedUpdate()
    {
        
    }
    
    public override void OnExit()
    {
        SetTutorialEventsListeners(false);
    }
}