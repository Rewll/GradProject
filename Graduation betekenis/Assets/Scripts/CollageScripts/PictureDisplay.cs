using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

public class PictureDisplay : MonoBehaviour
{
    public GameObject displayPicturePrefab;
    public PictureStorage picStorageRef;
    [Space]
    public GameObject picturesViewScreen;
    public GameObject picturesDisplayParent;
    [Space]
    public List<RawImage> picturePlaceHolders = new List<RawImage>();
    public List<GameObject> pictures = new List<GameObject>();
    List<GameObject> picturesInGrid = new List<GameObject>();
    [Space] public float picturesPerPage;
    [Space] [SerializeField] int pageAmount;
    public int currentPageNumber;
    public int pageMin;
    public int pageMax;
    [Space] public GameObject nextButton;
    public GameObject previousButton;
    public TMP_Text pageNumberText;

    private void Awake()
    {
        foreach (RawImage placeHolder in picturePlaceHolders)
        {
            placeHolder.color = new Color(placeHolder.color.r, placeHolder.color.g, placeHolder.color.b, 0);
        }
        picturesViewScreen.SetActive(false);
    }
    
    public void MakePictureGameObject(Texture2D texture)
    {
        GameObject newDisplayPicture = Instantiate(displayPicturePrefab);
        newDisplayPicture.name = (pictures.Count + 1).ToString();
        newDisplayPicture.GetComponent<RawImage>().texture = texture;
        newDisplayPicture.GetComponent<RectTransform>().SetParent(picturesDisplayParent.GetComponent<RectTransform>(), false);
        pictures.Add(newDisplayPicture);
    }

    public void EmptyCamera()
    {
        foreach (GameObject obj in pictures)
        {
            Destroy(obj);
        }
        pictures.Clear();
        
        PictureAlign();
    }
    
    public void ShowPictures()
    {
        //Debug.Log("Showing picture display");
        picturesViewScreen.SetActive(true);
        PictureAlign();
    }

    void PictureAlign()
    {
        pageAmount = Mathf.CeilToInt(pictures.Count / picturesPerPage);
        pageMin = (currentPageNumber * (int)picturesPerPage) - (int)picturesPerPage;
        pageMax = pageMin + (int)picturesPerPage;
        picturesInGrid.Clear();
        for (int i = 0; i < pictures.Count; i++)
        {
            GameObject picture = pictures[i];
            if (i >= pageMin && i < pageMax )
            {
                picturesInGrid.Add(picture);
            }
            else
            {
                picture.SetActive(false);
            }
        }

        for (int i = 0; i < picturesInGrid.Count; i++)
        {
            picturesInGrid[i].SetActive(true);
            picturesInGrid[i].GetComponent<RectTransform>().anchoredPosition = picturePlaceHolders[i].GetComponent<RectTransform>().anchoredPosition;
        }

        SetPageButtons();
    }

    void SetPageButtons()
    {
        if (currentPageNumber >= pageAmount)
        {
            nextButton.SetActive(false);
        }
        else if (currentPageNumber < pageAmount)
        {
            nextButton.SetActive(true);
        }

        if (currentPageNumber <= 1)
        {
            previousButton.SetActive(false);
        }
        else if (currentPageNumber <= pageAmount)
        {
            previousButton.SetActive(true);
        }

        if (pageAmount > 1)
        {
            pageNumberText.gameObject.SetActive(true);
        }
        else
        {
            pageNumberText.gameObject.SetActive(false);
        }
        string pageNumberFormatted = currentPageNumber.ToString() + "/" + pageAmount.ToString();
        pageNumberText.text = pageNumberFormatted;
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

    public void SavePictures()
    {
        foreach (GameObject pictureObject in pictures)
        {
            Texture texture = pictureObject.GetComponent<RawImage>().texture;
            picStorageRef.picturesStored.Add(texture);
        }
    }
}