using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Space] [Header("Movement")] 
    public float moveSpeed;
    private float _currentSpeed;
    public float groundDrag;
    [SerializeField] private bool debugRunEnabled;
    [Space] [Header("Ground Check")] 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation;
    [Space] [Header("Gravity")] 
    public float gravityScale = 1.0f;
    [SerializeField] private float underNeathGroundTreshold;
    private const float GlobalGravity = -9.81f;

    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _moveDirection;
    private Rigidbody _rb;
    private PlayerAgent _playerAgentRef;
    private Vector3 _lastGroundPos;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _rb.linearDamping = groundDrag;
    }
    
    public void GroundCheck()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance,whatIsGround);
        if (grounded)
        {
            _lastGroundPos = transform.position;
        }
    }

    public void HandleDrag()
    {
        if (grounded)
            _rb.linearDamping = groundDrag;
        else
            _rb.linearDamping = 0;
    }
    public void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    public void MovePlayer()
    {
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
        _rb.AddForce(_moveDirection.normalized * (_currentSpeed * 10f), ForceMode.Force);
    }
    
    public void SpeedControl()
    {
        if (Input.GetKey(KeyCode.LeftShift) && debugRunEnabled)
        {
            _currentSpeed = moveSpeed * 10f;
        }
        else
        {
            _currentSpeed = moveSpeed;
        }
        Vector3 flatVel = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
        
        // limit velocity if needed
        if(flatVel.magnitude > _currentSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _currentSpeed;
            _rb.linearVelocity = new Vector3(limitedVel.x, _rb.linearVelocity.y, limitedVel.z);
        }
    }

    public void Gravity()
    {
        if (!grounded)
        {
            Vector3 gravity = GlobalGravity * gravityScale * Vector3.up;
            _rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }

    public void Teleport(Vector3 destinationPos)
    {
        _rb.position = destinationPos;
    }
    public void KeepPlayerAfloat()
    {
        if (transform.position.y < underNeathGroundTreshold && !grounded)
        {
            Teleport(_lastGroundPos);
        }
    }
}