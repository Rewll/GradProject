using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuFade : MonoBehaviour
{
    public float fadeTime;
    public Image fadepPanel;
    public GameObject mainMenu;
    public GameObject titleScreen;
    private bool isFading = false;
    public AnimationCurve fadeCurve;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
        {
            StartCoroutine(FadeToMainMenu());
        }
    }

    IEnumerator FadeToMainMenu()
    {
        if(isFading)
        {
            yield break;
        }
        isFading = true;

        float t = 0.0f;

        while (t <= 1.0f)
        {
            fadepPanel.color = Color.Lerp(Color.clear, Color.black, fadeCurve.Evaluate(t));
            yield return null;
            t += Time.deltaTime/fadeTime;
        }

        fadepPanel.color = Color.black;
        titleScreen.SetActive(false);
        mainMenu.SetActive(true);
        t = 0.0f;


        while (t <= 1.0f)
        {
            fadepPanel.color = Color.Lerp(Color.clear, Color.black, fadeCurve.Evaluate(1.0f-t));
            yield return null;
            t += Time.deltaTime / fadeTime;
        }

        fadepPanel.color = Color.clear;
    }
}
