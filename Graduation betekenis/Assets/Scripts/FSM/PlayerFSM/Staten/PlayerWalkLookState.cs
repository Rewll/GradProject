using UnityEngine;

public class PlayerWalkLookState : BaseState
{
    [SerializeField] private PlayerAgent _playerAgentRef;
    [Space]
    public bool spelerMagKijken;
    public bool spelerMagLopen;
    [Header("Fabriek Variables:")]
    [SerializeField] private LayerMask fabriekLayer;
    [SerializeField] private float raycastLength;
    public FotoOphangManager ophangManagerRef;
    
    void Awake()
    {
        //_playerAgentRef = GetComponent<PlayerAgent>();
        if (_playerAgentRef.playerStartPos)
        {
            _playerAgentRef.playerStartPos.position = transform.position;
        }
    }
    
    public override void OnEnter()
    {
        if (_playerAgentRef)
        {
            _playerAgentRef.huidigeStaat = PlayerStates.WalkLookState;
        }
        else
        {
            _playerAgentRef = FindAnyObjectByType<PlayerAgent>();
            _playerAgentRef.huidigeStaat = PlayerStates.WalkLookState;
        }
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public override void OnUpdate()
    {
        if (spelerMagLopen)
        {
            _playerAgentRef.playerMoveRef.MyInput();
        }
        _playerAgentRef.playerMoveRef.GroundCheck();
        _playerAgentRef.playerMoveRef.SpeedControl();
        _playerAgentRef.playerMoveRef.KeepPlayerAfloat();
        
        if (spelerMagKijken)
        {
            _playerAgentRef.playerLookRef.MouseInput();
        }
        _playerAgentRef.playerLookRef.MouseLook();
        
        if (Input.GetKeyDown(_playerAgentRef.CameraKnop))
        {
            _playerAgentRef.kameraAnimator.SetTrigger("TrEnable");
            owner.SwitchState(typeof(PlayerKameraState));
            return;
        }

        if (_playerAgentRef.inFabriek)
        {
            FotoOphangRaycast();
        }
    }

    void FotoOphangRaycast()
    {
        if (_playerAgentRef.heeftFoto)
        {
            Transform raycastPos = _playerAgentRef.fabriekRaycastPos;
            RaycastHit hit;
            Debug.DrawRay(raycastPos.position, raycastPos.TransformDirection(Vector3.forward) * raycastLength, Color.yellow); 
            if (Physics.Raycast(raycastPos.position, raycastPos.TransformDirection(Vector3.forward), out hit, raycastLength, fabriekLayer))
            {
                ophangManagerRef.selectieActief = true;  
                if (Input.GetMouseButton(0))
                {
                    if (_playerAgentRef.heeftFotoMetObject)
                    {
                        hit.transform.gameObject.GetComponent<FotoOphangManager>().fotoTexture = _playerAgentRef.fabriekFoto;
                        hit.transform.gameObject.GetComponent<FotoOphangManager>().onOphang.Invoke();
                        _playerAgentRef.heeftFoto = false;
                        _playerAgentRef.fabriekFoto = null;
                        ophangManagerRef.selectieActief = false;
                        ophangManagerRef.HerrinerDeSpeler(false);

                    }
                    else
                    {
                        ophangManagerRef.HerrinerDeSpeler(true);
                    }
   
                }
            }
            else
            {
                ophangManagerRef.selectieActief = false;
            }
        }
        else
        {
            ophangManagerRef.selectieActief = false;
        }
    }
    
    public override void OnFixedUpdate()
    {
        if (spelerMagLopen)
        {

        }
        _playerAgentRef.playerMoveRef.MovePlayer();
        _playerAgentRef.playerMoveRef.Gravity();
    }
    
    public override void OnExit()
    {
    }
}