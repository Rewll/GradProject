using System;
using UnityEngine;

public class CollageKlaarRegelaar : MonoBehaviour
{
    public bool printen;
    public bool delen;
    [Space] 
    public GameObject printIcoon;
    public GameObject textAlgemeen;
    public GameObject printTekst;
    public GameObject deelIcoon;
    public GameObject deelTekst;
    public GameObject ampersandTekst;

    private void Awake()
    {
        printen = false;
        delen = false;
        printIcoon.SetActive(false);
        printTekst.SetActive(false);
        deelIcoon.SetActive(false);
        deelTekst.SetActive(false);
        textAlgemeen.SetActive(false);
        ampersandTekst.SetActive(false);
    }

    public void PrintenInschakel()
    {
        printen = true;
        printIcoon.SetActive(printen);
        printTekst.SetActive(printen);
        textAlgemeen.SetActive(printen);
    }

    public void DelenInschakel()
    {
        delen = true;
        deelIcoon.SetActive(delen);
        deelTekst.SetActive(delen);
        if (printen && delen)
        {
            ampersandTekst.SetActive(true);
        }
    }
}
