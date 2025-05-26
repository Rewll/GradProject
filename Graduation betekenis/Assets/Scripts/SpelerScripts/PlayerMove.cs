using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Space] [Header("Movement")] public float moveSpeed;
    public float groundDrag;
    [Space] [Header("Ground Check")] public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation;
    [Space] [Header("Gravity")] 
    public float gravityScale = 1.0f;

    [SerializeField] private bool playerCanFallBelowGround;
    [SerializeField] private float underNeathGroundTreshold;
    
    [Space] 
    private static float _globalGravity = -9.81f;
    
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _moveDirection;
    private Rigidbody _RB;
    private PlayerAgent _playerAgentRef;
    Vector3 lastGroundPos;
    
    void Awake()
    {
        _RB = GetComponent<Rigidbody>();
        _RB.freezeRotation = true;
        _RB.linearDamping = groundDrag;
        _playerAgentRef.kameraDisabledMesh.SetActive(false);
    }
    
    public void GroundCheck()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance,whatIsGround);
        if (grounded)
        {
            lastGroundPos = transform.position;
        }
    }

    public void HandleDrag()
    {
        if (grounded)
            _RB.linearDamping = groundDrag;
        else
            _RB.linearDamping = 0;
    }
    public void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    public void MovePlayer()
    {
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
        _RB.AddForce(_moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
    }
    
    public void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_RB.linearVelocity.x, 0f, _RB.linearVelocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _RB.linearVelocity = new Vector3(limitedVel.x, _RB.linearVelocity.y, limitedVel.z);
        }
    }

    public void Gravity()
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
    public void KeepplayerAfloat()
    {
        if (transform.position.y < underNeathGroundTreshold && !grounded)
        {
            //Debug.Log("speler onderwater!");
            Teleport(lastGroundPos);
        }
    }
    public void SetPlayerRotation(float xRotation, float yRotation)
    {
        _playerAgentRef.playerLookRef.xRotation = xRotation;
        _playerAgentRef.playerLookRef.yRotation = yRotation;
    }
}
