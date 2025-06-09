using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SVImageControl : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    [SerializeField] private Image pickerImage;

    private RawImage _SVImage;
    
    [SerializeField] private RectTransform svRectTransform;

    [SerializeField] private ColourpickerController CC;
    
    private RectTransform _rectTransform;
    
    private RectTransform _pickerRectTransform;


    private void Awake()
    {
        _SVImage = GetComponent<RawImage>();
        _rectTransform = GetComponent<RectTransform>();
        
        _pickerRectTransform = pickerImage.GetComponent<RectTransform>();
        //_pickerRectTransform.position = new Vector2(-(_rectTransform.sizeDelta.x * 0.5f), -(_rectTransform.sizeDelta.y * 0.5f));
    }

    void UpdateColour(PointerEventData eventData)
    {
        Vector3 pos = _rectTransform.InverseTransformPoint(eventData.position);
        
        float deltaX = svRectTransform.sizeDelta.x * 0.5f;
        float deltaY = svRectTransform.sizeDelta.y * 0.5f;

        if (pos.x < -deltaX)
        {
            pos.x = -deltaX;
        }else if (pos.x > deltaX)
        {
            pos.x = deltaX;
        }

        if (pos.y < -deltaY)
        {
            pos.y = -deltaY;
        }else if (pos.y > deltaY)
        {
            pos.y = deltaY;
        }

        float x = pos.x + deltaX;
        float y = pos.y + deltaY;
        
        float xNorm = x / svRectTransform.sizeDelta.x;
        float yNorm = y / svRectTransform.sizeDelta.y;
        _pickerRectTransform.localPosition = pos;
        pickerImage.color = Color.HSVToRGB(0, 0, 1 - yNorm);
        
        CC.SetSV(xNorm, yNorm);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateColour(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateColour(eventData);
    }
}