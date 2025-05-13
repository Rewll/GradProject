using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Kamera : BaseState
{
    private Player playerRef;
    [Header("Kamera variables")]
    public RenderTexture rendText;
    [Space] 
    public RawImage latestPicture;
    public List<Texture2D> pictureTextures = new List<Texture2D>();
    
    public PictureDisplay picDisplayRef;
    
    private void Start()
    {
        playerRef = GetComponent<Player>();
        SetGameObjects(false);

    }

    public override void OnEnter()
    {
        //Debug.Log("kamera");
        playerRef.huidigeStaat = playerStates.CameraMode;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SetGameObjects(true);
    }

    public override void OnUpdate()
    {
        //Debug.Log("kamera");
        if (Input.GetKeyDown(playerRef.CameraKnop))
        {
            owner.SwitchState(typeof(PlayerMove));
            return;
        }

        if (Input.GetMouseButton(1))
        {
            playerRef.playerLookRef.OnUpdate();
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }else if (Cursor.lockState == CursorLockMode.Locked)
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
        foreach (GameObject kameraGObject in playerRef.kameraModeGameObjects)
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
        latestPicture.texture = fotoTexture;
        pictureTextures.Add(fotoTexture);
        picDisplayRef.MakePictureGameObject(fotoTexture);
    }
}