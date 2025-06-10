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
    public GameObject ophangParent;
    [SerializeField] private int aantalFotos;
    [SerializeField] private int fotoTreshold = 10;
    public UnityEvent onFotoTresholdBereikt;
    public UnityEvent onOphang;

    private void Start()
    {
        for (int i = 0; i < ophangParent.transform.childCount; i++)
        {
            fotoOphangImages.Add(ophangParent.transform.GetChild(i).gameObject);
        }
    }

    public void HangFotoOp(Texture pictureTexture)
    {
        fotoOphangImages[aantalFotos].GetComponent<RawImage>().texture = pictureTexture;
        aantalFotos++;
        if (aantalFotos == fotoTreshold )
        {
            onFotoTresholdBereikt.Invoke();
        }
        onOphang.Invoke();
    }
}
