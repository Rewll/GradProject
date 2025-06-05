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
    private RectTransform _rTransform;
    [Space]
    [SerializeField] private float scaleFactor;
    [SerializeField] private float maxScale;
    [SerializeField] private float minScale;
    [SerializeField] private bool isHold;
    private void Awake()
    {
        _rTransform = GetComponent<RectTransform>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {

    }
    
    public void OnDrag(PointerEventData eventData)
    {
        _rTransform.anchoredPosition += eventData.delta / _rTransform.parent.localScale /canvas.scaleFactor;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        isHold = true;
        //RTransform.SetAsLastSibling();
        if (gameManRef.selectedPicture != this.gameObject)
        {
            gameManRef.SetSelected(this.gameObject);
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        isHold = false;
    }

    void ResizePictureInCollage()
    {
        float newScale = Input.mouseScrollDelta.y * scaleFactor * Time.deltaTime;
        
        if ((_rTransform.localScale.x + newScale) < maxScale &&
            (_rTransform.localScale.x + newScale) > minScale)
        {
            _rTransform.localScale += new Vector3(newScale, newScale, newScale);
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
        selectionBackgroundImage.enabled = true;
    }
    
    public void OnDeselect()
    {
        //Debug.Log(gameObject.name + "OnDeSelect");
        selectionBackgroundImage.enabled = false;
    }
}