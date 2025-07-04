using System;
using System.Collections;
using System.Collections.Generic;
using SimpleCursor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PictureInCollage : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("References: ")]
    public Canvas canvas;
    public CollageCreateState gameManRef;
    public Image selectionBackgroundImage;
    public RectTransform parentRectTransform;
    private RectTransform _rt;
    [Space]
    public float scaleFactor;
    public float maxScale;
    public float minScale;
    [SerializeField] private bool isSelected;
    public float rotationSpeed;
    public bool magGekniptWorden;
    private float _rotationVelocity;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!gameManRef.gmRef.paused)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                CursorUtilities.ChangeCursor(CursorType.Move);
            }
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (!gameManRef.gmRef.paused)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                _rt.anchoredPosition += eventData.delta / parentRectTransform.localScale /canvas.scaleFactor;
            }
        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!gameManRef.gmRef.paused)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                CursorUtilities.ChangeCursor(default);
            }
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!gameManRef.gmRef.paused)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (gameManRef.selectedPicture != this.gameObject)
                {
                    gameManRef.SetSelected(this.gameObject);
                }
            }
        }
    }
    
    void ResizePictureInCollage()
    {
        if (!gameManRef.gmRef.paused)
        {
            float newScale = Input.mouseScrollDelta.y * scaleFactor;
        
            if ((_rt.localScale.x + newScale) < maxScale &&
                (_rt.localScale.x + newScale) > minScale)
            {
                _rt.localScale += new Vector3(newScale, newScale, newScale);
            }
        }
    }

    void RotatePictureInCollage()
    {
        _rotationVelocity = Input.mousePositionDelta.x * rotationSpeed;
        transform.Rotate(Vector3.back, -_rotationVelocity, Space.Self);
    }
    
    private void Update()
    {
        if (!gameManRef.gmRef.paused)
        {
            if (isSelected)
            {
                ResizePictureInCollage();
                if (Input.GetMouseButton(1))
                {
                    RotatePictureInCollage();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    CursorUtilities.ChangeCursor(CursorType.Rotate);
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    CursorUtilities.ChangeCursor(CursorType.Default);
                }
            }
        }
    }
    
    public void OnSelect()
    {
        //Debug.Log(gameObject.name + "OnSelect");
        isSelected = true;
        selectionBackgroundImage.enabled = true;
    }
    
    public void OnDeselect()
    {
        //Debug.Log(gameObject.name + "OnDeSelect");
        isSelected = false;
        selectionBackgroundImage.enabled = false;
    }


}