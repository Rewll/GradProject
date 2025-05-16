using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;


public class PictureSelectManager : MonoBehaviour
{
    [SerializeField] private CollageCreateState managerRef;
    [SerializeField] private CollageEditManager editManagerRef;
    
    [SerializeField] private float collageSmallScale;
    [SerializeField] private Vector2 collageSmallPos;
    [SerializeField] private float collageFullScale;
    [SerializeField] private Vector2 collageFullPos;
    [Space]
    [SerializeField] GameObject selectedPicture;
    
    public void SetSelected(GameObject selected)
    {
        foreach (GameObject obj in managerRef.picturesToCollageWithObjects)
        {
            if (obj == selected)
            {
                selectedPicture = obj;
                obj.GetComponent<PictureSelectObject>().SetSelectedVisual(true);
            }
            else
            {
                obj.GetComponent<PictureSelectObject>().SetSelectedVisual(false);
            }
        }
    }

    public void ResetSelected()
    {
        selectedPicture = null;
        foreach (GameObject obj in managerRef.picturesToCollageWithObjects)
        {
            obj.GetComponent<PictureSelectObject>().SetSelectedVisual(false);
        }
    }
    
    public void SetPicturePanel(bool state)
    {
        RectTransform collageRT = managerRef.Collage.GetComponent<RectTransform>();
        if (state)
        {
            collageRT.localScale = new Vector3(collageSmallScale, collageSmallScale, collageSmallScale);
            collageRT.anchoredPosition = collageSmallPos;
        }
        else if (!state)
        {
            collageRT.localScale = new Vector3(collageFullScale, collageFullScale, collageFullScale);
            collageRT.anchoredPosition = collageFullPos;
        }
    }

    public void AddPictureToCollage()
    {
        if (selectedPicture != null)
        {
            editManagerRef.AddPictureToCollage(selectedPicture);
        }
    }
}
