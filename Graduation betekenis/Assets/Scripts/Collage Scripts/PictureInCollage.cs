using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PictureInCollage : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler , IDragHandler, IScrollHandler,IPointerClickHandler
{
    public Canvas canvas;
    RectTransform RTransform;
    [SerializeField] private float scaleFactor;
    [SerializeField] private float maxScale;
    [SerializeField] private float minScale;
    
    [SerializeField] private bool isHold;
    private void Awake()
    {
        RTransform = GetComponent<RectTransform>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        RTransform.anchoredPosition += eventData.delta / RTransform.parent.localScale /canvas.scaleFactor;
        
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        RTransform.SetAsLastSibling();
        isHold = true;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        isHold = false;
    }

    void ResizePictureInCollage()
    {
        float newScale = Input.mouseScrollDelta.y * scaleFactor * Time.deltaTime;
        
        if ((RTransform.localScale.x + newScale) < maxScale &&
            (RTransform.localScale.x + newScale) > minScale)
        {
            RTransform.localScale += new Vector3(newScale, newScale, newScale);
        }
        
        //Debug.Log("x: " + Input.mouseScrollDelta.x);
        //Debug.Log("y: " + Input.mouseScrollDelta.y);
    }

    void RotatePictureInCollage()
    {
        
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    
    public void OnScroll(PointerEventData eventData)
    {
        
    }

    private void Update()
    {
        if (isHold)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                
            }
            else
            {
                ResizePictureInCollage();
            }
        }
    }
}
