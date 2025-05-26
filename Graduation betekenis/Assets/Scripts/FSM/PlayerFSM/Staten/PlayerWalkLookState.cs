using System.Transactions;
using UnityEngine;

public class PlayerWalkLookState : BaseState
{
    private PlayerAgent _playerAgentRef;
    
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
        _playerAgentRef.playerMoveRef.MyInput();
        _playerAgentRef.playerMoveRef.GroundCheck();
        _playerAgentRef.playerMoveRef.SpeedControl();
        _playerAgentRef.playerLookRef.MouseLook();
        
        if (Input.GetKeyDown(_playerAgentRef.CameraKnop))
        {
            owner.SwitchState(typeof(PlayerKameraState));
        }
    }
    public override void OnFixedUpdate()
    {
        _playerAgentRef.playerMoveRef.MovePlayer();
        _playerAgentRef.playerMoveRef.Gravity();
    }
    
    public override void OnExit()
    {
        _playerAgentRef.kameraDisabledMesh.SetActive(false);
    }
}