using System;
using UnityEngine;
using UnityEngine.UI;

public class LandschapManager : MonoBehaviour
{
    public Transform playerStartPos;
    public GameObject player;
    PlayerAgent _playerAgentRef;
    [Space] 
    public Image fadeVlak;

    public GameObject tutorialArea;
    public audioManager audioManRef;
    private void Awake()
    {
        _playerAgentRef = player.GetComponent<PlayerAgent>();
    }

    public void TeleportPlayer(Vector3 destinationPos)
    {
        player.GetComponent<PlayerMove>().Teleport(destinationPos);
    }

    public void SetCursorMode(int mode)
    {
        if (mode == 0)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; 
        }
        else if (mode == 1)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; 
        }
    }

    public void SetFreezePlayer(bool frozenBool)
    {
        if (frozenBool)
        {
            _playerAgentRef.playerIsFrozen = true;
        }
        else if (!frozenBool)
        {
            _playerAgentRef.playerIsFrozen = false;
        }
    }
    
    public void SetPlayerRotation(float xRotation, float yRotation)
    {
        _playerAgentRef.playerLookRef.xRotation = xRotation;
        _playerAgentRef.playerLookRef.yRotation = yRotation;
    }
}