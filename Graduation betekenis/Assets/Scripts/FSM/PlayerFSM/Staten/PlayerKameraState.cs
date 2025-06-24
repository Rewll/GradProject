using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerKameraState : BaseState
{
    private PlayerAgent _playerAgentRef;
    public KameraPictureDisplay picDisplayRef;
    
    [Space] [Header("Kamera variables")] 
    [SerializeField] private Camera kamera;
    public RenderTexture rendText;
    [Space] 
    public RawImage latestPictureShowImage;
    
    private Texture2D _fotoTexture;
    [Space] 
    public bool magZoomen;
    [SerializeField] private float zoomSensitivity;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [Space] 
    [Header("Fabriek variables")]
    public GameObject redObject;
    [SerializeField] private float raycastLength;
    [SerializeField] private bool objectInPicture;
    
    private void Awake()
    {
        _playerAgentRef = GetComponent<PlayerAgent>();
        //SetGameObjects(false);  
    }

    public override void OnEnter()
    {
        //Debug.Log("kamera");
        _playerAgentRef.huidigeStaat = PlayerStates.KameraState;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //SetGameObjects(true);
    }

    public override void OnUpdate()
    {
        //Debug.Log("kamera");
        if (Input.GetKeyDown(_playerAgentRef.CameraKnop))
        {
            _playerAgentRef.kameraAnimator.SetTrigger("TrDisable");
            owner.SwitchState(typeof(PlayerWalkLookState));
            return;
        }

        if (magZoomen)
        {
            ZoomCamera();
        }

        if (_playerAgentRef.inFabriek)
        {
            Debug.DrawRay(_playerAgentRef.kamTransform.position, _playerAgentRef.kamTransform.TransformDirection(Vector3.forward) * raycastLength, Color.red);
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
        
        
    }
    
    void ZoomCamera()
    {
        if (Input.GetMouseButtonDown(2))
        {
            kamera.fieldOfView = maxZoom;
        }
        else
        {
            float newZoom = -Input.mouseScrollDelta.y * zoomSensitivity;
        
            if ((kamera.fieldOfView + newZoom) < maxZoom && (kamera.fieldOfView + newZoom) > minZoom)
            {
                kamera.fieldOfView += newZoom;
            }
        }
    }
    
    public void MakePicture()
    {
        StartCoroutine(PictureRoutine(_playerAgentRef.inFabriek));
    }
    
    IEnumerator PictureRoutine(bool fabriek)
    {
        yield return new WaitForEndOfFrame();
        RenderTexture.active = rendText;
        int width = rendText.width;
        int height = rendText.height;
        //_fotoTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
        _fotoTexture = new Texture2D(width, height, TextureFormat.RGBA64, false);
        Rect rect = new Rect(0, 0, width, height);
        _fotoTexture.ReadPixels(rect, 0, 0);
        _fotoTexture.Apply();
        latestPictureShowImage.texture = _fotoTexture;
        //pictureTextures.Add(fotoTexture);
        if (!fabriek)
        {
            picDisplayRef.MakePictureGameObject(_fotoTexture);
        }
        else if(fabriek)
        {
            _playerAgentRef.heeftFoto = true;
            if (CheckIfObjectIsInPicture())
            {
                //Debug.Log("Foto gemaakt met object");
                _playerAgentRef.heeftFotoMetObject = true;
            }
            else
            {
                //Debug.Log("Foto gemaakt");
                _playerAgentRef.heeftFotoMetObject = false;
            }
            _playerAgentRef.fabriekFoto = _fotoTexture;
        }
    }

    private bool CheckIfObjectIsInPicture()
    {
        RaycastHit hit;
        if (Physics.Raycast(_playerAgentRef.kamTransform.position, _playerAgentRef.kamTransform.TransformDirection(Vector3.forward), out hit, raycastLength))
        {
            if (hit.transform.CompareTag("RedObject"))
            {
                return true;
            }
        }
        return false;
    }
}