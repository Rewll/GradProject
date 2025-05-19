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
    [SerializeField] private CollageCuttingManager cuttingManagerRef;
    [Space]
    [SerializeField] private float collageSmallScale;
    [SerializeField] private Vector2 collageSmallPos;
    [SerializeField] private float collageFullScale;
    [SerializeField] private Vector2 collageFullPos;
    [Space]
    [SerializeField] GameObject selectedPicture;
    

    

    
    public void SetPicturePanel(bool state)
    {
        RectTransform collageRT = managerRef.collage.GetComponent<RectTransform>();
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

    public void AddCuttedPicturesToCollage()
    {

    }

    public void PassTextureToCutScreen()
    {
        cuttingManagerRef.textureToCutFrom = selectedPicture.GetComponent<RawImage>().texture;
    }
}
