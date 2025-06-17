using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerLook playerRef;
    [SerializeField] private Slider muisSlider;
    private float mouseSensSaved;
    public bool paused = false;
    private bool _cursorWasVisible;
    
    public KeyCode pauseButton = KeyCode.Tab;
    private void Awake()
    {
        pauseMenu.SetActive(paused);
        if (playerRef)
        {
            muisSlider.value = playerRef.mouseSensitivity;
        }
    }

    //audiolistener.pause bestaat
    //hier is ook een ignore voor
    
    private void Update()
    {
        ResetFuncties();
        if (Input.GetKeyDown(pauseButton) && !paused)
        {
            SetPauseGame(true);
        }
        else if (Input.GetKeyDown(pauseButton) && paused)
        {
            SetPauseGame(false);
        }
    }

    public void SetPauseGame(bool setState)
    {
        if (setState)
        {
            paused = true;
            Time.timeScale = 0;
            //AudioListener.pause = true;
            _cursorWasVisible = Cursor.visible;
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; 
            pauseMenu.SetActive(true);
        }
        else
        {
            paused = false;
            Time.timeScale = 1;
            //AudioListener.pause = false;
            
            if (!_cursorWasVisible)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
            }
            pauseMenu.SetActive(false);
        }
    }
    
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ResetFuncties()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                LoadScene(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                LoadScene(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                LoadScene(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                LoadScene(0);
            }
        }
        
    }
}
