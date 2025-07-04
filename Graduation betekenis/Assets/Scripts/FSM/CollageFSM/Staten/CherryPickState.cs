using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

public class CherryPickState : BaseState
{
    private CollageAgent _collageAgentRef;
    private CollageManager _colManagerRef;

    [Header("References:")] 
    public GameObject cherryPickScreen;
    public GameObject nextButton;
    public GameObject previousButton;
    public TMP_Text pageNumberText;
    public TMP_Text selectieTekst;
    public GameObject cherryPickPrefab;
    public GameObject cherryPickParent;
    public GameObject teveelFotosScherm;
    public GameObject teWeinigFotosScherm;

    [Header("Picture Cherrypick:")] public List<RectTransform> picturesShowingPlaces = new List<RectTransform>();
    public List<GameObject> pictureCherryObjects = new List<GameObject>();
    public List<GameObject> cherryPickObjectsVisible = new List<GameObject>();
    [Space] public float picturesPerPage;
    [SerializeField] int pageAmount;
    public int currentPageNumber;
    public int pageMin;
    public int pageMax;
    public int amountOfSelectedPictures;
    [Space] 
    public bool skipTutorial;

    private void Awake()
    {
        _collageAgentRef = GetComponent<CollageAgent>();
        _colManagerRef = GetComponent<CollageManager>();
    }

    public override void OnEnter()
    {
        _collageAgentRef.huidigeStaat = CollageAgent.Collagestaten.CherryPickState;

        cherryPickScreen.SetActive(true);
        PictureInit();
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
        cherryPickScreen.SetActive(false);
    }

    void PictureInit()
    {
        for (int i = 0; i < _colManagerRef.picturesMade.Count; i++)
        {
            Texture picture = _colManagerRef.picturesMade[i];
            GameObject newCherryPickObject = Instantiate(cherryPickPrefab);
            CherryPickObject cherryScriptRef = newCherryPickObject.GetComponent<CherryPickObject>();

            RectTransform cherryRect = newCherryPickObject.GetComponent<RectTransform>();
            cherryRect.SetParent(cherryPickParent.GetComponent<RectTransform>(), false);
            cherryRect.anchoredPosition = Vector2.zero;
            //newCherryPickObject.SetActive(false);
            cherryScriptRef.textureIndex = i;
            cherryScriptRef.pictureTexture = picture;
            cherryScriptRef.pictureImage.texture = cherryScriptRef.pictureTexture;
            newCherryPickObject.name = "CherryPickObject " + cherryScriptRef.textureIndex;
            pictureCherryObjects.Add(newCherryPickObject);
            cherryScriptRef.onClick.AddListener(SelectionUpdate);
            cherryScriptRef.onSecondClick.AddListener(SelectionUpdate);
        }
    }

    void PictureAlign()
    {
        pageAmount = Mathf.CeilToInt(_colManagerRef.picturesMade.Count / picturesPerPage);
        pageMin = (currentPageNumber * (int)picturesPerPage) - (int)picturesPerPage;
        pageMax = pageMin + (int)picturesPerPage;
        cherryPickObjectsVisible.Clear();
        foreach (GameObject obj in pictureCherryObjects)
        {
            obj.transform.GetChild(0).gameObject.SetActive(false);
        }

        for (int i = 0; i < pictureCherryObjects.Count; i++)
        {
            GameObject cherryPickObject = pictureCherryObjects[i];
            if (i >= pageMin && i < pageMax)
            {
                cherryPickObjectsVisible.Add(cherryPickObject);
            }
        }

        for (int i = 0; i < cherryPickObjectsVisible.Count; i++) //put all visible cherrypick objects at the ricght pos and turned on
        {
            GameObject cherryPickObject = cherryPickObjectsVisible[i];
            cherryPickObject.GetComponent<RectTransform>().anchoredPosition = picturesShowingPlaces[i].anchoredPosition;
            cherryPickObject.transform.GetChild(0).gameObject.SetActive(true);
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

    public void SelectionCheck()
    {
        if (amountOfSelectedPictures > _colManagerRef.amountOfPicturesToCollageWith)
        {
            teveelFotosScherm.SetActive(true);
        }
        else if (amountOfSelectedPictures <= 0)
        {
            teWeinigFotosScherm.SetActive(true);
        }
        else if (amountOfSelectedPictures <= _colManagerRef.amountOfPicturesToCollageWith)
        {
            foreach (GameObject obj in pictureCherryObjects)
            {
                if (obj.GetComponent<CherryPickObject>().isClicked)
                {
                    _colManagerRef.picturesToCollageWith.Add(obj.GetComponent<CherryPickObject>().pictureTexture);
                }
            }
            
            if (_colManagerRef.skipTutorial)
            {
                owner.SwitchState(typeof(CollageCreateState));
            }
            else
            {
                owner.SwitchState(typeof(CollageTutorialState));
            }
        }
    }

    public void SelectionUpdate()
    {
        int amount = 0;
        foreach (GameObject obj in pictureCherryObjects)
        {
            if (obj.GetComponent<CherryPickObject>().isClicked)
            {
                amount++;
            }
        }

        //Debug.Log(amount);
        amountOfSelectedPictures = amount;
        selectieTekst.text = amountOfSelectedPictures + "/" + _colManagerRef.amountOfPicturesToCollageWith + " geselecteerd";
    }
}