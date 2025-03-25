using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class gamemanager : MonoBehaviour
{
    public int beginOmgeving;
    [Space]
    public PROTO_Camera protCamRef;
    public UnityEvent alsOmgeving1Start;
    public UnityEvent alsOmgeving2Start;
    public UnityEvent alsKlaar;
    [Space]
    public GameObject knop;
    public GameObject mesh;
    public GameObject resultaat;
    public GameObject scherm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        protCamRef = GetComponent<PROTO_Camera>();
        if (beginOmgeving == 1)
        {
            alsOmgeving1Start.Invoke();
        }
        else if (beginOmgeving == 2)
        {
            alsOmgeving2Start.Invoke();
        }
    }

    public void alsOmgeving1()
    {
        protCamRef.nummer = 1;

    }

    public void alsOmgeving2()
    {
        protCamRef.nummer = 2;
    }

    public void naarOmgeving2()
    {
        if (protCamRef.nummer == 2)
        {
            alsKlaar.Invoke();
            knop.SetActive(false);
            mesh.SetActive(false);
            resultaat.SetActive(false);
            scherm.SetActive(false);
        }
        else
        {
            alsOmgeving2Start.Invoke();
            GetComponent<PROTO_playerModeManager>().onLoopMode.Invoke();
        }
    }
}