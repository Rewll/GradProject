using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

public class CollageCreateState : BaseState
{
    private CollageAgent _collageAgentRef;
    private CollageManager _colManagerRef;
    
    
    public GameObject Collage;
    public GameObject collageCreateScreen;
    
    [Header("Picture Select Panel:")]
    [HideInInspector] public List<Texture> picturesShowingTextures = new List<Texture>();
    public List<GameObject> picturesToCollageWithObjects = new List<GameObject>();
    
    float picturesPerPage;
    int pageAmount;
    int currentPageNumber;
    int pageMin;
    int pageMax;
    
    GameObject nextButton;
    GameObject previousButton;
    TMP_Text pageNumberText;
    
    
    private void Awake()
    {
        _collageAgentRef = GetComponent<CollageAgent>();
        _colManagerRef = GetComponent<CollageManager>();
    }

    public override void OnEnter()
    {
        _collageAgentRef.huidigeStaat = CollageAgent.Collagestaten.CollageCreateState;
        
        collageCreateScreen.SetActive(true);
        SetPicturesToCollageWith();
    }
    
    public override void OnUpdate()
    {
       
    }
    
    public override void OnFixedUpdate()
    {
        
    }
    
    public override void OnExit()
    {
       
    }

    void SetPicturesToCollageWith()
    {
        for (int i = 0; i < _colManagerRef.picturesToCollageWith.Count; i++)
        {
            picturesToCollageWithObjects[i].GetComponent<RawImage>().texture = _colManagerRef.picturesToCollageWith[i];
        }
    }
    
    void PictureAlign()
    {
        pageAmount = Mathf.CeilToInt(_colManagerRef.picturesToCollageWith.Count / picturesPerPage);
        pageMin = (currentPageNumber * (int)picturesPerPage) - (int)picturesPerPage;
        pageMax = pageMin + (int)picturesPerPage;
        picturesShowingTextures.Clear();
        for (int i = 0; i < _colManagerRef.picturesToCollageWith.Count; i++)
        {
            Texture picture = _colManagerRef.picturesToCollageWith[i];
            if (i >= pageMin && i < pageMax )
            {
                picturesShowingTextures.Add(picture);
            }
        }
        
        for (int i = 0; i < picturesShowingTextures.Count; i++)
        {
            picturesToCollageWithObjects[i].GetComponent<RawImage>().texture = picturesShowingTextures[i];
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