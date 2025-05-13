using System.Transactions;
using UnityEngine;

public class PlayerMove : BaseState
{
    //public PlayerLook PlayerLookRef;
    [Space]
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    [Space]
    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask whatIsGround;
    public bool grounded;
    
    public Transform orientation;
    [Space]
    [Header("Gravity")]
    public float gravityScale = 1.0f;
    
    private static float _globalGravity = -9.81f;
    
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _moveDirection;
    private Rigidbody _RB;
    private Player _playerRef;
    
    void Awake()
    {
        _playerRef = GetComponent<Player>();
        _RB = GetComponent<Rigidbody>();
        _RB.freezeRotation = true;
        _RB.linearDamping = groundDrag;
        _playerRef.kameraDisabledMesh.SetActive(false);
    }
    public override void OnEnter()
    {
        _playerRef.huidigeStaat = playerStates.WalkLookMode;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        _playerRef.kameraDisabledMesh.SetActive(true);
    }
    
    public override void OnUpdate()
    {
        // ground check
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance,whatIsGround);
        _playerRef.playerLookRef.OnUpdate();
        MyInput();
        SpeedControl();

        // handle drag
        /*if (grounded)
            RB.linearDamping = groundDrag;
        else
            RB.linearDamping = 0;*/
        if (Input.GetKeyDown(_playerRef.CameraKnop))
        {
            owner.SwitchState(typeof(Kamera));
        }
    }
    public override void OnFixedUpdate()
    {
        MovePlayer();
        Gravity();
    }
    
    public override void OnExit()
    {
        _playerRef.kameraDisabledMesh.SetActive(false);
    }

    
    void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
        _RB.AddForce(_moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
    }
    
    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_RB.linearVelocity.x, 0f, _RB.linearVelocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _RB.linearVelocity = new Vector3(limitedVel.x, _RB.linearVelocity.y, limitedVel.z);
        }
    }

    void Gravity()
    {
        if (!grounded)
        {
            Vector3 gravity = _globalGravity * gravityScale * Vector3.up;
            _RB.AddForce(gravity, ForceMode.Acceleration);
        }
    }

    public void Teleport(Vector3 destinationPos)
    {
        _RB.position = destinationPos;
    }
}