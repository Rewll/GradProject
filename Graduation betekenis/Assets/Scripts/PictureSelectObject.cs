using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PictureSelectObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler
{
    public PictureSelectManager pictSelManageRef;
    public Image selectionBackground;
    
    private void Awake()
    {
        SetSelectedVisual(false);
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
        //pictSelManageRef.SetSelected(gameObject);
    }

    public void SetSelectedVisual(bool isSelected)
    {
        selectionBackground.enabled = isSelected;
    }
}
