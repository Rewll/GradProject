using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerLook playerRef;
    [SerializeField] private Slider muisSlider;
    private float mouseSensSaved;
    public bool paused = false;
    private bool _cursorWasVisible;
    [SerializeField] private Image fadeVlak;
    [SerializeField] private float sceneFadeTime = 2f;
    private playerprefspoep playerPrepRef;
    public KeyCode pauseButton = KeyCode.Escape;
    public List<GameObject> destroyObjects;
    [Space] 
    public bool collage;
    
    private void Awake()
    {
        pauseMenu.SetActive(paused);
        if (FindAnyObjectByType<playerprefspoep>() && !collage)
        {
            playerPrepRef = FindAnyObjectByType<playerprefspoep>();
            //Debug.Log("Slider wordt gezet");
            playerPrepRef.LaadGetal();
            muisSlider.value = playerRef.mouseSensitivity;
            destroyObjects.Add(playerPrepRef.gameObject);
        }
        else if (!collage)
        {
            Debug.Log("Er is geen playerPref poep");
        }
    }

    //audiolistener.pause bestaat
    //hier is ook een ignore voor
    private void Start()
    {
#if UNITY_EDITOR

        Debug.Log("Unity Editor");
        pauseButton = KeyCode.Tab;
#endif
    }

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
    
    public void VeranderScene(int sceneIndex)
    {

        StartCoroutine(LaadSceneRoutine(sceneIndex));
    }

    void ClearObjects(int sceneIndex )
    {
        if (sceneIndex == 0)
        {
            foreach (GameObject obj in destroyObjects)
            {
                Destroy(obj);
            }
        }
    }
    public IEnumerator LaadSceneRoutine(int sceneIndex)
    {
        fadeVlak.gameObject.SetActive(true);
        Tween fadeTween = fadeVlak.DOFade(1, sceneFadeTime);
        fadeTween.SetUpdate(true);
        yield return fadeTween.WaitForCompletion();
        yield return new WaitForSecondsRealtime(1f);
        ClearObjects(sceneIndex);
        Time.timeScale = 1;
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
                VeranderScene(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                VeranderScene(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                VeranderScene(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                VeranderScene(0);

            }
        }
        
    }
}
