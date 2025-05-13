using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerStartPos;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
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
}