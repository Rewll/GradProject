using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragObject: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDragHandler
{
    public Canvas canvas;
    RectTransform RTransform;
    [Space] 
    public UnityEvent onClick;
    public UnityEvent onSecondClick;

    private bool _isClicked;
    private void Awake()
    {
        RTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (!_isClicked)
        {
            onClick.Invoke();
        }
        else if (_isClicked)
        {
            onSecondClick.Invoke();
        }
    }
}