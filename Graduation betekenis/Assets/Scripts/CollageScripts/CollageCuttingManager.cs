using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CollageCuttingManager : MonoBehaviour
{
    [Header("References:")]
    public CollageCreateState gameManRef;
    public pictureToCutFrom picScriptRef;
    public GameObject cutPiecePrefab;
    public RectTransform cutPieceParent;
    public RawImage pictureToCutImage;
    public Button cutButton;
    private RectTransform _pictureToCutRT;
    
    public RectTransform selectionPoint1;
    public RectTransform selectionPoint2;
    public RectTransform selectionSquare;
    public Camera cam;
    public Canvas MainCanvas;
    [Space]
    public List<GameObject> CutPieceObjects = new List<GameObject>();
    [HideInInspector] public Texture textureToCutFrom;
    RenderTexture cutRT;
    [Space] 
    private Vector2 cutSelectionPos1;
    private Vector2 cutSelectionPos2;
    public bool isCutting;

    public bool inImage;

    private Vector2 _selectionStartPos;
    private bool _selectionActive;
    
    private void Awake()
    {
        _selectionStartPos = selectionSquare.anchoredPosition;
        _pictureToCutRT = pictureToCutImage.rectTransform;
    }

    public void CutInit(Texture texture)
    {
        textureToCutFrom = texture;
        pictureToCutImage.texture = textureToCutFrom;
        ResetSelection();
        isCutting = true;
    }

    public void CutClose()
    {
        isCutting = false;
        CutPieceObjects.Clear();
    }

    private void Update()
    {
        if (isCutting)
        {
            CutSelection();
        }
    }

    bool MouseIsOverImage()
    {
        Vector2 mousePos = Input.mousePosition;
        
        Vector3[] v = new Vector3[4];
        _pictureToCutRT.GetWorldCorners(v);
        //0: bottom left
        //1: top left
        //2: top right
        //3: bottom right
        if ((mousePos.x > v[0].x && mousePos.x < v[3].x) && 
            (mousePos.y > v[0].y && mousePos.y < v[1].y))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    void CutSelection()
    {
        Vector3[] v = new Vector3[4];
        _pictureToCutRT.GetWorldCorners(v);
        //0: bottom left
        //1: top left
        //2: top right
        //3: bottom right
        Vector2 minPos = v[0];
        Vector2 maxPos = v[0];
        if (MouseIsOverImage())
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetSelectionButtons(true);
                cutSelectionPos1 = Input.mousePosition;
                
                selectionPoint1.anchoredPosition = Input.mousePosition / (Vector2)selectionPoint2.parent.localScale / MainCanvas.scaleFactor;
                selectionSquare.anchoredPosition = selectionPoint1.anchoredPosition;
            }
        
            if (Input.GetMouseButton(0))
            {
                selectionPoint2.anchoredPosition = Input.mousePosition / (Vector2)selectionPoint2.parent.localScale / MainCanvas.scaleFactor;
            
                selectionSquare.sizeDelta = new Vector2(Math.Abs(selectionPoint1.anchoredPosition.x - selectionPoint2.anchoredPosition.x),
                    Math.Abs(selectionPoint1.anchoredPosition.y - selectionPoint2.anchoredPosition.y));
            }
        
            if (Input.GetMouseButtonUp(0))
            {
                cutSelectionPos2 = Input.mousePosition;
            }
        }
    }
    
    public void MakeCut()
    {
        StartCoroutine(CuttingRoutine());
    }
    
    IEnumerator CuttingRoutine()
    {
        yield return new WaitForEndOfFrame(); // waits until frame is done drawing
        textureToCutFrom = pictureToCutImage.texture;
        cutRT = new RenderTexture(textureToCutFrom.width, textureToCutFrom.height, GraphicsFormat.R8G8B8A8_SRGB, GraphicsFormat.D32_SFloat_S8_UInt); //creates new RT to use
        Graphics.Blit(textureToCutFrom, cutRT); //Puts imagetocutfrom on RT
        RenderTexture.active = cutRT; //points to RT i want to use
        Rect sizeRect = CuttingRect();
        Texture2D newCutTexture = new Texture2D((int)sizeRect.width, (int)sizeRect.height, TextureFormat.RGB24, false); //Creates texture for cutout with the rect size
        
        newCutTexture.ReadPixels(sizeRect, 0, 0); //reads pixels from the rendertexture to the texture 
        newCutTexture.Apply(); //apply
        
        GameObject newCutPiece = Instantiate(cutPiecePrefab);
        newCutPiece.GetComponent<PictureInCollage>().gameManRef = gameManRef;
        newCutPiece.GetComponent<PictureInCollage>().canvas = MainCanvas;
        RectTransform RT = newCutPiece.GetComponent<RectTransform>();
        RawImage cutPieceImage = RT.GetChild(1).GetComponent<RawImage>();
        RT.SetParent(cutPieceParent, false);
        cutPieceImage.texture = newCutTexture;
        cutPieceImage.SetNativeSize();
        cutPieceImage.rectTransform.localScale = pictureToCutImage.rectTransform.localScale;
        cutPieceImage.rectTransform.anchoredPosition = Vector2.zero; 
        RT.GetChild(0).GetComponent<RectTransform>().sizeDelta = RT.GetChild(1).GetComponent<RectTransform>().sizeDelta;
        float scale = RT.GetChild(1).GetComponent<RectTransform>().localScale.x;
        float newScale = scale + (scale * 0.08f);
        RT.GetChild(0).GetComponent<RectTransform>().localScale =  new Vector3(newScale,newScale,newScale);
        CutPieceObjects.Add(newCutPiece);
    }
    
    
    Rect CuttingRect()
    {
        Vector3[] v = new Vector3[4];
        pictureToCutImage.rectTransform.GetWorldCorners(v);
        //0: bottom left
        //1: top left
        //2: top right
        //3: bottom right
        //Debug.Log("cuttingRect");
        //Debug.Log("bottomleft = " + v[0]);
        float worldWidth = v[3].x- v[0].x;
        float worldHeight = v[2].y - v[0].y;
        
        float minX = Mathf.Min(cutSelectionPos1.x, cutSelectionPos2.x);
        float minY = Mathf.Min(cutSelectionPos1.y, cutSelectionPos2.y);

        float widthRatio = pictureToCutImage.texture.width/worldWidth;
        float heightRatio = pictureToCutImage.texture.height/worldHeight;
        
        float xRect = (minX - v[0].x) * widthRatio;
        float yRect = (minY - v[0].y) * heightRatio;
        float rectWidth = Math.Abs(cutSelectionPos1.x - cutSelectionPos2.x) * widthRatio;
        float rectHeight = Math.Abs(cutSelectionPos1.y - cutSelectionPos2.y) * heightRatio;
        
        //Debug.Log("xRect = " + xRect); 
        //Debug.Log("yRect = " + yRect);
        //Debug.Log("rectWidth = " + rectWidth);
        //Debug.Log("rectHeight = " + rectHeight);
        
        /*return new Rect(xRect * imageToCutFrom.rectTransform.localScale.x,
                        yRect * imageToCutFrom.rectTransform.localScale.y,
                   rectWidth * imageToCutFrom.rectTransform.localScale.x,
                  rectHeight * imageToCutFrom.rectTransform.localScale.y);*/
        return new Rect(
            xRect , 
            yRect , 
            rectWidth , 
            rectHeight );
    }

    public void ResetSelection()
    {
        selectionSquare.anchoredPosition = _selectionStartPos;
        selectionPoint1.anchoredPosition = _selectionStartPos;
        selectionPoint2.anchoredPosition = _selectionStartPos;
        selectionSquare.sizeDelta = new Vector2(10,10);
        SetSelectionButtons(false);
    }

    public void SetSelectionButtons(bool state)
    {
        if (state)
        {
            cutButton.interactable = true;
        }
        else
        {
            cutButton.interactable = false;
        }
    }
}
