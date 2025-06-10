using UnityEngine;

public class PlayerWalkLookState : BaseState
{
    private PlayerAgent _playerAgentRef;
    [Header("Fabriek Variables:")]
    [SerializeField] private LayerMask fabriekLayer;
    [SerializeField] private float raycastLength;
    
    void Awake()
    {
        _playerAgentRef = GetComponent<PlayerAgent>();
    }
    
    public override void OnEnter()
    {
        _playerAgentRef.huidigeStaat = PlayerStates.WalkLookState;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public override void OnUpdate()
    {
        _playerAgentRef.playerMoveRef.MyInput();
        _playerAgentRef.playerMoveRef.GroundCheck();
        _playerAgentRef.playerMoveRef.SpeedControl();
        _playerAgentRef.playerLookRef.MouseLook();
        
        if (Input.GetKeyDown(_playerAgentRef.CameraKnop))
        {
            _playerAgentRef.kameraAnimator.SetTrigger("TrEnable");
            owner.SwitchState(typeof(PlayerKameraState));
            return;
        }

        if (_playerAgentRef.inFabriek && _playerAgentRef.heeftFoto)
        {
            FotoOphangRaycast();
        }
    }

    void FotoOphangRaycast()
    {
        Transform raycastPos = _playerAgentRef.fabriekRaycastPos;
        RaycastHit hit;
        Debug.DrawRay(raycastPos.position, raycastPos.TransformDirection(Vector3.forward) * raycastLength, Color.yellow); 
        if (Physics.Raycast(raycastPos.position, raycastPos.TransformDirection(Vector3.forward), out hit, raycastLength, fabriekLayer))
        {
            //Debug.Log("Did Hit"); 
            if (Input.GetMouseButton(0))
            {
                hit.transform.gameObject.GetComponent<FotoOphangManager>().HangFotoOp(_playerAgentRef.fabriekFoto);
                _playerAgentRef.heeftFoto = false;
                _playerAgentRef.fabriekFoto = null;
            }
        }
    }
    
    public override void OnFixedUpdate()
    {
        _playerAgentRef.playerMoveRef.MovePlayer();
        _playerAgentRef.playerMoveRef.Gravity();
    }
    
    public override void OnExit()
    {
    }
}