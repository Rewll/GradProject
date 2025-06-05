using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PictureInCollage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler , IDragHandler, IPointerClickHandler
{
    [Header("References: ")]
    public Canvas canvas;
    public CollageCreateState gameManRef;
    public Image selectionBackgroundImage;
    private RectTransform _rt;
    [Space]
    [SerializeField] private float scaleFactor;
    [SerializeField] private float maxScale;
    [SerializeField] private float minScale;
    [SerializeField] private bool isHold;
    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {

    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _rt.anchoredPosition += eventData.delta / _rt.parent.parent.localScale /canvas.scaleFactor;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (gameManRef.selectedPicture != this.gameObject)
            {
                gameManRef.SetSelected(this.gameObject);
            }
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    void ResizePictureInCollage()
    {
        float newScale = Input.mouseScrollDelta.y * scaleFactor * Time.deltaTime;
        
        if ((_rt.localScale.x + newScale) < maxScale &&
            (_rt.localScale.x + newScale) > minScale)
        {
            _rt.localScale += new Vector3(newScale, newScale, newScale);
        }
    }

    void RotatePictureInCollage()
    {
        
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
    
    private void Update()
    {
        if (isHold)
        {
            ResizePictureInCollage();
        }
    }
    
    public void OnSelect()
    {
        //Debug.Log(gameObject.name + "OnSelect");
        isHold = true;
        selectionBackgroundImage.enabled = true;
    }
    
    public void OnDeselect()
    {
        //Debug.Log(gameObject.name + "OnDeSelect");
        isHold = false;
        selectionBackgroundImage.enabled = false;
    }
}