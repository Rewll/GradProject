using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollageInitState : BaseState
{
    private CollageAgent _collageAgentRef;
    private CollageManager _colManagerRef;
    
    public PictureStorage picStorageRef;
    private void Awake()
    {
        _collageAgentRef = GetComponent<CollageAgent>();
        _colManagerRef = GetComponent<CollageManager>();
    }

    public override void OnEnter()
    {
        _collageAgentRef.huidigeStaat = CollageAgent.Collagestaten.CollageInitState;
        picStorageRef = FindFirstObjectByType(typeof(PictureStorage)) as PictureStorage;
        if (picStorageRef)
        {
            foreach (Texture pictureTexture in picStorageRef.picturesStored)
            {
                _colManagerRef.picturesMade.Add(pictureTexture);
            }
            picStorageRef.picturesStored.Clear();
            //Destroy(picStorageRef.gameObject);
            if (_colManagerRef.picturesMade.Count > _colManagerRef.amountOfPicturesToCollageWith)
            { //if there are lot of pictures, cherrypicking needed
                owner.SwitchState(typeof(CherryPickState));
            }
            else if(_colManagerRef.picturesMade.Count < _colManagerRef.amountOfPicturesToCollageWith)
            { //if there are less pictures than the amount then no cherrypicking needed
                foreach (Texture picture in _colManagerRef.picturesMade)
                {
                    _colManagerRef.picturesToCollageWith.Add(picture);
                }
                owner.SwitchState(typeof(CollageCreateState));
            }
        }
        else
        { //if no picturestorage then use testtextures
            Debug.Log("No PictureStorage found. loading testTextures to create with");
            for (int i = 0; i < _colManagerRef.amountOfPicturesToCollageWith; i++)
            {
                _colManagerRef.picturesToCollageWith.Add(_colManagerRef.testTexture);
            }
            owner.SwitchState(typeof(CollageCreateState));
        }
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
}