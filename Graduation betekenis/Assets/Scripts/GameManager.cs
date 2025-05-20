using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform playerStartPos;
    public GameObject player;
    Player _playerRef;
    [Space] 
    public Image fadeVlak;

    public GameObject tutorialArea;
    public GameObject deurBeginRuimte;
    public Transform deurGeslotenPos;
    public Transform deurOpenPos;
    public audioManager audioManRef;
    private void Awake()
    {
        _playerRef = player.GetComponent<Player>();
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
            _playerRef.playerIsFrozen = true;
        }
        else if (!frozenBool)
        {
            _playerRef.playerIsFrozen = false;
        }
    }
    
    public void SetPlayerRotation(float xRotation, float yRotation)
    {
        _playerRef.playerLookRef.xRotation = xRotation;
        _playerRef.playerLookRef.yRotation = yRotation;
    }

    public void SetDeurBeginRuimte(int state)
    {
        switch (state)
        {
            case 0:
                deurBeginRuimte.transform.position = deurGeslotenPos.position;
                break;
            case 1:
                deurBeginRuimte.transform.position = deurOpenPos.position;
                break;
        }
    }
}