using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class collageTest : MonoBehaviour
{
    [SerializeField, Range(1, 1024)] private int cutAreaWidth;
    [SerializeField, Range(1, 1024)] private int cutAreaHeight;
    [SerializeField, Range(0, 1024)] private int cutSourceX;
    [SerializeField, Range(0, 1024)] private int cutSourceY;
    [Space]
    public Texture textureToCutFrom;
    public RenderTexture cutRT;
    [Space] 
    public RawImage imageToCutFrom;
    public RawImage cutPiece;
    public RectTransform selectionSquare;
    [Space] 
    public Vector2 upperRightCorner;
    public Vector2 lowerLeftCorner;
    public RectTransform upRight;
    public RectTransform lowLeft;
    [Space]
    public Vector2 mousePosition1;
    public Vector2 mousePosition2;
    
    private void Start()
    {
        textureToCutFrom = imageToCutFrom.texture;
        //DisplayWorldCorners(selectionSquare);
        //DisplayLocalCorners(selectionSquare);
        //display(selectionSquare);
    }

    private void Update()
    {
        upperRightCorner = selectionSquare.offsetMax; //up rightd
        lowerLeftCorner = selectionSquare.offsetMin; //lowleft
        //upRight.anchoredPosition = upperRightCorner;
        //lowLeft.anchoredPosition = lowerLeftCorner;
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            mousePosition1 = Input.mousePosition;
            upRight.anchoredPosition = mousePosition1;
            
        }
        if (Input.GetMouseButtonUp(0) && Input.GetKey(KeyCode.LeftControl))
        {
            mousePosition2 = Input.mousePosition;
            lowLeft.anchoredPosition = mousePosition1;
        }
    }

    public void MakePicture()
    {
        StartCoroutine(PictureRoutine());
    }

    public void MakePicturewithOwnSquare()
    {
        StartCoroutine(PictureRoutine2());
    }

    IEnumerator PictureRoutine()
    {
        yield return new WaitForEndOfFrame(); // waits until frame is done drawing
        cutRT = new RenderTexture(textureToCutFrom.width, textureToCutFrom.height, GraphicsFormat.R8G8B8A8_SRGB, GraphicsFormat.D32_SFloat_S8_UInt); //creates new RT to use
        Graphics.Blit(textureToCutFrom, cutRT); //Puts imagetocutfrom on RT
        RenderTexture.active = cutRT; //points to RT i want to use
        Rect sizeRect = new Rect(cutSourceX, cutSourceY, cutAreaWidth, cutAreaHeight); //Creates rect
        Texture2D newCutTexture = new Texture2D((int)sizeRect.width, (int)sizeRect.height, TextureFormat.RGB24, false); //Creates texture for cutout with the rect size
        
        newCutTexture.ReadPixels(sizeRect, 0, 0); //reads pixels from the rendertexture to the texture 
        newCutTexture.Apply(); //apply
        cutPiece.texture = newCutTexture;
        cutPiece.SetNativeSize();
        cutPiece.rectTransform.localScale = imageToCutFrom.rectTransform.localScale;
    }

    IEnumerator PictureRoutine2()
    {
        yield return new WaitForEndOfFrame(); // waits until frame is done drawing
        cutRT = new RenderTexture(textureToCutFrom.width, textureToCutFrom.height, GraphicsFormat.R8G8B8A8_SRGB, GraphicsFormat.D32_SFloat_S8_UInt); //creates new RT to use
        Graphics.Blit(textureToCutFrom, cutRT); //Puts imagetocutfrom on RT
        RenderTexture.active = cutRT; //points to RT i want to use
        Rect sizeRect = cuttingRect();
        Texture2D newCutTexture = new Texture2D((int)sizeRect.width, (int)sizeRect.height, TextureFormat.RGB24, false); //Creates texture for cutout with the rect size
        
        newCutTexture.ReadPixels(sizeRect, 0, 0); //reads pixels from the rendertexture to the texture 
        newCutTexture.Apply(); //apply
        cutPiece.texture = newCutTexture;
        cutPiece.SetNativeSize();
        cutPiece.rectTransform.localScale = imageToCutFrom.rectTransform.localScale;
    }

    Rect cuttingRect()
    {
        Vector3[] v = new Vector3[4];
        imageToCutFrom.rectTransform.GetWorldCorners(v);
        //0: bottom left
        //1: top left
        //2: top right
        //3: bottom right
        Debug.Log("cuttingRect");
        Debug.Log("bottomleft = " + v[0]);
        float worldWidth = v[3].x- v[0].x;
        float worldHeight = v[2].y - v[0].y;
        
        float minX = Mathf.Min(mousePosition1.x, mousePosition2.x);
        float minY = Mathf.Min(mousePosition1.y, mousePosition2.y);

        float widthRatio = imageToCutFrom.texture.width/worldWidth;
        float heightRatio = imageToCutFrom.texture.height/worldHeight;
        
        float xRect = (minX - v[0].x) * widthRatio;
        float yRect = (minY - v[0].y) * heightRatio;
        float rectWidth = Math.Abs(mousePosition1.x - mousePosition2.x) * widthRatio;
        float rectHeight = Math.Abs(mousePosition1.y - mousePosition2.y) * heightRatio;
        
        Debug.Log("xRect = " + xRect); 
        Debug.Log("yRect = " + yRect);
        Debug.Log("rectWidth = " + rectWidth);
        Debug.Log("rectHeight = " + rectHeight);
        
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
    void DisplayWorldCorners(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);

        Debug.Log("World Corners: ");
        for (var i = 0; i < 4; i++)
        {
            Debug.Log("World Corner " + i + " : " + v[i]);
        }
    }
    
    
    void DisplayLocalCorners(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];

        //rt.rotation = Quaternion.AngleAxis(45, Vector3.forward);
        rt.GetLocalCorners(v);

        Debug.Log("Local Corners: ");
        for (var i = 0; i < 4; i++)
        {
            Debug.Log("Local Corner " + i + " : " + v[i]);
        }
    }

    void display(RectTransform RT)
    {
        Debug.Log("RT.offsetMin.x: " + RT.offsetMin.x); //Left
        Debug.Log("RT.offsetMax.x: " + RT.offsetMax.x); //Right
        Debug.Log("RT.offsetMax.y: " + RT.offsetMax.y); //Top
        Debug.Log("RT.offsetMin.y: " + RT.offsetMin.y); //Bottom
        /*/*Left#1# rectTransform.offsetMin.x;
        /*Right#1# rectTransform.offsetMax.x;
        /*Top#1# rectTransform.offsetMax.y;
        /*Bottom#1# rectTransform.offsetMin.y;*/
    }
}
