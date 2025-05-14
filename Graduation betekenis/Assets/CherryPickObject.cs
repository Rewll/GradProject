using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CherryPickObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public RawImage pictureImage;
    public Image selectedBackground;
    [Space]
    public int textureIndex;
    public Texture pictureTexture;
    [Space]
    public UnityEvent onClick;
    public UnityEvent onSecondClick;

    public bool isClicked;

    private void Awake()
    {
        selectedBackground.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    { 
        if (!isClicked)
        {
            onClick.Invoke();
            isClicked = true;
            selectedBackground.enabled = true;
        }
        else if (isClicked)
        {
            onSecondClick.Invoke();
            isClicked = false;
            selectedBackground.enabled = false;
        }
    }
}
