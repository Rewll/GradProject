using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;


public class FotoOphangManager : MonoBehaviour
{
    [Header("References: ")]
    public GameObject ophangSelectieImage;
    public GameObject herinnertekst;
    public List<GameObject> fotoOphangImages = new List<GameObject>();
    public GameObject ophangParent;
    public TMP_Text paginaText;
    [Space]
    private int _paginaNummer = 1;
    [HideInInspector] public Texture fotoTexture;
    [SerializeField] private int aantalFotos;
    private int fotoTreshold = 10;
    [Space]
    [Header("Events: ")]
    public UnityEvent onFotoTresholdBereikt;
    public UnityEvent onOphang;
    [Space] 
    public bool selectieActief;


    private void Start()
    {
        paginaText.gameObject.SetActive(false);
    }

    public void HangFotoOp()
    {
        fotoOphangImages[aantalFotos].GetComponent<RawImage>().texture = fotoTexture;
        aantalFotos++;
        if (aantalFotos == fotoTreshold)
        {
            onFotoTresholdBereikt.Invoke();
        }

        if (aantalFotos >= fotoOphangImages.Count)
        {
            StartCoroutine(VolgendePaginaRoutine());
        }
    }

    IEnumerator VolgendePaginaRoutine()
    {
        aantalFotos = 0;
            
        _paginaNummer++;
        paginaText.text = "Pagina: " + _paginaNummer.ToString();
        paginaText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        foreach (GameObject obj in fotoOphangImages)
        {
            obj.GetComponent<RawImage>().texture = null;
        }
    }

    public void HerrinerDeSpeler(bool state)
    {
        herinnertekst.SetActive(state);
        if (state)
        {
            StartCoroutine(VerdwijnRoutine());
        }
        Debug.Log("Herinnering");
    }

    IEnumerator VerdwijnRoutine()
    {
        yield return new WaitForSeconds(6f);
        herinnertekst.SetActive(false);
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