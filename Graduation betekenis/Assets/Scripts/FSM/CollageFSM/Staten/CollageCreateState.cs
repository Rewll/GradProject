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
    [Space]
    public GameObject Collage;
    
    
    [Header("Picture Select Panel:")]
    public List<Texture> picturesShowingTextures = new List<Texture>();
    public List<GameObject> picturesToCollageWithObjects = new List<GameObject>();
    [SerializeField] GameObject selectedPicture;
    [Space] 
    public float collageSmallScale;
    public Vector2 collageSmallPos;
    public float collageFullScale;
    public Vector2 collageFullPos;
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
        selectedPicture = null;
    }

    public override void OnEnter()
    {
        _collageAgentRef.huidigeStaat = CollageAgent.Collagestaten.CollageCreateState;
        collageCreateScreen.SetActive(true);
        SetPicturesToCollageWith();
        //SetPicturePanel(false);
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

    public void ShowPicturesToSelect()
    {
        
    }

    public void ShowCollageFull()
    {
        
    }

    public void SetSelected(GameObject selected)
    {
        foreach (GameObject obj in picturesToCollageWithObjects)
        {
            if (obj == selected)
            {
                selectedPicture = obj;
                obj.GetComponent<PictureSelectObject>().setSelectedVisual(true);
            }
            else
            {
                obj.GetComponent<PictureSelectObject>().setSelectedVisual(false);
            }
        }
    }

    public void ResetSelected()
    {
        selectedPicture = null;
        foreach (GameObject obj in picturesToCollageWithObjects)
        {
            obj.GetComponent<PictureSelectObject>().setSelectedVisual(false);
        }
    }
    
    public void SetPicturePanel(bool state)
    {
        RectTransform collageRT = Collage.GetComponent<RectTransform>();
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