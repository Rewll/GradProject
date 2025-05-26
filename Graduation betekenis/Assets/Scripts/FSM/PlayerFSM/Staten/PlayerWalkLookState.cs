using System.Transactions;
using UnityEngine;

public class PlayerWalkLookState : BaseState
{
    private PlayerAgent _playerAgentRef;
    [SerializeField] private PlayerMove playerMoveRef;
    
    
    void Awake()
    {
        _playerAgentRef = GetComponent<PlayerAgent>();
    }
    
    public override void OnEnter()
    {
        _playerAgentRef.huidigeStaat = PlayerStates.WalkLookState;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _playerAgentRef.kameraDisabledMesh.SetActive(true);
    }
    
    public override void OnUpdate()
    {
        playerMoveRef.MyInput();
        playerMoveRef.SpeedControl();
        _playerAgentRef.playerLookRef.MouseLook();
        
        if (Input.GetKeyDown(_playerAgentRef.CameraKnop))
        {
            owner.SwitchState(typeof(PlayerKameraState));
        }
    }
    public override void OnFixedUpdate()
    {
        playerMoveRef.MovePlayer();
        playerMoveRef.Gravity();
    }
    
    public override void OnExit()
    {
        _playerAgentRef.kameraDisabledMesh.SetActive(false);
    }
    public void SetPlayerRotation(float xRotation, float yRotation)
    {
        _playerAgentRef.playerLookRef.xRotation = xRotation;
        _playerAgentRef.playerLookRef.yRotation = yRotation;
    }
}