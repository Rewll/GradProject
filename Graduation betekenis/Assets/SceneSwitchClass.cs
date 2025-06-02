using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchClass : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
