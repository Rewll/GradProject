using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PictureSelectObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler
{
    public CollageCreateState colageCreateStateRef;
    RectTransform RTransform;
    public Image selectionBackground;
    [Space] 
    
    private bool _isClicked;
    private void Awake()
    {
        RTransform = GetComponent<RectTransform>();
        setSelectedVisual(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        colageCreateStateRef.SetSelected(gameObject);
    }

    public void setSelectedVisual(bool isSelected)
    {
        selectionBackground.enabled = isSelected;
    }
}
