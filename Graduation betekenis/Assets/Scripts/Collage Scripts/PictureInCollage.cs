using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PictureInCollage : MonoBehaviour, IPointerEnterHandler, 
                                                IPointerExitHandler, IPointerDownHandler, IPointerUpHandler , IDragHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    public Canvas canvas;
    RectTransform RTransform;
    [SerializeField] private float scaleFactor;
    [SerializeField] private float maxScale;
    [SerializeField] private float minScale;
    public Image SelectionBackgroundImage;
    [SerializeField] private bool isHold;
    private void Awake()
    {
        RTransform = GetComponent<RectTransform>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(gameObject.name + "OnSelect");
        SelectionBackgroundImage.enabled = true;
    }
    
    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log(gameObject.name + "OnDeSelect");
        SelectionBackgroundImage.enabled = false;
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
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        isHold = false;
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
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
