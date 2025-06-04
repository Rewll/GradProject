using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CollageCreateState : BaseState
{
    private CollageAgent _collageAgentRef;
    private CollageManager _colManagerRef;
    
    
    [Header("References:")]
    public GameObject collage;
    public GameObject collageCreateScreen;
    public GameObject pictureInCollagePrefab;
    public Canvas mainCanvas;
    public RectTransform pictureInCollageParent;
    [Space] 
    [SerializeField] private List<RectTransform> pictureStartPositions = new List<RectTransform>();
    public CollageCuttingManager collCutRef;
    public RawImage collagePreview;

    public GameObject tutorialObject;
    public GameObject tutorialObject2;

    public RectTransform knipselsPos;
    
    [Header("Collage stuff:")] 
    [SerializeField] private float collageSmallScale;
    [SerializeField] private Vector2 collageSmallPosition;
    [SerializeField] private float collageFullScale;
    [SerializeField] private Vector2 collageFullPosition;
    [Space]
    private RectTransform collageRT;
    [SerializeField] private float collageDoneScale;
    [SerializeField] private Vector2 collageDonePosition;
    [Space]
    public List<Button> buttonsThatUseSelect = new List<Button>();
    public Button layerUpButton;
    public Button layerDownButton;
    [Header("Picture stuff:")]
    public List<GameObject> picturesInCollage = new List<GameObject>();
    public GameObject selectedPicture;
    [Space] 
    public bool tutorial;
    
    
    private void Awake()
    {
        _collageAgentRef = GetComponent<CollageAgent>();
        _colManagerRef = GetComponent<CollageManager>();
        collageRT = collage.GetComponent<RectTransform>();
    }

    public override void OnEnter()
    {
        _collageAgentRef.huidigeStaat = CollageAgent.Collagestaten.CollageCreateState;

        
        collageCreateScreen.SetActive(true);
        OnDeselectGlobal();
        SetPicturesToCollageWith(_colManagerRef.picturesToCollageWith);
        SetPicturesPos(picturesInCollage);
        
        if (tutorial)
        {
            tutorialObject.SetActive(true);
            tutorialObject2.SetActive(true);
        }
        else
        {
            tutorialObject.SetActive(false);
            tutorialObject2.SetActive(false);
        }
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
    
    void SetPicturesToCollageWith(List<Texture> pictures)
    {
        for (int i = 0; i < pictures.Count; i++)
        {
            GameObject newPictureInCollage = Instantiate(pictureInCollagePrefab);
            newPictureInCollage.name = "Picture in collage " + picturesInCollage.Count;
            RectTransform rt = newPictureInCollage.GetComponent<RectTransform>();
            RawImage image = rt.GetChild(1).GetComponent<RawImage>();
            image.texture = pictures[i];
            rt.SetParent(pictureInCollageParent, false);
            rt.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            newPictureInCollage.GetComponent<PictureInCollage>().canvas = mainCanvas;
            newPictureInCollage.GetComponent<PictureInCollage>().gameManRef = this;
            picturesInCollage.Add(newPictureInCollage);
        }
    }

    void SetPicturesPos(List<GameObject> pictures)
    {
        for (int i = 0; i < pictures.Count; i++)
        {
            RectTransform rt = pictures[i].GetComponent<RectTransform>();
            rt.anchoredPosition = pictureStartPositions[i].anchoredPosition;
        }
    }

    void setCutPiece()
    {
        
    }
    
    public void SetFullScreen(bool setState)
    {
        
        if (setState)
        {
            collageRT.localScale = new Vector3(collageFullScale, collageFullScale, collageFullScale);
            collageRT.anchoredPosition = collageFullPosition;
        }
        else if (!setState)
        {
            collageRT.localScale = new Vector3(collageSmallScale, collageSmallScale, collageSmallScale);
            collageRT.anchoredPosition = collageSmallPosition;
        }
    }
    
    public void PassTextureToCut()
    {
        collCutRef.CutInit(selectedPicture.GetComponent<RectTransform>().GetChild(1).GetComponent<RawImage>().texture);
    }

    public void AddCutsToCollage()
    {
        foreach (GameObject obj in collCutRef.CutPieceObjects)
        {
            obj.name = "Picture in collage " + picturesInCollage.Count;
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.SetParent(pictureInCollageParent, false);
            rt.anchoredPosition = knipselsPos.anchoredPosition;
            rt.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            rt.GetChild(1).localScale = Vector3.one;
            float pictureScale = rt.GetChild(1).GetComponent<RectTransform>().localScale.x;
            float selectionImageScale = pictureScale + (pictureScale * 0.08f);
            rt.GetChild(0).localScale = new Vector3(selectionImageScale,selectionImageScale,selectionImageScale);
            
            picturesInCollage.Add(obj);
        }
    }

    void SetLayerButtons()
    {
        layerDownButton.interactable = true;
        layerUpButton.interactable = true;
        if (selectedPicture.transform.GetSiblingIndex() == 0)
        {
            layerDownButton.interactable = false;
        }
        else if (selectedPicture.transform.GetSiblingIndex() > selectedPicture.transform.parent.childCount - 1)
        { 
            layerUpButton.interactable = false;
        }
    }
    public void MoveImageLayer(bool upOrDown)
    {
        int siblingIndex = selectedPicture.transform.GetSiblingIndex();
        
        if (upOrDown)
        {
            Debug.Log("Move up");
            selectedPicture.transform.SetSiblingIndex(siblingIndex++);
        }
        else if(!upOrDown)
        {
            Debug.Log("Move down");
            selectedPicture.transform.SetSiblingIndex(siblingIndex--);
        }
        SetLayerButtons();
    }
    
    
    public void SetSelected(GameObject selected)
    {
        if (selectedPicture != null)
        {
            selectedPicture.GetComponent<PictureInCollage>().OnDeselect();
        }
        selectedPicture = selected;
        OnSelectGlobal();
        selectedPicture.GetComponent<PictureInCollage>().OnSelect();
    }

    public void Deselect()
    {
        if (selectedPicture != null)
        {
            selectedPicture.GetComponent<PictureInCollage>().OnDeselect();
        }
        selectedPicture = null;
        OnDeselectGlobal();
    }

    public void OnSelectGlobal()
    {
        foreach (Button button in buttonsThatUseSelect)
        {
            button.interactable = true;
        }
        SetLayerButtons();
    }

    public void OnDeselectGlobal()
    {
        foreach (Button button in buttonsThatUseSelect)
        {
            button.interactable = false;
        }
    }
    
    public void LaadCollageKlaarScherm(bool setState)
    {
        if (setState)
        {
            collageRT.localScale = new Vector3(collageDoneScale, collageDoneScale, collageDoneScale);
            collageRT.anchoredPosition = collageDonePosition;
        }
        else if (!setState)
        {
            collageRT.localScale = new Vector3(collageSmallScale, collageSmallScale, collageSmallScale);
            collageRT.anchoredPosition = collageSmallPosition;
        }
    }
    
    public void RenderCollageToTexture()
    {
        StartCoroutine(CollageRenderRoutine());
        
    }

    IEnumerator CollageRenderRoutine()
    {
        yield return new WaitForEndOfFrame(); // waits until frame is done drawing
        //RenderTexture.active = _colManagerRef.collageRT; //points to RT i want to use
        _colManagerRef.collageTexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false); //Creates texture for cutout with the rect size
        Rect sizeRect = new Rect(0 , 0, Screen.width,Screen.height);
        
        _colManagerRef.collageTexture.ReadPixels(sizeRect, 0, 0); //reads pixels from the rendertexture to the texture 
        _colManagerRef.collageTexture.Apply(); //apply
        
        collagePreview.texture = _colManagerRef.collageTexture;
        _colManagerRef.collagePreviewScherm.SetActive(true);
    }
    
    private IEnumerator CoroutineScreenshot()
    {
        yield return new WaitForEndOfFrame();
        _colManagerRef.collageTexture = new Texture2D(1920, 1080, TextureFormat.RGB24, false); //Creates texture for cutout with the rect size
        //Rect rect = new Rect(0, 0, width, height);
        //screenShotTexture.ReadPixels(rect, 0, 0); //print rectangle on the texture, 0, 0 post on the texture
        //screenShotTexture.Apply();

        //byte[] byteArray = screenShotTexture.EncodeToPNG();
        //System.IO.File.WriteAllBytes("Assets/Resources/Gezichten/Emoji" + plekNummer + ".png", byteArray);
        //Sprite tempSprite = Sprite.Create(screenShotTexture, rect, new Vector2(0.5f, 0.5f));
    }
}