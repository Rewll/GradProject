using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class PROTO_Camera : MonoBehaviour
{
    public Renderer fotoRenderer;
    public RenderTexture rendText;
    public Material resultaat1Mat;
    public Material resultaat2Mat;
    public Material wit;
    Texture2D destinationText;
    [Space]
    public Image fotoplek1;
    public GameObject nogNietGemaaktTekst1;
    public Image fotoplek2;
    public GameObject nogNietGemaaktTekst2;
    public Image eindeFoto1;
    public Image eindeFoto2;
    [Space]
    public Camera fotoCamera;
    [Space]
    public int nummer;
    public void fotoMaken()
    {
        //screenGrabRenderer.material.mainTexture = ScreenCapture.CaptureScreenshotAsTexture();
        StartCoroutine(fotoMakenRoutine());
    }

    IEnumerator fotoMakenRoutine()
    {
        yield return new WaitForEndOfFrame();
        RenderTexture.active = rendText;
        int width = rendText.width;
        int height = rendText.height;
        Texture2D fotoTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
        Rect rect = new Rect(0, 0, width, height);
        fotoTexture.ReadPixels(rect, 0, 0);
        fotoTexture.Apply();
        if (nummer == 1)
        {
            fotoRenderer.material = resultaat1Mat;
            resultaat1Mat.mainTexture = fotoTexture;
            fotoplek1.material = resultaat1Mat;
            eindeFoto1.material = fotoplek1.material;
            nogNietGemaaktTekst1.SetActive(false);

        }
        else if (nummer == 2)
        {
            fotoRenderer.material = resultaat2Mat;
            resultaat2Mat.mainTexture = fotoTexture;
            fotoplek2.material = resultaat2Mat;
            eindeFoto2.material = fotoplek2.material;
            nogNietGemaaktTekst2.SetActive(false);
        }
    }

    public void resetResultaat()
    {
        fotoRenderer.material = wit;
    }


    void OnApplicationQuit()
    {
        Debug.Log("hoi");
        resultaat1Mat = fotoRenderer.material;
    }

}