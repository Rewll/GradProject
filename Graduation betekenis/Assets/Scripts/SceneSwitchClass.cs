using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class SceneSwitchClass : MonoBehaviour
{
    public Image fadeVlak;
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void StartSpel(float fadeTime)
    {
        StartCoroutine(StartSpelRoutine(fadeTime));
    }
    IEnumerator StartSpelRoutine(float fadeTime)
    {
        yield return new WaitForSeconds(1f);
        Tween fadeTween = fadeVlak.DOFade(1, fadeTime);
        yield return fadeTween.WaitForCompletion();
        yield return new WaitForSeconds(1f);
        LoadScene(1);
    }
    public void SluitGameAf(float fadeTime)
    {
        StartCoroutine(AfsluitRoutine(fadeTime));
    }
    IEnumerator AfsluitRoutine(float fadeTime)
    {
        yield return new WaitForSeconds(1f);
        Tween fadeTween = fadeVlak.DOFade(1, fadeTime);
        yield return fadeTween.WaitForCompletion();
        Application.Quit();
    }
}
