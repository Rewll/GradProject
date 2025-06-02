using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerKameraState : BaseState
{
    private PlayerAgent _playerAgentRef;
    public PictureDisplay picDisplayRef;
    [Space]
    [Header("Kamera variables")]
    public RenderTexture rendText;
    [Space] 
    public RawImage latestPictureShowImage;

    private Texture2D _fotoTexture;
    
    private void Awake()
    {
        _playerAgentRef = GetComponent<PlayerAgent>();
        SetGameObjects(false);  
    }

    public override void OnEnter()
    {
        //Debug.Log("kamera");
        _playerAgentRef.huidigeStaat = PlayerStates.KameraState;
        
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
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            _playerAgentRef.playerLookRef.MouseLook();
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
        StartCoroutine(PictureRoutine(_playerAgentRef.inFabriek));
    }
    
    IEnumerator PictureRoutine(bool Fabriek)
    {
        yield return new WaitForEndOfFrame();
        RenderTexture.active = rendText;
        int width = rendText.width;
        int height = rendText.height;
        _fotoTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
        Rect rect = new Rect(0, 0, width, height);
        _fotoTexture.ReadPixels(rect, 0, 0);
        _fotoTexture.Apply();
        latestPictureShowImage.texture = _fotoTexture;
        //pictureTextures.Add(fotoTexture);
        if (!Fabriek)
        {
            picDisplayRef.MakePictureGameObject(_fotoTexture);
        }

        if (Fabriek)
        {
            if (!_playerAgentRef.heeftFoto)
            {
                _playerAgentRef.heeftFoto = true;
            }
            _playerAgentRef.fabriekFoto = _fotoTexture;
        }

    }
}