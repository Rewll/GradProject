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
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    
    public Transform orientation;
    [Space]
    [Header("Gravity")]
    public float gravityScale = 1.0f;
    float globalGravity = -9.81f;
    
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody RB;
    private Player playerRef;
    
    void Start()
    {
        playerRef = GetComponent<Player>();
        RB = GetComponent<Rigidbody>();
        RB.freezeRotation = true;
        RB.linearDamping = groundDrag;
        playerRef.kameraDisabledMesh.SetActive(false);
    }
    public override void OnEnter()
    {
        playerRef.huidigeStaat = playerStates.WalkLookMode;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        playerRef.kameraDisabledMesh.SetActive(true);
    }
    
    public override void OnUpdate()
    {
        // ground check
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance,whatIsGround);
        playerRef.playerLookRef.OnUpdate();
        MyInput();
        SpeedControl();

        // handle drag
        /*if (grounded)
            RB.linearDamping = groundDrag;
        else
            RB.linearDamping = 0;*/
        if (Input.GetKeyDown(playerRef.CameraKnop))
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
        playerRef.kameraDisabledMesh.SetActive(false);
    }

    
    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        RB.AddForce(moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
    }
    
    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(RB.linearVelocity.x, 0f, RB.linearVelocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            RB.linearVelocity = new Vector3(limitedVel.x, RB.linearVelocity.y, limitedVel.z);
        }
    }

    void Gravity()
    {
        if (!grounded)
        {
            Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            RB.AddForce(gravity, ForceMode.Acceleration);
        }
    }

    public void Teleport(Vector3 destinationPos)
    {
        RB.position = destinationPos;
    }
}