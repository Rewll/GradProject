using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class FotoOphangManager : MonoBehaviour
{
    public List<GameObject> fotoOphangImages = new List<GameObject>();
    [SerializeField] private int aantalFotos;
    [SerializeField] private int fotoTreshold = 10;
    public UnityEvent onFotoTresholdBereikt;
    public void HangFotoOp(Texture pictureTexture)
    {
        fotoOphangImages[aantalFotos].GetComponent<RawImage>().texture = pictureTexture;
        aantalFotos++;
        if (aantalFotos == fotoTreshold )
        {
            onFotoTresholdBereikt.Invoke();
        }
    }
}
