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
    [HideInInspector] public Texture fotoTexture;
    [SerializeField] private int aantalFotos;
    [SerializeField] private int fotoTreshold = 10;
    public UnityEvent onFotoTresholdBereikt;
    public UnityEvent onOphang;
    [Space] 
    public bool selectieActief;

    public GameObject ophangSelectieImage;
    public GameObject herinnertekst;
    
    private void Start()
    {
        for (int i = 0; i < ophangParent.transform.childCount; i++)
        {
            fotoOphangImages.Add(ophangParent.transform.GetChild(i).gameObject);
        }
    }
    
    public void HangFotoOp()
    {
        fotoOphangImages[aantalFotos].GetComponent<RawImage>().texture = fotoTexture;
        aantalFotos++;
        if (aantalFotos == fotoTreshold )
        {
            onFotoTresholdBereikt.Invoke();
        }
    }

    public void HerrinerDeSpeler(bool state)
    {
        herinnertekst.SetActive(state);
    }
    
    private void Update()
    {
        if (selectieActief && !ophangSelectieImage.activeSelf)
        {
            ophangSelectieImage.SetActive(true);
        }
        else if(!selectieActief && ophangSelectieImage.activeSelf)
        {
            ophangSelectieImage.SetActive(false);
        }
    }
}