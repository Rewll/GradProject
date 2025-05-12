using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PictureDisplay : MonoBehaviour
{
    public List<GameObject> pictures = new List<GameObject>();
    [Space]
    public GameObject displayPicturePrefab;
    public GameObject picturesViewScreen;
    public GameObject picturesDisplayParent;
    [Space]
    public List<RawImage> picturePlaceHolders = new List<RawImage>();
    [Space] 
    public float picturesPerPage;
    [Space]
    [SerializeField] int pageAmount;
    public int currentPageNumber;
    [Space] 
    public GameObject nextButton;
    public GameObject previousButton;

    private void Start()
    {
        foreach (RawImage placeHolder in picturePlaceHolders)
        {
            placeHolder.color = new Color(placeHolder.color.r, placeHolder.color.g, placeHolder.color.b, 0);
        }
    }

    public void MakePictureGameObject(Texture2D texture)
    {
        GameObject newDisplayPicture = Instantiate(displayPicturePrefab);
        newDisplayPicture.GetComponent<RawImage>().texture = texture;
        newDisplayPicture.GetComponent<RectTransform>().SetParent(picturesDisplayParent.GetComponent<RectTransform>(), false);
        pictures.Add(newDisplayPicture);
    }
    
    public void ShowPictures()
    {
        picturesViewScreen.SetActive(true);
        PictureAlign();
    }
    
    void PictureAlign()
    {
        pageAmount = Mathf.CeilToInt(pictures.Count / picturesPerPage) ;
        for (int i = 0; i < pictures.Count; i++)
        {
            RawImage picture = pictures[i].GetComponent<RawImage>();
            
            if (i > (picturesPerPage * currentPageNumber) )
            {
                picture.enabled = false;
            }
            else if (i < (picturesPerPage * currentPageNumber))
            {
                picture.enabled = false;
            }
            else
            {
                picture.enabled = true;
                picture.GetComponent<RectTransform>().anchoredPosition = picturePlaceHolders[i].GetComponent<RectTransform>().anchoredPosition;
            }
            
        }
        if (currentPageNumber == pageAmount)
        {
            nextButton.SetActive(false);
        }
        else if (currentPageNumber < pageAmount)
        {
            nextButton.SetActive(true);
        }

        if (currentPageNumber == 0)
        {
            previousButton.SetActive(false);
        }
        else if (currentPageNumber > pageAmount)
        {
            previousButton.SetActive(true);
        }
    }

    public void NextPage()
    {
        currentPageNumber++;
        PictureAlign();
    }

    public void PreviousPage()
    {
        currentPageNumber--;
        PictureAlign();
    }
}
