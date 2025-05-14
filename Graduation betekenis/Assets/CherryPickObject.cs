using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CherryPickObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public int textureIndex;
    public Texture pictureTexture;
    [Space]
    public UnityEvent onClick;
    public UnityEvent onSecondClick;

    private bool _isClicked;


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
