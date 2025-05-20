using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

public class CollageManager : MonoBehaviour
{
    public List<Texture> picturesMade = new List<Texture>();
    public List<Texture> picturesToCollageWith = new List<Texture>();
    public int amountOfPicturesToCollageWith;
    public Texture testTexture;
    [Space] 
    public RenderTexture collageRT;

    public Texture2D collageTexture;
    public GameObject collagePreviewScherm;
}
