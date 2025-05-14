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
    
    [Header("References:")]
    public GameObject collageCreateScreen;
    public GameObject nextButton;
    public GameObject previousButton;
    public TMP_Text pageNumberText;
    
    [Header("Picture Select Panel:")]
    public List<Texture> picturesShowingTextures = new List<Texture>();
    public List<GameObject> picturesShowingPlaces = new List<GameObject>();
    [Space]
    public float picturesPerPage;
    [SerializeField] int pageAmount;
    public int currentPageNumber;
    public int pageMin;
    public int pageMax;
    
    private void Awake()
    {
        _collageAgentRef = GetComponent<CollageAgent>();
        _colManagerRef = GetComponent<CollageManager>();
    }

    public override void OnEnter()
    {
        _collageAgentRef.huidigeStaat = CollageAgent.Collagestaten.CollageCreateState;
        collageCreateScreen.SetActive(true);
        PictureAlign();
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