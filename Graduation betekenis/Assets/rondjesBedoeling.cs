using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public class rondjesBedoeling : MonoBehaviour
{
    public GameObject rondje;
    [Space]
    public float groeiFactor;
    public float maxGrootte;
    public float minGrootte;
    [Space]
    public float snelheid;
    public buttoneigen linksKnop;
    public buttoneigen rechtsknop;
    public buttoneigen omhoogKnop;
    public buttoneigen omlaagKnop;
    public void groei()
    {
        if (rondje.transform.localScale.x < maxGrootte)
        {
            rondje.transform.localScale += new Vector3(groeiFactor, groeiFactor);
        }
    }

    public void krimp(int rondjeNummer)
    {
        if (rondje.transform.localScale.x > minGrootte)
        {
            rondje.transform.localScale -= new Vector3(groeiFactor, groeiFactor);
        }
    }

    private void Update()
    {
        if (linksKnop.isHeldDown)
        {
            rondje.transform.Translate(new Vector3(-1,0) * snelheid * Time.deltaTime);
        }
        if (rechtsknop.isHeldDown)
        {
            rondje.transform.Translate(new Vector3(1, 0) * snelheid * Time.deltaTime);
        }
        if (omhoogKnop.isHeldDown)
        {
            rondje.transform.Translate(new Vector3(0, 1) * snelheid * Time.deltaTime);
        }
        if (omlaagKnop.isHeldDown)
        {
            rondje.transform.Translate(new Vector3(0, -1) * snelheid * Time.deltaTime);
        }
    }
}