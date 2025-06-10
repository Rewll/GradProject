using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

public class OldCollageManager : MonoBehaviour
{
    public PictureStorage picStorageRef;
    public List<Texture> picturesToCollageWith = new List<Texture>();

    [Space] [Header("Picture Cherrypicking")]
    public int amountOfPicturesToCollageWith;

    public GameObject cherryPickScreen;
    
    [Header("Picture collage select Screen")]
    public List<Texture> picturesShowingTextures = new List<Texture>();
    public List<GameObject> picturesShowingPlaces = new List<GameObject>();
    public Texture testTexture;
    [Space] 
    public float picturesPerPage;
    [Space] 
    [SerializeField] int pageAmount;
    public int currentPageNumber;
    public int pageMin;
    public int pageMax;
    [Space] 
    public GameObject nextButton;
    public GameObject previousButton;
    public TMP_Text pageNumberText;
    private void Awake()
    {
        cherryPickScreen.SetActive(false);

        
        picStorageRef = FindFirstObjectByType(typeof(PictureStorage)) as PictureStorage;
        if (picStorageRef != null)
        {
            foreach (Texture pictureTexture in picStorageRef.picturesStored)
            {
                picturesToCollageWith.Add(pictureTexture);
            }
            picStorageRef.picturesStored.Clear();
            //Destroy(picStorageRef.gameObject);
            if (picturesToCollageWith.Count > amountOfPicturesToCollageWith)
            {
                LoadCherryPickScreen();
            }
            else if(picturesToCollageWith.Count >= amountOfPicturesToCollageWith)
            {
                PictureAlign();
            }
        }
        else
        {
            Debug.Log("No PictureStorage found. loading testTextures");
            for (int i = 0; i < picturesShowingPlaces.Count; i++)
            {
                picturesShowingPlaces[i].GetComponent<RawImage>().texture = testTexture;
            }
            SetPageButtons();
        }
    }

    void LoadCherryPickScreen()
    {
        cherryPickScreen.SetActive(true);
    }
    
    void PictureAlign()
    {
        pageAmount = Mathf.CeilToInt(picturesToCollageWith.Count / picturesPerPage);
        pageMin = (currentPageNumber * (int)picturesPerPage) - (int)picturesPerPage;
        pageMax = pageMin + (int)picturesPerPage;
        picturesShowingTextures.Clear();
        for (int i = 0; i < picturesToCollageWith.Count; i++)
        {
            Texture picture = picturesToCollageWith[i];
            if (i >= pageMin && i < pageMax )
            {
                picturesShowingTextures.Add(picture);
            }
        }
        
        for (int i = 0; i < picturesShowingTextures.Count; i++)
        {
            picturesShowingPlaces[i].GetComponent<RawImage>().texture = picturesShowingTextures[i];
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
}
