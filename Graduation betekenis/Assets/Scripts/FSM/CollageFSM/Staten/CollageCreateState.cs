using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CollageCreateState : BaseState
{
    private CollageAgent _collageAgentRef;
    private CollageManager _colManagerRef;
    
    [Header("References:")]
    public GameManager gmRef;
    public GameObject collage;
    public GameObject collageCreateScreen;
    public GameObject pictureInCollagePrefab;
    public GameObject textInCollagePrefab;
    public Canvas mainCanvas;
    public RectTransform pictureInCollageParent;
    [Space] 
    [SerializeField] private List<RectTransform> pictureStartPositions = new List<RectTransform>();
    public CollageCuttingManager collCutRef;
    public RawImage collagePreview;
    
    public RectTransform knipselsPos;
    
    [Header("Collage stuff:")] 
    private readonly float _collageSmallScale = 0.6473701f;
    private readonly Vector2 _collageSmallPosition = new Vector2(0, -178f);
    private readonly float _collageFullScale = 0.959199f;
    private readonly Vector2 _collageFullPosition = new Vector2(-42.6531f, -9.3674f);
    [Space]
    private RectTransform _collageRT;
    [SerializeField] private float collageDoneScale;
    [SerializeField] private Vector2 collageDonePosition;
    [Space]
    public List<Button> buttonsThatUseSelect = new List<Button>();
    public Button layerUpButton;
    public Button layerDownButton;
    public TMP_Text layerText;
    [Header("Picture stuff:")]
    public List<GameObject> picturesInCollage = new List<GameObject>();
    public GameObject selectedPicture;
    [Space] 
    public TMP_Text pathTekst;

    
    
    private void Awake()
    {
        _collageAgentRef = GetComponent<CollageAgent>();
        _colManagerRef = GetComponent<CollageManager>();
        _collageRT = collage.GetComponent<RectTransform>();
    }

    public override void OnEnter()
    {
        _collageAgentRef.huidigeStaat = CollageAgent.Collagestaten.CollageCreateState;
        
        collageCreateScreen.SetActive(true);
        OnDeselectGlobal();
        SetPicturesToCollageWith(_colManagerRef.picturesToCollageWith);
        SetPicturesPos(picturesInCollage);
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
            rt.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            newPictureInCollage.GetComponent<PictureInCollage>().canvas = mainCanvas;
            newPictureInCollage.GetComponent<PictureInCollage>().gameManRef = this;
            newPictureInCollage.GetComponent<PictureInCollage>().parentRectTransform = collage.GetComponent<RectTransform>();
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
            _collageRT.localScale = new Vector3(_collageFullScale, _collageFullScale, _collageFullScale);
            _collageRT.anchoredPosition = _collageFullPosition;
        }
        else if (!setState)
        {
            _collageRT.localScale = new Vector3(_collageSmallScale, _collageSmallScale, _collageSmallScale);
            _collageRT.anchoredPosition = _collageSmallPosition;
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
            obj.name = "CutPicture in collage " + picturesInCollage.Count;
            obj.GetComponent<PictureInCollage>().parentRectTransform = collage.GetComponent<RectTransform>();
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

    public void AddWordToCollage(string word, Color wordColor, float wordScaleFactor, float wordMaxScale)
    {
        GameObject newTextInCollage = Instantiate(textInCollagePrefab);
        newTextInCollage.transform.GetChild(1).GetComponent<TMP_Text>().text = word;
        newTextInCollage.transform.GetChild(1).GetComponent<TMP_Text>().color = wordColor;
        newTextInCollage.name = "Tekst in collage";
        RectTransform rt = newTextInCollage.GetComponent<RectTransform>();
        rt.SetParent(pictureInCollageParent, false);
        PictureInCollage picInColRef = newTextInCollage.GetComponent<PictureInCollage>();
        picInColRef.canvas = mainCanvas;
        picInColRef.gameManRef = this;
        picInColRef.parentRectTransform = collage.GetComponent<RectTransform>();
        picInColRef.scaleFactor = wordScaleFactor;
        picInColRef.maxScale = wordMaxScale;
        RectTransform selectionRT = newTextInCollage.transform.GetChild(1).GetComponent<RectTransform>();
        selectionRT.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y);
        //Debug.Log(selectionRT.sizeDelta);
    }
    void SetLayerButtons()
    {
        layerDownButton.interactable = true;
        layerUpButton.interactable = true;
        layerText.text = "Laag: " + "\n" + selectedPicture.transform.GetSiblingIndex() + " / " + (pictureInCollageParent.transform.childCount - 1);
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
        //Debug.Log("index was:"+ siblingIndex);
        if (upOrDown)
        {
            int newIndex = siblingIndex + 1;
            //Debug.Log("move up, new index" + newIndex);
            selectedPicture.transform.SetSiblingIndex(newIndex);
        }
        else if(!upOrDown)
        {
            int newIndex = siblingIndex - 1;
            //Debug.Log("move down, new index" + newIndex);
            selectedPicture.transform.SetSiblingIndex(newIndex);
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
        if (selectedPicture.GetComponent<PictureInCollage>().magGekniptWorden)
        {
            foreach (Button button in buttonsThatUseSelect)
            {
                button.interactable = true;
            }
        }
        layerText.gameObject.SetActive(true);
        SetLayerButtons();
    }

    public void OnDeselectGlobal()
    {
        foreach (Button button in buttonsThatUseSelect)
        {
            button.interactable = false;
        }
        layerText.gameObject.SetActive(false);
    }
    
    public void LaadCollageKlaarScherm()
    {
        StartCoroutine(CollageDoneRoutine());

    }
    
    IEnumerator CollageDoneRoutine()
    {
        _collageRT.localScale = new Vector3(1, 1, 1);
        _collageRT.anchoredPosition = Vector2.zero;
        yield return new WaitForEndOfFrame(); // waits until frame is done drawing
        int width = Screen.width;
        int height = Screen.height;
        
        _colManagerRef.collageTexture = new Texture2D(width,height, TextureFormat.RGB24, false); //Creates texture size of screen
        Rect sizeRect = new Rect(0, 0, width,height);
        
        _colManagerRef.collageTexture.ReadPixels(sizeRect, 0, 0); //reads pixels from the rendertexture to the texture 
        _colManagerRef.collageTexture.Apply(); //apply
        
        collagePreview.texture = _colManagerRef.collageTexture;
        _colManagerRef.collageKlaarScherm.SetActive(true);
        SetFullScreen(false);
    }

    public void SaveCollage()
    {
        byte[] byteArray = _colManagerRef.collageTexture.EncodeToPNG();
        string dateAndTime = System.DateTime.Now.ToString("dd/MM/yyyy_HH-mm-ss");
        //string path = Application.persistentDataPath + "Collage - " + dateAndTime + ".png";
        string path = System.IO.Directory.GetCurrentDirectory() + "Collage - " + dateAndTime + ".png";
        pathTekst.gameObject.SetActive(true);
        pathTekst.text = "Collage Opgeslagen in: " + path;
        System.IO.File.WriteAllBytes(path, byteArray);
        Debug.Log("Saved Collage at: " + path);
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