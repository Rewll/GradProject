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

    [Header("Collage stuff:")] 
    [SerializeField] private float collageSmallScale;
    [SerializeField] private Vector2 collageSmallPosition;
    [SerializeField] private float collageFullScale;
    [SerializeField] private Vector2 collageFullPosition;
    [Space]
    [Header("Picture stuff:")]
    public List<GameObject> picturesInCollage = new List<GameObject>();
    public GameObject selectedPicture;
    
    
    private void Awake()
    {
        _collageAgentRef = GetComponent<CollageAgent>();
        _colManagerRef = GetComponent<CollageManager>();
    }

    public override void OnEnter()
    {
        _collageAgentRef.huidigeStaat = CollageAgent.Collagestaten.CollageCreateState;
        
        collageCreateScreen.SetActive(true);
        SetPicturesToCollageWith();
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
    
    void SetPicturesToCollageWith()
    {
        for (int i = 0; i < _colManagerRef.picturesToCollageWith.Count; i++)
        {
            GameObject newPictureInCollage = Instantiate(pictureInCollagePrefab);
            newPictureInCollage.name = "Picture in collage " + i;
            RectTransform rt = newPictureInCollage.GetComponent<RectTransform>();
            RawImage image = rt.GetChild(1).GetComponent<RawImage>();
            rt.anchoredPosition = pictureStartPositions[i].anchoredPosition;
            image.texture = _colManagerRef.picturesToCollageWith[i];
            rt.SetParent(pictureInCollageParent, false);
            
            newPictureInCollage.GetComponent<PictureInCollage>().canvas = mainCanvas;
            picturesInCollage.Add(newPictureInCollage);
        }
    }

    public void SetFullScreen(bool setState)
    {
        RectTransform collageRT = collage.GetComponent<RectTransform>();
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
        collCutRef.CutInit(EventSystem.current.currentSelectedGameObject.GetComponent<RawImage>().texture);
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