using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class TutorialRegelaar : MonoBehaviour
{
    [Header("Script references:")]
    public PlayerAgent playerAgentRef;
    public PlayerWalkLookState walkLookRef;
    public AudioLayer audioManRef;
    [Space] 
    [Header("Object references:")] 
    public Image fadeVlak;
    public Button fotoMaakKnop;
    public Button fotoBekijkKnop;
    public Button fotoBekijkTerugKnop;
    public GameObject deur;
    [Space] 
    [Header("Bools enzo:")] 
    public bool spelerHeeftfotoGemaakt;
    public bool spelerHeeftBekeken;
    public bool spelerGingTerug;


    [Space]
    public List <GameObject> tutorialObjects = new List<GameObject>();

    void Start()
    {
        deur.SetActive(false);
        fadeVlak.gameObject.SetActive(true);
        fadeVlak.DOFade(1, 0.001f);
        playerAgentRef.SetPlayerState(PlayerStates.SleepState);
        walkLookRef.spelerMagKijken = false;
        walkLookRef.spelerMagLopen = false;
        
        fotoMaakKnop.onClick.AddListener(ZetFotoGemaaktOpTrue);
        fotoBekijkKnop.onClick.AddListener(ZetBekekenOpTrue);
        fotoBekijkTerugKnop.onClick.AddListener(ZetGingTerugOpTrue);
        
        StartCoroutine(TutorialRoutine());
    }

    IEnumerator TutorialRoutine()
    {
        audioManRef.FadeInSound(0,1f);
        playerAgentRef.SetPlayerState(PlayerStates.WalkLookState);
        playerAgentRef.SetPlayerLookDirection(0,-90);
        yield return new WaitForSeconds(3f);
        Tween fadeTween = fadeVlak.DOFade(0, 3f);
        yield return fadeTween.WaitForCompletion();
        fadeVlak.gameObject.SetActive(false);

        walkLookRef.spelerMagKijken = true;
        tutorialObjects[0].SetActive(true);
        yield return new WaitUntil(CheckForLookInput);
        yield return new WaitForSeconds(2f);
        
        tutorialObjects[0].SetActive(false);
        tutorialObjects[1].SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        yield return new WaitForSeconds(1f);
        tutorialObjects[1].SetActive(false);
        tutorialObjects[2].SetActive(true);
        yield return new WaitForSeconds(3f);
        
        tutorialObjects[2].SetActive(false);
        tutorialObjects[3].SetActive(true);
        yield return new WaitUntil(() => CheckForLookInput() && Input.GetMouseButton(1));
        yield return new WaitForSeconds(2f);
        
        tutorialObjects[3].SetActive(false);
        tutorialObjects[4].SetActive(true);
        yield return new WaitUntil(() => spelerHeeftfotoGemaakt);
        spelerHeeftfotoGemaakt = false;
        
        tutorialObjects[4].SetActive(false);
        tutorialObjects[5].SetActive(true);
        yield return new WaitUntil(() => spelerHeeftBekeken);
        tutorialObjects[5].SetActive(false);
        yield return new WaitForSeconds(1f);
        
        tutorialObjects[6].SetActive(true);
        yield return new WaitUntil(() => spelerGingTerug);
        
        tutorialObjects[6].SetActive(false);
        tutorialObjects[7].SetActive(true);
        yield return new WaitUntil(() => Input.mouseScrollDelta.y != 0);
        yield return new WaitForSeconds(2f);
        
        tutorialObjects[7].SetActive(false);
        tutorialObjects[8].SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        tutorialObjects[8].SetActive(false);
        yield return new WaitForSeconds(1f);
        tutorialObjects[9].SetActive(true);
        
        walkLookRef.spelerMagLopen = true;
        yield return new WaitUntil(() => CheckForWalkInput());
        yield return new WaitForSeconds(2f);
        tutorialObjects[9].SetActive(false);
        tutorialObjects[10].SetActive(true);
        yield return new WaitForSeconds(3f);
        tutorialObjects[10].SetActive(false);
        tutorialObjects[11].SetActive(true);
        deur.SetActive(true);
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
        spelerHeeftfotoGemaakt = true;
    }
    
    void ZetBekekenOpTrue()
    {
        spelerHeeftBekeken = true;
    }
    
    void ZetGingTerugOpTrue()
    {
        spelerGingTerug = true;
    }
    
    public void SetCursorMode(int mode)
    {
        if (mode == 0)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; 
        }
        else if (mode == 1)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; 
        }
    }
}
