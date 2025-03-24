using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class PROTO_Camera : MonoBehaviour
{
    public Renderer fotoRenderer;
    public RenderTexture rendText;
    public Material mat;
    Texture2D destinationText;
    bool fotoAanHetMaken;
    
    public Camera fotoCamera;
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
        Texture2D fotoTexture = new Texture2D(width, height, TextureFormat.RGB24, true);
        Rect rect = new Rect(0, 0, width, height);
        fotoTexture.ReadPixels(rect, 0, 0);
        fotoTexture.Apply();
        mat.mainTexture = fotoTexture;
    }

    void OnApplicationQuit()
    {
        Debug.Log("hoi");
        mat = fotoRenderer.material;
    }
}