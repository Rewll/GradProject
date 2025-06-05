using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

public class CollageEditManager : MonoBehaviour
{
    public GameObject pictureInCollagePrefab;
    public Canvas mainCanvas;
    public RectTransform pictureInCollageParent;
    [SerializeField] private Vector2 pictureStartPos;
    [Space] 
    public List<GameObject> picturesInCollage = new List<GameObject>();
    public void AddPictureToCollage(GameObject picture)
    {
        GameObject newPictureInCollage = Instantiate(pictureInCollagePrefab);
        RawImage image = newPictureInCollage.GetComponent<RawImage>();
        RectTransform rt = newPictureInCollage.GetComponent<RectTransform>();
        rt.anchoredPosition = pictureStartPos;
        image.texture = picture.GetComponent<RawImage>().texture;
        rt.SetParent(pictureInCollageParent, false);
        
        newPictureInCollage.GetComponent<PictureInCollage>().canvas = mainCanvas;
        picturesInCollage.Add(newPictureInCollage);
    }
}
