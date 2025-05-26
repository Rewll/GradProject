using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Kamera : BaseState
{
    private PlayerAgent _playerAgentRef;
    public PictureDisplay picDisplayRef;
    [Space]
    [Header("Kamera variables")]
    public RenderTexture rendText;
    [Space] 
    public RawImage latestPictureShowImage;
    
    private void Awake()
    {
        _playerAgentRef = GetComponent<PlayerAgent>();
        SetGameObjects(false);  
    }

    public override void OnEnter()
    {
        //Debug.Log("kamera");
        _playerAgentRef.huidigeStaat = playerStates.CameraMode;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SetGameObjects(true);
    }

    public override void OnUpdate()
    {
        //Debug.Log("kamera");
        if (Input.GetKeyDown(_playerAgentRef.CameraKnop))
        {
            owner.SwitchState(typeof(PlayerWalkLookState));
            return;
        }

        if (Input.GetMouseButton(1))
        {
            _playerAgentRef.playerLookRef.OnUpdate();
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    
    public override void OnFixedUpdate()
    {
        
    }
    
    public override void OnExit()
    {
        SetGameObjects(false);
        
    }
    
    public void SetGameObjects(bool status)
    {
        foreach (GameObject kameraGObject in _playerAgentRef.kameraModeGameObjects)
        {
            kameraGObject.SetActive(status);
        }
    }
    
    public void MakePicture()
    {
        StartCoroutine(PictureRoutine());
    }

    IEnumerator PictureRoutine()
    {
        yield return new WaitForEndOfFrame();
        RenderTexture.active = rendText;
        int width = rendText.width;
        int height = rendText.height;
        Texture2D fotoTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
        Rect rect = new Rect(0, 0, width, height);
        fotoTexture.ReadPixels(rect, 0, 0);
        fotoTexture.Apply();
        latestPictureShowImage.texture = fotoTexture;
        //pictureTextures.Add(fotoTexture);
        picDisplayRef.MakePictureGameObject(fotoTexture);
    }
}